using PCBuilder.Models;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace PCBuilder.Services.Connectors
{
    // ══════════════════════════════════════════════════════════════════
    //  ИНТЕРФЕЙС — все коннекторы реализуют этот контракт
    // ══════════════════════════════════════════════════════════════════

    public interface IDataSourceConnector
    {
        /// <summary>
        /// Проверить соединение / доступность файла
        /// </summary>
        Task<(bool ok, string error)> TestConnectionAsync(DataSource ds);

        /// <summary>
        /// Получить список таблиц (для SQL) или листов (для Excel)
        /// </summary>
        Task<List<string>> GetTablesAsync(DataSource ds);

        /// <summary>
        /// Получить список колонок таблицы
        /// </summary>
        Task<List<string>> GetColumnsAsync(DataSource ds, string tableName);

        /// <summary>
        /// Загрузить данные и вернуть список компонентов с офферами
        /// </summary>
        Task<List<ImportedComponent>> FetchDataAsync(DataSource ds);
    }

    // ══════════════════════════════════════════════════════════════════
    //  DTO — результат импорта
    // ══════════════════════════════════════════════════════════════════

    public class ImportedComponent
    {
        public string  Name         { get; set; } = "";
        public string  Category     { get; set; } = "";
        public string  Specs        { get; set; } = "";
        public string? ImageUrl     { get; set; }
        public decimal Price        { get; set; }
        public string  MerchantName { get; set; } = "";
        public string? ProductUrl   { get; set; }
    }

    // ══════════════════════════════════════════════════════════════════
    //  БАЗОВЫЙ SQL-КОННЕКТОР (общая логика для всех SQL-источников)
    // ══════════════════════════════════════════════════════════════════

    public abstract class BaseSqlConnector : IDataSourceConnector
    {
        protected abstract DbConnection CreateConnection(string connectionString);

        public async Task<(bool ok, string error)> TestConnectionAsync(DataSource ds)
        {
            try
            {
                await using var conn = CreateConnection(ds.ConnectionString!);
                await conn.OpenAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<string>> GetTablesAsync(DataSource ds)
        {
            var tables = new List<string>();
            await using var conn = CreateConnection(ds.ConnectionString!);
            await conn.OpenAsync();
            var schema = await conn.GetSchemaAsync("Tables");
            foreach (DataRow row in schema.Rows)
            {
                var tableName = row[2]?.ToString();
                if (!string.IsNullOrEmpty(tableName))
                    tables.Add(tableName);
            }
            return tables;
        }

        public async Task<List<string>> GetColumnsAsync(DataSource ds, string tableName)
        {
            var columns = new List<string>();
            await using var conn = CreateConnection(ds.ConnectionString!);
            await conn.OpenAsync();
            var schema = await conn.GetSchemaAsync("Columns", new[] { null, null, tableName });
            foreach (DataRow row in schema.Rows)
            {
                var colName = row["COLUMN_NAME"]?.ToString();
                if (!string.IsNullOrEmpty(colName))
                    columns.Add(colName);
            }
            return columns;
        }

        public async Task<List<ImportedComponent>> FetchDataAsync(DataSource ds)
        {
            var result = new List<ImportedComponent>();
            await using var conn = CreateConnection(ds.ConnectionString!);
            await conn.OpenAsync();

            var schema = string.IsNullOrEmpty(ds.SchemaName) ? "" : $"{ds.SchemaName}.";
            var table  = string.IsNullOrEmpty(ds.TableName)  ? "products" : ds.TableName;

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {schema}{table}";

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(MapRow(reader, ds));
            }
            return result;
        }

        private ImportedComponent MapRow(DbDataReader reader, DataSource ds)
        {
            string Get(string? col) =>
                !string.IsNullOrEmpty(col) && HasColumn(reader, col)
                    ? reader[col]?.ToString() ?? ""
                    : "";

            decimal GetDecimal(string? col)
            {
                var v = Get(col);
                return decimal.TryParse(v, out var d) ? d : 0;
            }

            return new ImportedComponent
            {
                Name         = Get(ds.ColName),
                Category     = !string.IsNullOrEmpty(ds.ColCategory)
                                   ? Get(ds.ColCategory)
                                   : (ds.DefaultCategory ?? ""),
                Specs        = Get(ds.ColSpecs),
                ImageUrl     = Get(ds.ColImageUrl),
                Price        = GetDecimal(ds.ColPrice),
                MerchantName = !string.IsNullOrEmpty(ds.ColMerchantName)
                                   ? Get(ds.ColMerchantName)
                                   : (ds.DefaultMerchant ?? ds.Name),
                ProductUrl   = Get(ds.ColProductUrl),
            };
        }

        private bool HasColumn(DbDataReader reader, string colName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  SQL SERVER (1С, корпоративные ERP, большинство Windows-систем)
    // ══════════════════════════════════════════════════════════════════

    public class SqlServerConnector : BaseSqlConnector
    {
        protected override DbConnection CreateConnection(string connectionString)
        {
            // Microsoft.Data.SqlClient
            return new Microsoft.Data.SqlClient.SqlConnection(connectionString);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  MYSQL / MARIADB (интернет-магазины, WordPress/WooCommerce, OpenCart)
    //  Класс назван MySqlDbConnector чтобы не конфликтовать с namespace
    //  MySqlConnector из одноимённого NuGet-пакета
    // ══════════════════════════════════════════════════════════════════

    public class MySqlDbConnector : BaseSqlConnector
    {
        protected override DbConnection CreateConnection(string connectionString)
        {
            // MySqlConnector NuGet package → namespace MySqlConnector, класс MySqlConnection
            return new global::MySqlConnector.MySqlConnection(connectionString);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  POSTGRESQL (современные системы, Linux-серверы)
    // ══════════════════════════════════════════════════════════════════

    public class PostgreSqlConnector : BaseSqlConnector
    {
        protected override DbConnection CreateConnection(string connectionString)
        {
            // Npgsql NuGet package
            return new Npgsql.NpgsqlConnection(connectionString);
        }

        // PostgreSQL использует другую схему для получения таблиц
        public new async Task<List<string>> GetTablesAsync(DataSource ds)
        {
            var tables = new List<string>();
            await using var conn = new Npgsql.NpgsqlConnection(ds.ConnectionString);
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT table_name FROM information_schema.tables
                WHERE table_schema = 'public'
                ORDER BY table_name";

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                tables.Add(reader.GetString(0));

            return tables;
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  SQLITE (локальные базы, небольшие магазины)
    // ══════════════════════════════════════════════════════════════════

    public class SqliteConnector : BaseSqlConnector
    {
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  EXCEL (.xlsx — выгрузка из 1С, прайс-листы)
    // ══════════════════════════════════════════════════════════════════

    public class ExcelConnector : IDataSourceConnector
    {
        public Task<(bool ok, string error)> TestConnectionAsync(DataSource ds)
        {
            if (string.IsNullOrEmpty(ds.FilePath) || !File.Exists(ds.FilePath))
                return Task.FromResult((false, $"Файл не найден: {ds.FilePath}"));
            return Task.FromResult((true, ""));
        }

        public Task<List<string>> GetTablesAsync(DataSource ds)
        {
            // Возвращает список листов Excel
            var sheets = new List<string>();
            try
            {
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using var package = new OfficeOpenXml.ExcelPackage(new FileInfo(ds.FilePath!));
                foreach (var ws in package.Workbook.Worksheets)
                    sheets.Add(ws.Name);
            }
            catch { }
            return Task.FromResult(sheets);
        }

        public Task<List<string>> GetColumnsAsync(DataSource ds, string sheetName)
        {
            var columns = new List<string>();
            try
            {
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using var package = new OfficeOpenXml.ExcelPackage(new FileInfo(ds.FilePath!));
                var ws = package.Workbook.Worksheets[sheetName]
                      ?? package.Workbook.Worksheets[0];
                if (ws == null) return Task.FromResult(columns);

                // Первая строка = заголовки
                int col = 1;
                while (ws.Cells[1, col].Value != null)
                {
                    columns.Add(ws.Cells[1, col].Value!.ToString()!);
                    col++;
                }
            }
            catch { }
            return Task.FromResult(columns);
        }

        public Task<List<ImportedComponent>> FetchDataAsync(DataSource ds)
        {
            var result = new List<ImportedComponent>();
            try
            {
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using var package = new OfficeOpenXml.ExcelPackage(new FileInfo(ds.FilePath!));

                var sheetName = ds.TableName;
                var ws = (!string.IsNullOrEmpty(sheetName) && package.Workbook.Worksheets[sheetName] != null)
                    ? package.Workbook.Worksheets[sheetName]
                    : package.Workbook.Worksheets[0];

                if (ws == null) return Task.FromResult(result);

                // Читаем заголовки из первой строки
                var headers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                int colCount = ws.Dimension?.Columns ?? 0;
                for (int c = 1; c <= colCount; c++)
                {
                    var h = ws.Cells[1, c].Value?.ToString();
                    if (!string.IsNullOrEmpty(h)) headers[h] = c;
                }

                string GetCell(string? colName, int row)
                {
                    if (string.IsNullOrEmpty(colName)) return "";
                    if (headers.TryGetValue(colName, out int colIdx))
                        return ws.Cells[row, colIdx].Value?.ToString() ?? "";
                    return "";
                }

                int rowCount = ws.Dimension?.Rows ?? 0;
                for (int r = 2; r <= rowCount; r++) // строки с данными (со 2-й)
                {
                    var name = GetCell(ds.ColName, r);
                    if (string.IsNullOrEmpty(name)) continue;

                    var priceStr = GetCell(ds.ColPrice, r);
                    decimal.TryParse(priceStr, out var price);

                    result.Add(new ImportedComponent
                    {
                        Name         = name,
                        Category     = !string.IsNullOrEmpty(ds.ColCategory)
                                           ? GetCell(ds.ColCategory, r)
                                           : (ds.DefaultCategory ?? ""),
                        Specs        = GetCell(ds.ColSpecs, r),
                        ImageUrl     = GetCell(ds.ColImageUrl, r),
                        Price        = price,
                        MerchantName = !string.IsNullOrEmpty(ds.ColMerchantName)
                                           ? GetCell(ds.ColMerchantName, r)
                                           : (ds.DefaultMerchant ?? ds.Name),
                        ProductUrl   = GetCell(ds.ColProductUrl, r),
                    });
                }
            }
            catch { }
            return Task.FromResult(result);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  CSV (простые таблицы, выгрузки из любых систем)
    // ══════════════════════════════════════════════════════════════════

    public class CsvConnector : IDataSourceConnector
    {
        public Task<(bool ok, string error)> TestConnectionAsync(DataSource ds)
        {
            if (string.IsNullOrEmpty(ds.FilePath) || !File.Exists(ds.FilePath))
                return Task.FromResult((false, $"Файл не найден: {ds.FilePath}"));
            return Task.FromResult((true, ""));
        }

        public Task<List<string>> GetTablesAsync(DataSource ds) =>
            Task.FromResult(new List<string> { Path.GetFileName(ds.FilePath ?? "") });

        public Task<List<string>> GetColumnsAsync(DataSource ds, string tableName)
        {
            var cols = new List<string>();
            try
            {
                var firstLine = File.ReadLines(ds.FilePath!).FirstOrDefault();
                if (firstLine != null)
                    cols = firstLine.Split(',', ';', '\t')
                                   .Select(c => c.Trim('"', ' '))
                                   .ToList();
            }
            catch { }
            return Task.FromResult(cols);
        }

        public Task<List<ImportedComponent>> FetchDataAsync(DataSource ds)
        {
            var result = new List<ImportedComponent>();
            try
            {
                var lines = File.ReadAllLines(ds.FilePath!);
                if (lines.Length < 2) return Task.FromResult(result);

                // Авто-определение разделителя
                char sep = lines[0].Contains(';') ? ';' : lines[0].Contains('\t') ? '\t' : ',';

                var headers = lines[0].Split(sep)
                    .Select((h, i) => (h.Trim('"', ' '), i))
                    .ToDictionary(x => x.Item1, x => x.Item2, StringComparer.OrdinalIgnoreCase);

                string GetField(string? col, string[] fields)
                {
                    if (string.IsNullOrEmpty(col)) return "";
                    if (headers.TryGetValue(col, out int idx) && idx < fields.Length)
                        return fields[idx].Trim('"', ' ');
                    return "";
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    var fields = lines[i].Split(sep);
                    var name   = GetField(ds.ColName, fields);
                    if (string.IsNullOrEmpty(name)) continue;

                    decimal.TryParse(GetField(ds.ColPrice, fields), out var price);

                    result.Add(new ImportedComponent
                    {
                        Name         = name,
                        Category     = !string.IsNullOrEmpty(ds.ColCategory)
                                           ? GetField(ds.ColCategory, fields)
                                           : (ds.DefaultCategory ?? ""),
                        Specs        = GetField(ds.ColSpecs, fields),
                        ImageUrl     = GetField(ds.ColImageUrl, fields),
                        Price        = price,
                        MerchantName = !string.IsNullOrEmpty(ds.ColMerchantName)
                                           ? GetField(ds.ColMerchantName, fields)
                                           : (ds.DefaultMerchant ?? ds.Name),
                        ProductUrl   = GetField(ds.ColProductUrl, fields),
                    });
                }
            }
            catch { }
            return Task.FromResult(result);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    //  XML (прайс-листы поставщиков, выгрузка 1С CommerceML)
    // ══════════════════════════════════════════════════════════════════

    public class XmlConnector : IDataSourceConnector
    {
        public Task<(bool ok, string error)> TestConnectionAsync(DataSource ds)
        {
            if (string.IsNullOrEmpty(ds.FilePath) || !File.Exists(ds.FilePath))
                return Task.FromResult((false, $"Файл не найден: {ds.FilePath}"));
            try { XDocument.Load(ds.FilePath); return Task.FromResult((true, "")); }
            catch (Exception ex) { return Task.FromResult((false, ex.Message)); }
        }

        public Task<List<string>> GetTablesAsync(DataSource ds)
        {
            // Возвращает список уникальных корневых элементов — подсказка для XPath
            var hints = new List<string>();
            try
            {
                var doc = XDocument.Load(ds.FilePath!);
                hints = doc.Root?.Elements()
                    .Select(e => e.Name.LocalName)
                    .Distinct()
                    .Take(10)
                    .ToList() ?? hints;
            }
            catch { }
            return Task.FromResult(hints);
        }

        public Task<List<string>> GetColumnsAsync(DataSource ds, string elementName)
        {
            // Возвращает атрибуты и дочерние элементы первого найденного узла
            var attrs = new List<string>();
            try
            {
                var doc  = XDocument.Load(ds.FilePath!);
                var item = doc.Descendants(elementName).FirstOrDefault();
                if (item != null)
                {
                    attrs.AddRange(item.Attributes().Select(a => "@" + a.Name.LocalName));
                    attrs.AddRange(item.Elements().Select(e => e.Name.LocalName));
                }
            }
            catch { }
            return Task.FromResult(attrs);
        }

        public Task<List<ImportedComponent>> FetchDataAsync(DataSource ds)
        {
            var result = new List<ImportedComponent>();
            try
            {
                var doc      = XDocument.Load(ds.FilePath!);
                var itemPath = ds.XmlItemPath ?? "product"; // XPath к элементу товара
                var items    = doc.Descendants(itemPath);

                string GetVal(XElement el, string? field)
                {
                    if (string.IsNullOrEmpty(field)) return "";
                    // Поддержка @атрибутов и дочерних элементов
                    if (field.StartsWith("@"))
                        return el.Attribute(field[1..])?.Value ?? "";
                    return el.Element(field)?.Value ?? "";
                }

                foreach (var item in items)
                {
                    var name = GetVal(item, ds.ColName);
                    if (string.IsNullOrEmpty(name)) continue;

                    decimal.TryParse(GetVal(item, ds.ColPrice), out var price);

                    result.Add(new ImportedComponent
                    {
                        Name         = name,
                        Category     = !string.IsNullOrEmpty(ds.ColCategory)
                                           ? GetVal(item, ds.ColCategory)
                                           : (ds.DefaultCategory ?? ""),
                        Specs        = GetVal(item, ds.ColSpecs),
                        ImageUrl     = GetVal(item, ds.ColImageUrl),
                        Price        = price,
                        MerchantName = !string.IsNullOrEmpty(ds.ColMerchantName)
                                           ? GetVal(item, ds.ColMerchantName)
                                           : (ds.DefaultMerchant ?? ds.Name),
                        ProductUrl   = GetVal(item, ds.ColProductUrl),
                    });
                }
            }
            catch { }
            return Task.FromResult(result);
        }
    }
}
