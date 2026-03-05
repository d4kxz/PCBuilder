namespace PCBuilder.Services.API
{
    public class Ozon
    {
        private readonly Dictionary<string, (string Name, decimal Price, string Image)> _mockOzonDatabase = new()
        {
            { "2080175880", ("Palit GeForce RTX 5060 Ti Dual 8 GB", 54990m, "https://ir.ozone.ru/s3/multimedia-1/6884562541.jpg") },
            { "7600", ("AMD Ryzen 5 7600X OEM", 21500m, "https://ir.ozone.ru/s3/multimedia-1/6453213456.jpg") }
        };

        public async Task<(string Name, decimal Price, string Image)> GetProductDataAsync(string sku)
        {
            await Task.Delay(300); // Имитация задержки сети

            if (_mockOzonDatabase.TryGetValue(sku, out var product))
            {
                return product;
            }

            return ("Товар не найден", 0, "");
        }
    }
}