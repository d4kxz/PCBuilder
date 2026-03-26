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
        Task<ChatResponse?> GenerateBuildAsync(string prompt, List<Component>? selectedComponents = null);
        Task<bool> SaveGeneratedComponentAsync(ComponentProposal proposal);
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
            {
                var ms = expiresAt.GetInt64();
                _tokenExpiry = DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime;
            }
            else
            {
                _tokenExpiry = now.AddMinutes(25);
            }

            return _token!;
        }

        public async Task<ChatResponse?> GenerateBuildAsync(string prompt, List<Component>? selectedComponents = null)
        {
            var token = await GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Системный промпт — используем verbatim string, кавычки экранируем через ""
            var systemPrompt = @"Ты — AI-помощник по сборке компьютеров в магазине PC CORE.

Твоя задача:
1. Проанализировать запрос пользователя (бюджет, цели: игры, работа, монтаж)
2. Предложить оптимальную сборку ПК
3. Учитывать совместимость (сокет процессора и материнской платы, тип DDR памяти, мощность БП)
4. Уложиться в бюджет

ВАЖНО: Ответ СТРОГО в формате JSON, без текста до или после JSON.

Формат ответа:
{
  ""buildName"": ""Название сборки"",
  ""components"": [
    {""category"": ""Процессор"", ""name"": ""Название модели"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": ""AM5"", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0},
    {""category"": ""Материнская плата"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": ""AM5"", ""ramType"": ""DDR5"", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0},
    {""category"": ""Видеокарта"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0},
    {""category"": ""Оперативная память"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": """", ""ramType"": ""DDR5"", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0},
    {""category"": ""Накопитель SSD"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0},
    {""category"": ""Блок питания"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 750},
    {""category"": ""Корпус"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": """", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0},
    {""category"": ""Охлаждение"", ""name"": ""Название"", ""specs"": ""Характеристики"", ""price"": 0, ""socket"": ""AM5,LGA1700"", ""ramType"": """", ""tdp"": 0, ""powerScore"": 0, ""psuWatts"": 0}
  ],
  ""totalPrice"": 0,
  ""reasoning"": ""Краткое обоснование выбора компонентов""
}

Категории СТРОГО: Процессор, Материнская плата, Видеокарта, Оперативная память, Накопитель SSD, Блок питания, Корпус, Охлаждение.
Цены указывай в рублях, реалистичные на 2025 год.";

            var messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = prompt }
            };

            var payload = JsonSerializer.Serialize(new
            {
                model = Model,
                messages = messages,
                temperature = 0.7,
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
                throw new Exception("GigaChat не вернул ответ");

            var rawContent = choices[0].GetProperty("message").GetProperty("content").GetString() ?? "";

            // Убираем markdown обёртку если есть
            rawContent = rawContent.Trim();
            if (rawContent.StartsWith("```json"))
                rawContent = rawContent.Substring(7);
            if (rawContent.StartsWith("```"))
                rawContent = rawContent.Substring(3);
            if (rawContent.EndsWith("```"))
                rawContent = rawContent.Substring(0, rawContent.Length - 3);
            rawContent = rawContent.Trim();

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<ChatResponse>(rawContent, options);
            }
            catch (JsonException e)
            {
                throw new Exception($"Ошибка парсинга ответа GigaChat. Ответ: {rawContent}", e);
            }
        }

        public async Task<bool> SaveGeneratedComponentAsync(ComponentProposal proposal)
        {
            var exists = await _context.Components.AnyAsync(c =>
                c.Name.ToLower() == proposal.name!.ToLower() &&
                c.Category == proposal.category);

            if (exists)
                return false;

            var newComp = new Component
            {
                Category = proposal.category,
                Name = proposal.name ?? "Unknown",
                Specs = proposal.specs ?? "",
                Price = proposal.price,
                ImageUrl = proposal.imageUrl ?? "",
                Socket = proposal.socket ?? "",
                RamType = proposal.ramType ?? "",
                TDP = proposal.tdp,
                PowerScore = proposal.powerScore,
                PsuWatts = proposal.psuWatts,
                InStock = true
            };

            _context.Components.Add(newComp);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // ── DTO классы ─────────────────────────────────────────────

    public class ChatResponse
    {
        public string buildName { get; set; } = "";
        public List<ComponentProposal> components { get; set; } = new();
        public decimal totalPrice { get; set; }
        public string reasoning { get; set; } = "";
    }

    public class ComponentProposal
    {
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
    }
}