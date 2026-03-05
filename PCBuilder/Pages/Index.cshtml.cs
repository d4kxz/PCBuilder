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

        public void OnGet()
        {
            // Метод для начальной загрузки страницы
        }

        public async Task<JsonResult> OnGetSearchComponentsAsync(string category, string term)
        {
            // 1. Поиск в БД с фильтрацией и защитой от регистра
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

            // 

            foreach (var comp in componentsFromDb)
            {
                // 2. Получаем живые данные из Ozon
                // Используем null-coalescing оператор для безопасности
                var (liveName, livePrice, liveImg) = await _ozonService.GetProductDataAsync(comp.OzonSku ?? "");

                // 3. Формируем предложения
                var allOffers = comp.Offers?
                .Select(o => (object)new
                {
                merchantName = o.MerchantName,
                price = o.Price
                 }).ToList() ?? new List<object>();
                // Добавляем Ozon, если данные получены успешно
                if (livePrice > 0)
                {
                    allOffers.Insert(0, new { merchantName = "Ozon", price = livePrice });
                }

                // Определяем базовую цену для расчета рейтинга
                decimal minPrice = allOffers.Any()
    ? allOffers.Min(o => ((dynamic)o).price)
    : 0;

                results.Add(new
                {
                    id = comp.Id,
                    name = (livePrice > 0 && !string.IsNullOrEmpty(liveName)) ? liveName : comp.Name,
                    price = minPrice,
                    image = (livePrice > 0 && !string.IsNullOrEmpty(liveImg)) ? liveImg : (comp.ImageUrl ?? "/img/no-image.png"),
                    specs = comp.Specs ?? "",
                    offers = allOffers,
                    powerScore = CalculatePowerScore(comp.Category, minPrice)
                });
            }

            return new JsonResult(results);
        }

        private int CalculatePowerScore(string category, decimal price)
        {
            if (price <= 0) return 0;

            // Логика оценки мощности на основе цены (условная)
            return category switch
            {
                "Процессор" => Math.Min((int)(price / 700), 100),
                "Видеокарта" => Math.Min((int)(price / 1500), 100),
                "Материнская Плата" => Math.Min((int)(price / 300), 100),
                "Оперативная память" => Math.Min((int)(price / 200), 100),
                _ => 50
            };
        }

        private string GetTierName(decimal price)
        {
            return price switch
            {
                > 80000 => "Enthusiast-Tier",
                > 40000 => "High-Tier",
                > 20000 => "Mid-Tier",
                _ => "Entry-Tier"
            };
        }
    }
}