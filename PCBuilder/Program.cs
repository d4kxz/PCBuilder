// ══════════════════════════════════════════════════════════════
//  Дополнительные NuGet пакеты для Identity:
//  dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
// ══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Services;
using PCBuilder.Services.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// ── База данных ──────────────────────────────────────────────
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=pcbuilder.db"));

// ── Identity ─────────────────────────────────────────────────
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit           = false;
    options.Password.RequireUppercase       = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength         = 6;
    options.SignIn.RequireConfirmedAccount  = false; // без подтверждения email
})
.AddEntityFrameworkStores<ApplicationContext>();

// Куда редиректить если не залогинен
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath  = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

// ── Остальные сервисы ─────────────────────────────────────────
builder.Services.AddScoped<Ozon>();
builder.Services.AddSingleton<DataSourceManager>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // ← ОБЯЗАТЕЛЬНО перед UseAuthorization
app.UseAuthorization();

app.MapRazorPages();

app.Run();
