namespace PCBuilder.Models
{
    public class ProductOffer
    {
        public int Id { get; set; }
        public string MerchantName { get; set; } = ""; // "OZON", "Citilink", "DNS"
        public decimal Price { get; set; }
        public string? ProductUrl { get; set; }

        public int ComponentId { get; set; }
        public Component Component { get; set; } = null!;
    }
}
