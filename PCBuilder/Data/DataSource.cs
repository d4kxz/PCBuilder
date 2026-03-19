namespace PCBuilder.Models
{
    /// <summary>
    /// Тип источника данных, который подключает компания-клиент
    /// </summary>
    public enum DataSourceType
    {
        SqlServer   = 1,  // Microsoft SQL Server (1С, корпоративные ERP)
        MySql       = 2,  // MySQL / MariaDB (интернет-магазины, CMS)
        PostgreSql  = 3,  // PostgreSQL (современные системы)
        Sqlite      = 4,  // SQLite (локальные базы, небольшие магазины)
        Excel       = 5,  // Excel .xlsx (выгрузка из 1С, прайс-листы)
        Csv         = 6,  // CSV файлы
        Xml         = 7,  // XML (прайс-листы поставщиков, 1С-выгрузка)
    }

    /// <summary>
    /// Статус последней синхронизации
    /// </summary>
    public enum SyncStatus
    {
        Never    = 0,
        Success  = 1,
        Error    = 2,
        Running  = 3,
    }

    /// <summary>
    /// Источник данных компании-клиента.
    /// Хранит настройки подключения и маппинг колонок.
    /// </summary>
    public class DataSource
    {
        public int    Id          { get; set; }
        public string Name        { get; set; } = "";          // Название (напр. "База DNS")
        public DataSourceType Type { get; set; }

        // ── Подключение (для SQL-источников) ─────────────────────────
        public string? ConnectionString { get; set; }          // Строка подключения
        public string? TableName        { get; set; }          // Таблица с товарами
        public string? SchemaName       { get; set; }          // Схема (для PG/MSSQL)

        // ── Файловые источники (Excel, CSV, XML) ──────────────────────
        public string? FilePath         { get; set; }          // Путь к файлу на сервере
        public string? XmlItemPath      { get; set; }          // XPath к элементу товара

        // ── Маппинг колонок → поля Component ─────────────────────────
        public string? ColName          { get; set; }          // Колонка "Название"
        public string? ColCategory      { get; set; }          // Колонка "Категория"
        public string? ColPrice         { get; set; }          // Колонка "Цена"
        public string? ColSpecs         { get; set; }          // Колонка "Характеристики"
        public string? ColImageUrl      { get; set; }          // Колонка "Изображение"
        public string? ColProductUrl    { get; set; }          // Колонка "Ссылка на товар"
        public string? ColMerchantName  { get; set; }          // Магазин (если нет — берём Name)
        public string? DefaultMerchant  { get; set; }          // Название магазина по умолчанию

        // ── Категория по умолчанию (если нет колонки категорий) ───────
        public string? DefaultCategory  { get; set; }

        // ── Синхронизация ─────────────────────────────────────────────
        public bool       IsActive       { get; set; } = true;
        public SyncStatus LastSyncStatus { get; set; } = SyncStatus.Never;
        public DateTime?  LastSyncAt     { get; set; }
        public string?    LastSyncError  { get; set; }
        public int        SyncedCount    { get; set; }         // Сколько записей загружено

        // ── Авто-синхронизация ────────────────────────────────────────
        public bool AutoSync          { get; set; } = false;
        public int  AutoSyncIntervalH { get; set; } = 24;     // Раз в N часов

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
