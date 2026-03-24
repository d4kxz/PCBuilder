namespace PCBuilder.Models;

/// <summary>
/// Комплектующее в каталоге собственного магазина
/// </summary>
public class Component
{
    public int Id { get; set; }
    public string Category { get; set; } = "";   // "Процессор", "Видеокарта", ...
    public string Name { get; set; } = "";
    public string Specs { get; set; } = "";   // краткие характеристики для отображения
    public string? Socket { get; set; }         // AM5 / LGA1700 / — (для проверки совместимости)
    public string? RamType { get; set; }         // DDR4 / DDR5   (для материнских плат и ОЗУ)
    public int TDP { get; set; }         // Вт — потребление (0 для корпуса/БП-источника)
    public int PsuWatts { get; set; }         // Вт — мощность БП (только для "Блок питания")
    public int PowerScore { get; set; }       // 0–100, условный балл производительности
    public string ImageUrl { get; set; } = "";
    public decimal Price { get; set; }         // цена в рублях
    public bool InStock { get; set; } = true; // в наличии
}