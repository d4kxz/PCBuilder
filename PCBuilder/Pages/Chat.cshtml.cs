using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PCBuilder.Data;
using PCBuilder.Services;

namespace PCBuilder.Pages
{
    [Authorize] // только авторизованные пользователи
    public class ChatModel : PageModel
    {
        private readonly IGigaChatService _gigaChatService;
        private readonly ApplicationContext _context;

        public ChatModel(IGigaChatService gigaChatService, ApplicationContext context)
        {
            _gigaChatService = gigaChatService;
            _context = context;
        }

        public void OnGet() { }

        public async Task<JsonResult> OnPostSendAsync()
        {
            var message = Request.Form["message"].ToString();

            if (string.IsNullOrWhiteSpace(message))
                return new JsonResult(new { ok = false, error = "Сообщение не может быть пустым" });

            try
            {
                var response = await _gigaChatService.GenerateBuildAsync(message.Trim());

                if (response == null)
                    return new JsonResult(new { ok = false, error = "Не удалось получить ответ от нейросети" });

                return new JsonResult(new
                {
                    ok = true,
                    response = new
                    {
                        buildName = response.buildName,
                        totalPrice = response.totalPrice,
                        reasoning = response.reasoning,
                        components = response.components.Select(c => new
                        {
                            id = c.id,
                            category = c.category,
                            name = c.name,
                            specs = c.specs,
                            price = c.price,
                            imageUrl = c.imageUrl,
                            socket = c.socket,
                            ramType = c.ramType,
                            tdp = c.tdp,
                            powerScore = c.powerScore,
                            psuWatts = c.psuWatts,
                            inStock = c.inStock
                        })
                    },
                    hasBuild = response.components.Count > 0
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { ok = false, error = ex.Message });
            }
        }
    }
}