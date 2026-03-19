using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Models;
using PCBuilder.Services;

namespace PCBuilder.Pages.Admin
{
    public class DataSourcesModel : PageModel
    {
        private readonly ApplicationContext  _db;
        private readonly DataSourceManager   _manager;

        public List<DataSource> DataSources { get; set; } = new();

        public DataSourcesModel(ApplicationContext db, DataSourceManager manager)
        {
            _db      = db;
            _manager = manager;
        }

        public async Task OnGetAsync()
        {
            DataSources = await _db.DataSources.OrderByDescending(d => d.CreatedAt).ToListAsync();
        }

        // ── Проверить подключение ─────────────────────────────────────
        public async Task<JsonResult> OnPostTestConnectionAsync([FromBody] DataSourceDto dto)
        {
            var ds = dto.ToDataSource(_manager.UploadsPath);
            var (ok, error) = await _manager.TestAsync(ds);
            return new JsonResult(new { ok, error });
        }

        // ── Загрузить таблицы / листы ─────────────────────────────────
        public async Task<JsonResult> OnPostGetTablesAsync([FromBody] DataSourceDto dto)
        {
            var ds     = dto.ToDataSource(_manager.UploadsPath);
            var tables = await _manager.GetTablesAsync(ds);
            return new JsonResult(new { tables });
        }

        // ── Загрузить колонки ─────────────────────────────────────────
        public async Task<JsonResult> OnPostGetColumnsAsync([FromBody] DataSourceDto dto)
        {
            var ds      = dto.ToDataSource(_manager.UploadsPath);
            var columns = await _manager.GetColumnsAsync(ds, dto.TableName ?? "");
            return new JsonResult(new { columns });
        }

        // ── Загрузить файл (Excel/CSV/XML) ────────────────────────────
        public async Task<JsonResult> OnPostUploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new JsonResult(new { error = "Файл пустой" });

            var allowed = new[] { ".xlsx", ".csv", ".xml" };
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowed.Contains(ext))
                return new JsonResult(new { error = "Недопустимый тип файла" });

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(_manager.UploadsPath, fileName);

            await using var stream = System.IO.File.Create(fullPath);
            await file.CopyToAsync(stream);

            return new JsonResult(new { path = fullPath, name = file.FileName });
        }

        // ── Сохранить и синхронизировать ──────────────────────────────
        public async Task<JsonResult> OnPostSaveAndSyncAsync([FromBody] DataSourceDto dto)
        {
            var ds = dto.ToDataSource(_manager.UploadsPath);

            _db.DataSources.Add(ds);
            await _db.SaveChangesAsync();

            var syncResult = await _manager.SyncAsync(ds.Id);

            return new JsonResult(new
            {
                ok      = syncResult.Success,
                message = syncResult.Message,
                error   = syncResult.Success ? null : syncResult.Message
            });
        }

        // ── Синхронизировать существующий ─────────────────────────────
        public async Task<JsonResult> OnPostSyncAsync(int id)
        {
            var result = await _manager.SyncAsync(id);
            return new JsonResult(new { ok = result.Success, message = result.Message });
        }

        // ── Удалить источник ──────────────────────────────────────────
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var ds = await _db.DataSources.FindAsync(id);
            if (ds != null)
            {
                _db.DataSources.Remove(ds);
                await _db.SaveChangesAsync();
            }
            return new JsonResult(new { ok = true });
        }
    }

    // ── DTO для приёма JSON из формы ─────────────────────────────────
    public class DataSourceDto
    {
        public string?         Name              { get; set; }
        public int             Type              { get; set; }
        public string?         ConnectionString  { get; set; }
        public string?         TableName         { get; set; }
        public string?         SchemaName        { get; set; }
        public string?         FilePath          { get; set; }
        public string?         XmlItemPath       { get; set; }
        public string?         ColName           { get; set; }
        public string?         ColCategory       { get; set; }
        public string?         ColPrice          { get; set; }
        public string?         ColSpecs          { get; set; }
        public string?         ColProductUrl     { get; set; }
        public string?         ColImageUrl       { get; set; }
        public string?         ColMerchantName   { get; set; }
        public string?         DefaultMerchant   { get; set; }
        public string?         DefaultCategory   { get; set; }
        public bool            AutoSync          { get; set; }
        public int             AutoSyncIntervalH { get; set; } = 24;

        public DataSource ToDataSource(string uploadsPath) => new()
        {
            Name              = Name ?? "",
            Type              = (DataSourceType)Type,
            ConnectionString  = ConnectionString,
            TableName         = TableName,
            SchemaName        = SchemaName,
            FilePath          = FilePath,
            XmlItemPath       = XmlItemPath,
            ColName           = ColName,
            ColCategory       = ColCategory,
            ColPrice          = ColPrice,
            ColSpecs          = ColSpecs,
            ColProductUrl     = ColProductUrl,
            ColImageUrl       = ColImageUrl,
            ColMerchantName   = ColMerchantName,
            DefaultMerchant   = DefaultMerchant,
            DefaultCategory   = DefaultCategory,
            AutoSync          = AutoSync,
            AutoSyncIntervalH = AutoSyncIntervalH,
        };
    }
}
