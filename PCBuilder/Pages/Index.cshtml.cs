using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;

namespace PCBuilder.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<JsonResult> OnGetSearchComponentsAsync(string category, string term)
        {
            var query = _context.Components
                .Where(c => c.Category == category);

            if (!string.IsNullOrEmpty(term))
            {
                var lower = term.ToLower();
                // В SQLite .Contains() обычно транслируется в LIKE, который регистронезависим
                query = query.Where(c => c.Name.ToLower().Contains(lower)
                                      || c.Specs.ToLower().Contains(lower));
            }

            // ИСПРАВЛЕНИЕ: 
            // 1. Сначала загружаем данные из БД (ToListAsync)
            var componentsFromDb = await query.ToListAsync();

            // 2. Теперь сортируем в памяти и формируем анонимный объект
            var result = componentsFromDb
                .OrderBy(c => c.Price)
                .Select(c => new
                {
                    id = c.Id,
                    name = c.Name,
                    specs = c.Specs,
                    price = c.Price,
                    image = c.ImageUrl,
                    inStock = c.InStock,
                    socket = c.Socket ?? "",
                    ramType = c.RamType ?? "",
                    powerScore = c.PowerScore,
                    tdp = c.TDP,
                    psuWatts = c.PsuWatts
                });

            return new JsonResult(result);
        }
    }
}