using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Models;
using PCBuilder.Services.API;

namespace PCBuilder.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly Ozon _ozonService;

        public IndexModel(ApplicationContext context, Ozon ozonService)
        {
            _context = context;
            _ozonService = ozonService;
        }

        public void OnGet() { }

        public async Task<JsonResult> OnGetSearchComponentsAsync(string category, string term)
        {
            var query = _context.Components
                .Include(c => c.Offers)
                .Where(c => c.Category == category);

            if (!string.IsNullOrEmpty(term))
            {
                var lowerTerm = term.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(lowerTerm));
            }

            var componentsFromDb = await query.ToListAsync();
            var results = new List<object>();

            foreach (var comp in componentsFromDb)
            {
                var (liveName, livePrice, liveImg) = await _ozonService.GetProductDataAsync(comp.OzonSku ?? "");

                var allOffers = comp.Offers?
                    .Select(o => (object)new
                    {
                        merchantName = o.MerchantName,
                        price = o.Price
                    }).ToList() ?? new List<object>();

                if (livePrice > 0)
                    allOffers.Insert(0, new { merchantName = "Ozon", price = livePrice });

                decimal minPrice = allOffers.Any()
                    ? allOffers.Min(o => ((dynamic)o).price)
                    : 0;

                // TDP — берём из Specs (парсим) или из заранее заданных значений по категории
                int tdp = EstimateTdp(comp.Category, minPrice);
                int psuWatts = EstimatePsuWatts(comp.Category, minPrice);

                results.Add(new
                {
                    id = comp.Id,
                    name = (livePrice > 0 && !string.IsNullOrEmpty(liveName)) ? liveName : comp.Name,
                    price = minPrice,
                    image = (livePrice > 0 && !string.IsNullOrEmpty(liveImg)) ? liveImg : (comp.ImageUrl ?? "/img/no-image.png"),
                    specs = comp.Specs ?? "",
                    offers = allOffers,
                    powerScore = CalculatePowerScore(comp.Category, minPrice),
                    tdp = tdp,
                    psuWatts = psuWatts
                });
            }

            return new JsonResult(results);
        }

        // Оценка мощности на основе цены (условная шкала)
        private int CalculatePowerScore(string category, decimal price)
        {
            if (price <= 0) return 0;
            return category switch
            {
                "Процессор" => Math.Min((int)(price / 700), 100),
                "Видеокарта" => Math.Min((int)(price / 1800), 100),
                _ => 50
            };
        }

        // Приблизительный TDP компонента (для нагрузки БП)
        private int EstimateTdp(string category, decimal price)
        {
            return category switch
            {
                "Процессор" => price > 50000 ? 125 : price > 25000 ? 105 : 65,
                "Видеокарта" => price > 150000 ? 450 : price > 60000 ? 220 : price > 40000 ? 200 : 150,
                "Материнская плата" => 50,
                "Оперативная память" => 10,
                "Накопитель SSD" => 8,
                "Охлаждение" => 5,
                "Корпус" => 0,
                "Блок питания" => 0,  // сам БП не потребляет — это источник
                _ => 20
            };
        }

        // Мощность БП (только для категории "Блок питания")
        private int EstimatePsuWatts(string category, decimal price)
        {
            if (category != "Блок питания") return 0;
            return price switch
            {
                > 23000 => 1000,
                > 18000 => 850,
                > 12000 => 750,
                > 9000 => 650,
                _ => 550
            };
        }
    }
}