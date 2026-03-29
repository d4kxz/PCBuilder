using System.ComponentModel.DataAnnotations;

namespace PCBuilder.Models
{
    /// <summary>
    /// Диалог пользователя с AI
    /// </summary>
    public class ChatSession
    {
        public int    Id        { get; set; }
        public string UserId    { get; set; } = "";

        [MaxLength(200)]
        public string Title     { get; set; } = "Новый диалог"; // берём из первого сообщения
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<ChatMessage> Messages { get; set; } = new();
    }

    /// <summary>
    /// Одно сообщение в диалоге
    /// </summary>
    public class ChatMessage
    {
        public int    Id        { get; set; }
        public int    SessionId { get; set; }

        // "user" или "ai"
        [MaxLength(10)]
        public string Role      { get; set; } = "user";

        // Текст сообщения пользователя ИЛИ JSON сборки от AI
        public string Content   { get; set; } = "";

        // Если AI вернул сборку — храним её JSON отдельно для удобства
        public string? BuildJson { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ChatSession Session { get; set; } = null!;
    }
}
