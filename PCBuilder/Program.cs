using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// ── База данных ──────────────────────────────────────────────
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=pcbuilder.db"));

// ── Identity ─────────────────────────────────────────────────
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

// ── GigaChat API ─────────────────────────────────────────────
builder.Services.AddHttpClient<IGigaChatService, GigaChatService>()
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        // Sber API использует самоподписанный сертификат
        // Для localhost разработки отключаем проверку SSL
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
    });

// ── Остальные сервисы ─────────────────────────────────────────
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();