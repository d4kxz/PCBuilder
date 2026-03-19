using System.ComponentModel.DataAnnotations;

namespace PCBuilder.Models
{
    /// <summary>
    /// Сохранённая сборка пользователя
    /// </summary>
    public class SavedBuild
    {
        public int    Id        { get; set; }
        public string UserId    { get; set; } = "";   // FK → AppUser.Id

        [Required, MaxLength(100)]
        public string Name      { get; set; } = "";   // Название сборки

        public decimal TotalPrice { get; set; }

        // JSON-строка с полным составом сборки
        // Формат: { "Процессор": { id, name, selectedPrice, merchantName, productUrl, ... }, ... }
        public string BuildJson { get; set; } = "{}";

        // URL картинки корпуса (для карточки в профиле)
        public string? CaseImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
