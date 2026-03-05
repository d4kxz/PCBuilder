namespace PCBuilder.Models;

public class Component
{
    public int Id { get; set; }
    public string Category { get; set; } = "";
    public string Name { get; set; } = "";
    public string Specs { get; set; } = ""; // Добавили обратно
    public string? Socket { get; set; }     // Добавили обратно
    public string? OzonSku { get; set; }
    public string ImageUrl { get; set; } = "";

    // Список предложений от магазинов
    public List<ProductOffer> Offers { get; set; } = new();
}