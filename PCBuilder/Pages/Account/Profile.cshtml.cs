using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Models;

namespace PCBuilder.Pages.Account
{
    [Authorize] // только для залогиненных
    [IgnoreAntiforgeryToken]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationContext  _db;
        private readonly UserManager<IdentityUser>  _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileModel(ApplicationContext db, UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            _db           = db;
            _userManager  = um;
            _signInManager = sm;
        }

        public string UserName     { get; set; } = "";
        public string Email        { get; set; } = "";
        public string? UpdateMessage { get; set; }
        public List<SavedBuild> Builds { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            UserName = user.UserName ?? "";
            Email    = user.Email    ?? "";
            Builds   = await _db.SavedBuilds
                .Where(b => b.UserId == user.Id)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // ── Сохранить сборку (вызывается из Index.cshtml) ────────────
        public async Task<JsonResult> OnPostSaveBuildAsync([FromBody] SaveBuildDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return new JsonResult(new { ok = false, error = "Не авторизован" });

            if (string.IsNullOrWhiteSpace(dto.Name))
                return new JsonResult(new { ok = false, error = "Введите название сборки" });

            var build = new SavedBuild
            {
                UserId       = user.Id,
                Name         = dto.Name.Trim(),
                TotalPrice   = dto.TotalPrice,
                BuildJson    = dto.BuildJson ?? "{}",
                CaseImageUrl = dto.CaseImageUrl,
                CreatedAt    = DateTime.UtcNow,
                UpdatedAt    = DateTime.UtcNow,
            };

            _db.SavedBuilds.Add(build);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ok = true, id = build.Id });
        }

        // ── Удалить сборку ────────────────────────────────────────────
        public async Task<IActionResult> OnPostDeleteBuildAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var build = await _db.SavedBuilds
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == user.Id);

            if (build != null)
            {
                _db.SavedBuilds.Remove(build);
                await _db.SaveChangesAsync();
            }
            return new JsonResult(new { ok = true });
        }

        // ── Обновить профиль ──────────────────────────────────────────
        public async Task<IActionResult> OnPostUpdateProfileAsync(string newUserName, string? newPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage();

            if (!string.IsNullOrWhiteSpace(newUserName) && newUserName != user.UserName)
            {
                await _userManager.SetUserNameAsync(user, newUserName);
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var token  = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, newPassword);
            }

            await _signInManager.RefreshSignInAsync(user);
            UpdateMessage = "Профиль обновлён";

            // Перезагружаем данные
            await OnGetAsync();
            return Page();
        }

        // ── Выйти ─────────────────────────────────────────────────────
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }

    public class SaveBuildDto
    {
        public string  Name         { get; set; } = "";
        public decimal TotalPrice   { get; set; }
        public string? BuildJson    { get; set; }
        public string? CaseImageUrl { get; set; }
    }
}
