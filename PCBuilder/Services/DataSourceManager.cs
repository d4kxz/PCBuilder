using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Models;
using PCBuilder.Services.Connectors;

namespace PCBuilder.Services
{
    /// <summary>
    /// Управляет подключением внешних источников данных и синхронизацией.
    /// Регистрируется как Singleton в Program.cs
    /// </summary>
    public class DataSourceManager
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebHostEnvironment  _env;
        private readonly ILogger<DataSourceManager> _logger;

        // Папка для загружаемых файлов (Excel, CSV, XML)
        public string UploadsPath => Path.Combine(_env.ContentRootPath, "DataUploads");

        public DataSourceManager(
            IServiceScopeFactory scopeFactory,
            IWebHostEnvironment env,
            ILogger<DataSourceManager> logger)
        {
            _scopeFactory = scopeFactory;
            _env          = env;
            _logger       = logger;

            Directory.CreateDirectory(UploadsPath);
        }

        // ── Получить нужный коннектор ────────────────────────────────
        public IDataSourceConnector GetConnector(DataSourceType type) => type switch
        {
            DataSourceType.SqlServer  => new SqlServerConnector(),
            DataSourceType.MySql      => new MySqlDbConnector(),
            DataSourceType.PostgreSql => new PostgreSqlConnector(),
            DataSourceType.Sqlite     => new SqliteConnector(),
            DataSourceType.Excel      => new ExcelConnector(),
            DataSourceType.Csv        => new CsvConnector(),
            DataSourceType.Xml        => new XmlConnector(),
            _                         => throw new NotSupportedException($"Тип {type} не поддерживается")
        };

        // ── Тест подключения ─────────────────────────────────────────
        public async Task<(bool ok, string error)> TestAsync(DataSource ds)
        {
            try
            {
                var connector = GetConnector(ds.Type);
                return await connector.TestConnectionAsync(ds);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // ── Получить таблицы / листы ─────────────────────────────────
        public async Task<List<string>> GetTablesAsync(DataSource ds)
        {
            var connector = GetConnector(ds.Type);
            return await connector.GetTablesAsync(ds);
        }

        // ── Получить колонки ─────────────────────────────────────────
        public async Task<List<string>> GetColumnsAsync(DataSource ds, string table)
        {
            var connector = GetConnector(ds.Type);
            return await connector.GetColumnsAsync(ds, table);
        }

        // ── Синхронизация одного источника ──────────────────────────
        public async Task<SyncResult> SyncAsync(int dataSourceId)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var ds = await db.DataSources.FindAsync(dataSourceId);
            if (ds == null) return new SyncResult(false, 0, "Источник не найден");

            ds.LastSyncStatus = SyncStatus.Running;
            await db.SaveChangesAsync();

            try
            {
                _logger.LogInformation("Синхронизация источника #{Id} «{Name}» начата", ds.Id, ds.Name);

                var connector = GetConnector(ds.Type);
                var imported  = await connector.FetchDataAsync(ds);

                if (!imported.Any())
                {
                    ds.LastSyncStatus = SyncStatus.Error;
                    ds.LastSyncError  = "Данные не получены — проверьте маппинг колонок";
                    ds.LastSyncAt     = DateTime.UtcNow;
                    await db.SaveChangesAsync();
                    return new SyncResult(false, 0, ds.LastSyncError);
                }

                int added = 0, updated = 0;

                foreach (var item in imported)
                {
                    if (string.IsNullOrWhiteSpace(item.Name) || item.Price <= 0) continue;

                    // Нормализуем категорию
                    var category = NormalizeCategory(item.Category);
                    if (string.IsNullOrEmpty(category)) continue;

                    // Ищем существующий компонент по имени + источнику
                    var existing = await db.Components
                        .Include(c => c.Offers)
                        .FirstOrDefaultAsync(c =>
                            c.Name == item.Name &&
                            c.Offers.Any(o => o.MerchantName == item.MerchantName));

                    if (existing != null)
                    {
                        // Обновляем оффер
                        var offer = existing.Offers.First(o => o.MerchantName == item.MerchantName);
                        offer.Price      = item.Price;
                        offer.ProductUrl = item.ProductUrl;
                        updated++;
                    }
                    else
                    {
                        // Создаём новый компонент или добавляем оффер к существующему
                        var component = await db.Components
                            .Include(c => c.Offers)
                            .FirstOrDefaultAsync(c => c.Name == item.Name && c.Category == category);

                        if (component == null)
                        {
                            component = new Component
                            {
                                Name     = item.Name,
                                Category = category,
                                Specs    = item.Specs,
                                ImageUrl = item.ImageUrl ?? "",
                            };
                            db.Components.Add(component);
                            await db.SaveChangesAsync(); // нужен Id
                        }

                        db.Set<ProductOffer>().Add(new ProductOffer
                        {
                            ComponentId  = component.Id,
                            MerchantName = item.MerchantName,
                            Price        = item.Price,
                            ProductUrl   = item.ProductUrl,
                        });
                        added++;
                    }
                }

                await db.SaveChangesAsync();

                ds.LastSyncStatus = SyncStatus.Success;
                ds.LastSyncError  = null;
                ds.LastSyncAt     = DateTime.UtcNow;
                ds.SyncedCount    = added + updated;
                await db.SaveChangesAsync();

                _logger.LogInformation("Синхронизация #{Id} завершена: +{Added} новых, ~{Updated} обновлено", ds.Id, added, updated);
                return new SyncResult(true, added + updated, $"Добавлено: {added}, обновлено: {updated}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка синхронизации источника #{Id}", ds.Id);
                ds.LastSyncStatus = SyncStatus.Error;
                ds.LastSyncError  = ex.Message;
                ds.LastSyncAt     = DateTime.UtcNow;
                await db.SaveChangesAsync();
                return new SyncResult(false, 0, ex.Message);
            }
        }

        // ── Нормализация категорий (приводим к нашим именам) ─────────
        private static readonly Dictionary<string, string> _categoryMap = new(StringComparer.OrdinalIgnoreCase)
        {
            // Процессоры
            ["процессор"]         = "Процессор",
            ["cpu"]               = "Процессор",
            ["processor"]         = "Процессор",
            // Материнские платы
            ["материнская плата"] = "Материнская плата",
            ["motherboard"]       = "Материнская плата",
            ["mainboard"]         = "Материнская плата",
            ["mb"]                = "Материнская плата",
            // Видеокарты
            ["видеокарта"]        = "Видеокарта",
            ["gpu"]               = "Видеокарта",
            ["videocard"]         = "Видеокарта",
            ["graphics card"]     = "Видеокарта",
            // Оперативная память
            ["оперативная память"]= "Оперативная память",
            ["ram"]               = "Оперативная память",
            ["память"]            = "Оперативная память",
            ["memory"]            = "Оперативная память",
            // SSD
            ["накопитель ssd"]    = "Накопитель SSD",
            ["ssd"]               = "Накопитель SSD",
            ["накопитель"]        = "Накопитель SSD",
            ["storage"]           = "Накопитель SSD",
            // Блок питания
            ["блок питания"]      = "Блок питания",
            ["psu"]               = "Блок питания",
            ["power supply"]      = "Блок питания",
            // Корпус
            ["корпус"]            = "Корпус",
            ["case"]              = "Корпус",
            ["chassis"]           = "Корпус",
            // Охлаждение
            ["охлаждение"]        = "Охлаждение",
            ["кулер"]             = "Охлаждение",
            ["cooler"]            = "Охлаждение",
            ["cooling"]           = "Охлаждение",
        };

        public string NormalizeCategory(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "";
            var key = raw.Trim().ToLower();
            // Точное совпадение
            if (_categoryMap.TryGetValue(key, out var exact)) return exact;
            // Частичное совпадение
            foreach (var (pattern, mapped) in _categoryMap)
                if (key.Contains(pattern)) return mapped;
            return "";
        }
    }

    public record SyncResult(bool Success, int Count, string Message);
}
