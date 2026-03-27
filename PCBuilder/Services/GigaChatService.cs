using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PCBuilder.Data;
using PCBuilder.Models;

namespace PCBuilder.Services
{
    public interface IGigaChatService
    {
        Task<string> GetTokenAsync();
        Task<ChatResponse?> GenerateBuildAsync(string prompt);
    }

    public class GigaChatService : IGigaChatService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ApplicationContext _context;
        private string? _token;
        private DateTime _tokenExpiry;

        private const string AuthUri = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
        private const string GenApiUri = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";
        private const string Model = "GigaChat";

        public GigaChatService(HttpClient httpClient, IConfiguration config, ApplicationContext context)
        {
            _httpClient = httpClient;
            _config = config;
            _context = context;
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // ── Авторизация ────────────────────────────────────────────────────────────
        public async Task<string> GetTokenAsync()
        {
            var now = DateTime.UtcNow;
            if (_token != null && _tokenExpiry > now.AddMinutes(1))
                return _token!;

            var authKey = _config["GigaChat:AuthorizationKey"] ?? "";

            var request = new HttpRequestMessage(HttpMethod.Post, AuthUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authKey);
            request.Headers.Add("RqUID", Guid.NewGuid().ToString());
            request.Content = new StringContent(
                "scope=GIGACHAT_API_PERS",
                Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Ошибка авторизации GigaChat: {response.StatusCode}. {responseString}");

            using var jsonDoc = JsonDocument.Parse(responseString);
            _token = jsonDoc.RootElement.GetProperty("access_token").GetString();

            if (jsonDoc.RootElement.TryGetProperty("expires_at", out var expiresAt))
                _tokenExpiry = DateTimeOffset.FromUnixTimeMilliseconds(expiresAt.GetInt64()).UtcDateTime;
            else
                _tokenExpiry = now.AddMinutes(25);

            return _token!;
        }

        // ── Основной метод ─────────────────────────────────────────────────────────
        public async Task<ChatResponse?> GenerateBuildAsync(string prompt)
        {
            // 1. Загружаем каталог из БД — только товары в наличии
            var allComponents = await _context.Components
                .Where(c => c.InStock)
                .OrderBy(c => c.Category)
                .ToListAsync();

            // SQLite не поддерживает сортировку decimal в SQL — сортируем на клиенте
            allComponents = allComponents
                .OrderBy(c => c.Category)
                .ThenBy(c => (double)c.Price)
                .ToList();

            if (!allComponents.Any())
                throw new Exception("Каталог пуст. Добавьте компоненты в базу данных.");

            // 2. Формируем текстовый каталог для промпта
            var catalogText = new StringBuilder();
            foreach (var grp in allComponents.GroupBy(c => c.Category))
            {
                catalogText.AppendLine($"\n### {grp.Key}");
                foreach (var item in grp)
                {
                    var line = $"- ID:{item.Id} | {item.Name} | {item.Specs} | Цена: {item.Price:F0} руб.";
                    if (!string.IsNullOrEmpty(item.Socket)) line += $" | Сокет: {item.Socket}";
                    if (!string.IsNullOrEmpty(item.RamType)) line += $" | RAM: {item.RamType}";
                    if (item.PsuWatts > 0) line += $" | Мощность БП: {item.PsuWatts}W";
                    if (item.TDP > 0) line += $" | TDP: {item.TDP}W";
                    catalogText.AppendLine(line);
                }
            }

            // 3. Системный промпт с каталогом
            var systemPrompt = $@"Ты — AI-консультант магазина PC CORE по сборке компьютеров.

КАТАЛОГ МАГАЗИНА (выбирай ТОЛЬКО из него, ничего не придумывай):
{catalogText}

ПРАВИЛА — обязательны для исполнения:
1. Выбирай компоненты ТОЛЬКО из каталога выше по их ID. Запрещено использовать модели не из каталога.
2. В ответе используй точные ID, названия и цены из каталога — не изменяй их.
3. Проверяй совместимость перед ответом:
   - Сокет CPU == Сокет материнской платы (AM5/LGA1700)
   - Тип RAM материнской платы == Тип RAM модуля (DDR4/DDR5)
   - Суммарный TDP всех компонентов < 80% мощности выбранного БП
4.Твой ответ ДОЛЖЕН содержать ровно по одному объекту для каждой из следующих категорий: 
[Процессор, Материнская плата, Видеокарта, Оперативная память, Накопитель SSD, Блок питания, Корпус, Охлаждение]. 
Если категория отсутствует — сборка считается неверной.
5. Ответ — строго JSON без текста до и после, без markdown.

Формат JSON:
{{
  ""buildName"": ""Краткое название сборки"",
  ""components"": [
    {{""id"": 1, ""category"": ""Процессор"", ""name"": ""...(из каталога)"", ""specs"": ""...(из каталога)"", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}},
    {{""id"": 10, ""category"": ""Материнская плата"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}},
    {{""id"": 20, ""category"": ""Видеокарта"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}},
    {{""id"": 30, ""category"": ""Оперативная память"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}},
    {{""id"": 40, ""category"": ""Накопитель SSD"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}},
    {{""id"": 50, ""category"": ""Блок питания"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 750}},
    {{""id"": 60, ""category"": ""Корпус"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}},
    {{""id"": 70, ""category"": ""Охлаждение"", ""name"": ""..."", ""specs"": ""..."", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}}
  ],
  ""totalPrice"": 0,
  ""reasoning"": ""Обоснование: совместимость, бюджет, цели""
}}";

            // 4. Запрос к GigaChat
            var token = await GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var payload = JsonSerializer.Serialize(new
            {
                model = Model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user",   content = prompt }
                },
                temperature = 0.2, // низкая температура = меньше фантазий
                max_tokens = 2048
            });

            var apiRequest = new HttpRequestMessage(HttpMethod.Post, GenApiUri)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(apiRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Ошибка GigaChat API: {response.StatusCode}. {responseString}");

            using var jsonDoc = JsonDocument.Parse(responseString);
            var choices = jsonDoc.RootElement.GetProperty("choices");
            if (choices.GetArrayLength() == 0)
                throw new Exception("GigaChat не вернул варианты ответа");

            var rawContent = choices[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "";

            // Убираем markdown-обёртку
            rawContent = rawContent.Trim();
            if (rawContent.StartsWith("```json")) rawContent = rawContent[7..];
            if (rawContent.StartsWith("```")) rawContent = rawContent[3..];
            if (rawContent.EndsWith("```")) rawContent = rawContent[..^3];
            rawContent = rawContent.Trim();

            // 5. Парсим ответ
            ChatResponse parsed;
            try
            {
                var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                parsed = JsonSerializer.Deserialize<ChatResponse>(rawContent, opts)
                    ?? throw new Exception("Пустой ответ после десериализации");
            }
            catch (JsonException e)
            {
                throw new Exception($"GigaChat вернул невалидный JSON: {rawContent}", e);
            }

            // 6. ВЕРИФИКАЦИЯ — перезаписываем данные реальными значениями из БД
            //    Это финальная защита от галлюцинаций: даже если модель что-то исказила,
            //    пользователь увидит только реальные данные из нашей БД.
            var dbLookup = allComponents.ToDictionary(c => c.Id);

            var verifiedComponents = new List<ComponentProposal>();
            foreach (var comp in parsed.components)
            {
                if (comp.id > 0 && dbLookup.TryGetValue(comp.id, out var db))
                {
                    // Полная замена данными из БД
                    verifiedComponents.Add(new ComponentProposal
                    {
                        id = db.Id,
                        category = db.Category,
                        name = db.Name,
                        specs = db.Specs,
                        price = db.Price,
                        imageUrl = db.ImageUrl,
                        socket = db.Socket ?? "",
                        ramType = db.RamType ?? "",
                        tdp = db.TDP,
                        powerScore = db.PowerScore,
                        psuWatts = db.PsuWatts,
                        inStock = db.InStock
                    });
                }
                else
                {
                    // Модель указала несуществующий ID — логируем и пропускаем
                    // (в продакшне можно добавить ILogger)
                    Console.WriteLine($"[GigaChat] Предупреждение: компонент с ID={comp.id} не найден в БД, пропущен.");
                }
            }

            parsed.components = verifiedComponents;
            parsed.totalPrice = verifiedComponents.Sum(c => c.price); // пересчёт по реальным ценам

            return parsed;
        }
    }

    // ── DTO ────────────────────────────────────────────────────────────────────────

    public class ChatResponse
    {
        public string buildName { get; set; } = "";
        public List<ComponentProposal> components { get; set; } = new();
        public decimal totalPrice { get; set; }
        public string reasoning { get; set; } = "";
    }

    public class ComponentProposal
    {
        public int id { get; set; }
        public string category { get; set; } = "";
        public string? name { get; set; }
        public string? specs { get; set; }
        public decimal price { get; set; }
        public string? imageUrl { get; set; }
        public string? socket { get; set; }
        public string? ramType { get; set; }
        public int tdp { get; set; }
        public int powerScore { get; set; }
        public int psuWatts { get; set; }
        public bool inStock { get; set; } = true;
    }
}