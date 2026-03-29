using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Models;
using PCBuilder.Services;

namespace PCBuilder.Pages
{
    [Authorize]
    public class ChatModel : PageModel
    {
        private readonly IGigaChatService    _gigaChatService;
        private readonly ApplicationContext  _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatModel(
            IGigaChatService gigaChatService,
            ApplicationContext context,
            UserManager<IdentityUser> userManager)
        {
            _gigaChatService = gigaChatService;
            _context         = context;
            _userManager     = userManager;
        }

        // Список сессий для отображения в сайдбаре
        public List<ChatSession> Sessions { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return;

            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
                return;

            Sessions = await _context.ChatSessions
                .Where(s => s.UserId == user.Id)
                .OrderByDescending(s => s.UpdatedAt)
                .Take(50)
                .ToListAsync();
        }

        // ── Отправить сообщение и получить ответ AI ────────────────
        public async Task<JsonResult> OnPostSendAsync()
        {
            var message   = Request.Form["message"].ToString();
            var sessionId = int.TryParse(Request.Form["sessionId"], out var sid) ? sid : 0;

            if (string.IsNullOrWhiteSpace(message))
                return new JsonResult(new { ok = false, error = "Сообщение не может быть пустым" });

            // Проверяем аутентификацию
            if (!User.Identity?.IsAuthenticated ?? false)
                return new JsonResult(new { ok = false, error = "Вы не авторизованы. Пожалуйста, войдите в систему." });

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return new JsonResult(new { ok = false, error = "Не удалось получить данные пользователя. Пожалуйста, переавторизуйтесь." });

            try
            {
                // Найти или создать сессию
                ChatSession? session = null;

                if (sessionId > 0)
                    session = await _context.ChatSessions
                        .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == user.Id);

                if (session == null)
                {
                    // Новая сессия — заголовок из первых 60 символов сообщения
                    session = new ChatSession
                    {
                        UserId    = user.Id,
                        Title     = message.Length > 60 ? message[..60] + "…" : message,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.ChatSessions.Add(session);
                    await _context.SaveChangesAsync(); // нужен Id
                }

                // Сохраняем сообщение пользователя
                _context.ChatMessages.Add(new ChatMessage
                {
                    SessionId = session.Id,
                    Role      = "user",
                    Content   = message.Trim(),
                    CreatedAt = DateTime.UtcNow
                });

                // Запрос к GigaChat
                var response = await _gigaChatService.GenerateBuildAsync(message.Trim());

                if (response == null)
                    return new JsonResult(new { ok = false, error = "Не удалось получить ответ от нейросети" });

                // Сохраняем ответ AI
                _context.ChatMessages.Add(new ChatMessage
                {
                    SessionId = session.Id,
                    Role      = "ai",
                    Content   = response.reasoning ?? "",
                    BuildJson = System.Text.Json.JsonSerializer.Serialize(response),
                    CreatedAt = DateTime.UtcNow
                });

                session.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return new JsonResult(new
                {
                    ok        = true,
                    sessionId = session.Id,
                    response  = new
                    {
                        buildName  = response.buildName,
                        totalPrice = response.totalPrice,
                        reasoning  = response.reasoning,
                        components = response.components.Select(c => new
                        {
                            id         = c.id,
                            category   = c.category,
                            name       = c.name,
                            specs      = c.specs,
                            price      = c.price,
                            imageUrl   = c.imageUrl,
                            socket     = c.socket,
                            ramType    = c.ramType,
                            tdp        = c.tdp,
                            powerScore = c.powerScore,
                            psuWatts   = c.psuWatts,
                            inStock    = c.inStock
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

        // ── Загрузить историю сессии ───────────────────────────────
        public async Task<JsonResult> OnGetSessionAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return new JsonResult(new { ok = false });

            var session = await _context.ChatSessions
                .Include(s => s.Messages.OrderBy(m => m.CreatedAt))
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (session == null)
                return new JsonResult(new { ok = false, error = "Сессия не найдена" });

            return new JsonResult(new
            {
                ok      = true,
                session = new
                {
                    id       = session.Id,
                    title    = session.Title,
                    messages = session.Messages.Select(m => new
                    {
                        role      = m.Role,
                        content   = m.Content,
                        buildJson = m.BuildJson,
                        createdAt = m.CreatedAt
                    })
                }
            });
        }

        // ── Удалить сессию ─────────────────────────────────────────
        public async Task<JsonResult> OnPostDeleteSessionAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return new JsonResult(new { ok = false });

            var session = await _context.ChatSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (session != null)
            {
                _context.ChatSessions.Remove(session);
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new { ok = true });
        }

        // ── Переименовать сессию ───────────────────────────────────
        public async Task<JsonResult> OnPostRenameSessionAsync(int id, string title)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return new JsonResult(new { ok = false });

            var session = await _context.ChatSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (session != null && !string.IsNullOrWhiteSpace(title))
            {
                session.Title = title.Trim()[..Math.Min(title.Trim().Length, 200)];
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new { ok = true });
        }
    }
}
