using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PCBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddChatTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "GPUs");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Specs = table.Column<string>(type: "TEXT", nullable: false),
                    Socket = table.Column<string>(type: "TEXT", nullable: true),
                    RamType = table.Column<string>(type: "TEXT", nullable: true),
                    TDP = table.Column<int>(type: "INTEGER", nullable: false),
                    PsuWatts = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    InStock = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedBuilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuildJson = table.Column<string>(type: "TEXT", nullable: false),
                    CaseImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedBuilds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    BuildJson = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ChatSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Category", "ImageUrl", "InStock", "Name", "PowerScore", "Price", "PsuWatts", "RamType", "Socket", "Specs", "TDP" },
                values: new object[,]
                {
                    { 1, "Процессор", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Ryzen_5_7600X_top.jpg/320px-AMD_Ryzen_5_7600X_top.jpg", true, "AMD Ryzen 5 7600X", 52, 22990m, 0, "DDR5", "AM5", "6 ядер / 12 потоков, 4.7–5.3 GHz, TDP 105W", 105 },
                    { 2, "Процессор", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Ryzen_5_7600X_top.jpg/320px-AMD_Ryzen_5_7600X_top.jpg", true, "AMD Ryzen 7 7700X", 68, 33990m, 0, "DDR5", "AM5", "8 ядер / 16 потоков, 4.5–5.4 GHz, TDP 105W", 105 },
                    { 3, "Процессор", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Ryzen_5_7600X_top.jpg/320px-AMD_Ryzen_5_7600X_top.jpg", true, "AMD Ryzen 9 7950X", 90, 59990m, 0, "DDR5", "AM5", "16 ядер / 32 потока, 4.5–5.7 GHz, TDP 170W", 170 },
                    { 4, "Процессор", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Intel_i5-13600K_top.jpg/320px-Intel_i5-13600K_top.jpg", true, "Intel Core i5-13600K", 60, 27990m, 0, "DDR5", "LGA1700", "14 ядер / 20 потоков, 3.5–5.1 GHz, TDP 125W", 125 },
                    { 5, "Процессор", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Intel_i5-13600K_top.jpg/320px-Intel_i5-13600K_top.jpg", true, "Intel Core i7-13700K", 75, 39990m, 0, "DDR5", "LGA1700", "16 ядер / 24 потока, 3.4–5.4 GHz, TDP 125W", 125 },
                    { 6, "Процессор", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Intel_i5-13600K_top.jpg/320px-Intel_i5-13600K_top.jpg", false, "Intel Core i9-14900K", 95, 64990m, 0, "DDR5", "LGA1700", "24 ядра / 32 потока, 3.2–6.0 GHz, TDP 125W", 125 },
                    { 10, "Материнская плата", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg", true, "ASUS ROG STRIX B650-A Gaming WiFi", 0, 21990m, 0, "DDR5", "AM5", "AM5, DDR5, ATX, PCIe 5.0, Wi-Fi 6E", 50 },
                    { 11, "Материнская плата", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg", true, "MSI MAG B650 TOMAHAWK WIFI", 0, 18990m, 0, "DDR5", "AM5", "AM5, DDR5, ATX, Wi-Fi 6E, 2.5G LAN", 50 },
                    { 12, "Материнская плата", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg", true, "GIGABYTE Z790 AORUS Elite AX", 0, 24990m, 0, "DDR5", "LGA1700", "LGA1700, DDR5, ATX, Wi-Fi 6E, Thunderbolt 4", 50 },
                    { 13, "Материнская плата", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg", true, "ASRock B760M Pro RS", 0, 11990m, 0, "DDR5", "LGA1700", "LGA1700, DDR5, mATX, PCIe 4.0", 50 },
                    { 20, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg", true, "Palit RTX 5060 Ti 16GB", 62, 49990m, 0, null, null, "16GB GDDR7, 4608 ядер CUDA, TDP 180W", 180 },
                    { 21, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg", true, "ASUS TUF Gaming RTX 4070 Super", 75, 69990m, 0, null, null, "12GB GDDR6X, 7168 ядер CUDA, TDP 220W", 220 },
                    { 22, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg", true, "MSI Gaming X RX 7800 XT", 70, 58990m, 0, null, null, "16GB GDDR6, 3840 ядер, TDP 263W", 263 },
                    { 23, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg", false, "Gigabyte RTX 4090 Gaming OC", 99, 189990m, 0, null, null, "24GB GDDR6X, 16384 ядер CUDA, TDP 450W", 450 },
                    { 30, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/RAM_n.jpg/320px-RAM_n.jpg", true, "Kingston Fury Beast DDR5-5200 32GB", 0, 7990m, 0, "DDR5", null, "2×16GB, DDR5-5200, CL40", 10 },
                    { 31, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/RAM_n.jpg/320px-RAM_n.jpg", true, "G.Skill Trident Z5 RGB DDR5-6000 32GB", 0, 11990m, 0, "DDR5", null, "2×16GB, DDR5-6000, CL30, RGB подсветка", 10 },
                    { 32, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/RAM_n.jpg/320px-RAM_n.jpg", true, "Corsair Vengeance DDR5-5600 64GB", 0, 16990m, 0, "DDR5", null, "2×32GB, DDR5-5600, CL36", 12 },
                    { 40, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg", true, "Samsung 990 Pro 1TB NVMe", 0, 8990m, 0, null, null, "M.2 PCIe 4.0, чтение 7450 / запись 6900 MB/s", 8 },
                    { 41, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg", true, "WD Black SN850X 2TB NVMe", 0, 14990m, 0, null, null, "M.2 PCIe 4.0, чтение 7300 / запись 6600 MB/s", 8 },
                    { 42, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg", true, "Crucial T700 1TB NVMe PCIe 5.0", 0, 12490m, 0, null, null, "M.2 PCIe 5.0, чтение 12400 / запись 11800 MB/s", 9 },
                    { 43, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg", true, "Kingston A2000 500GB NVMe", 0, 3990m, 0, null, null, "M.2 PCIe 3.0, чтение 2200 / запись 2000 MB/s", 5 },
                    { 50, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg", true, "be quiet! Pure Power 12 M 750W", 0, 8990m, 750, null, null, "750W, 80+ Gold, ATX 3.0, модульный", 0 },
                    { 51, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg", true, "Seasonic Focus GX-650 650W", 0, 7490m, 650, null, null, "650W, 80+ Gold, ATX, полностью модульный", 0 },
                    { 52, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg", true, "ASUS ROG Thor 850P2 850W", 0, 22990m, 850, null, null, "850W, 80+ Platinum, ATX 3.0, OLED дисплей", 0 },
                    { 53, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg", true, "Corsair RM1000x 1000W", 0, 18990m, 1000, null, null, "1000W, 80+ Gold, ATX 3.0, полностью модульный", 0 },
                    { 60, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg", true, "NZXT H7 Flow", 0, 8490m, 0, null, null, "Mid-Tower ATX, закалённое стекло, 2×USB-A 3.2", 0 },
                    { 61, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg", true, "Fractal Design Meshify 2", 0, 11990m, 0, null, null, "Mid-Tower ATX, высокий поток воздуха, USB-C", 0 },
                    { 62, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg", true, "Lian Li PC-O11 Dynamic EVO", 0, 14990m, 0, null, null, "Mid-Tower ATX/E-ATX, двойная камера, стекло", 0 },
                    { 63, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg", true, "Deepcool CH510", 0, 5490m, 0, null, null, "Mid-Tower ATX, закалённое стекло, USB-C", 0 },
                    { 70, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Noctua NH-D15 chromax.black", 0, 9990m, 0, null, "AM5,LGA1700", "Башенный, 2×140mm вентилятора, до 250W TDP", 5 },
                    { 71, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "be quiet! Dark Rock Pro 4", 0, 8490m, 0, null, "AM5,LGA1700", "Башенный, 2×135mm вентилятора, до 250W TDP", 5 },
                    { 72, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Corsair iCUE H150i Elite LCD 360mm", 0, 19990m, 0, null, "AM5,LGA1700", "СВО 360mm, 3×120mm, LCD экран, RGB", 10 },
                    { 73, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "DeepCool AK620", 0, 4990m, 0, null, "AM5,LGA1700", "Башенный, 2×120mm вентилятора, до 260W TDP", 5 },
                    { 101, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_0", true, "Intel Core Ultra 9 285K", 98, 68900m, 0, "DDR5", "LGA1851", "24 ядра / 24 потока, до 5.7 GHz, TDP 125W", 125 },
                    { 102, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_1", true, "AMD Ryzen 9 9950X", 100, 74500m, 0, "DDR5", "AM5", "16 ядер / 32 потока, до 5.7 GHz, Zen 5, TDP 170W", 170 },
                    { 103, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_2", true, "AMD Ryzen 7 9800X3D", 99, 82000m, 0, "DDR5", "AM5", "8 ядер / 16 потока, 96MB L3 Cache, TDP 120W", 120 },
                    { 104, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_3", true, "Intel Core Ultra 7 265K", 88, 49900m, 0, "DDR5", "LGA1851", "20 ядер / 20 потоков, до 5.5 GHz, TDP 125W", 125 },
                    { 105, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_4", true, "AMD Ryzen 9 9900X", 92, 56000m, 0, "DDR5", "AM5", "12 ядер / 24 потока, до 5.6 GHz, TDP 120W", 120 },
                    { 106, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_5", true, "Intel Core Ultra 5 245K", 75, 36500m, 0, "DDR5", "LGA1851", "14 ядер / 14 потоков, до 5.2 GHz, TDP 125W", 125 },
                    { 107, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_6", true, "AMD Ryzen 7 9700X", 80, 41000m, 0, "DDR5", "AM5", "8 ядер / 16 потоков, до 5.5 GHz, TDP 65W", 65 },
                    { 108, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_7", true, "AMD Ryzen 5 9600X", 65, 31000m, 0, "DDR5", "AM5", "6 ядер / 12 потоков, до 5.4 GHz, TDP 65W", 65 },
                    { 109, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_8", true, "Intel Core i9-14900KS", 98, 78000m, 0, "DDR5", "LGA1700", "24 ядра / 32 потока, до 6.2 GHz, TDP 150W", 150 },
                    { 110, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_9", true, "AMD Ryzen 7 5700X3D", 70, 24500m, 0, "DDR4", "AM4", "8 ядер / 16 потоков, 96MB L3, TDP 105W", 105 },
                    { 111, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_10", true, "Intel Core i7-14700KF", 85, 44000m, 0, "DDR5", "LGA1700", "20 ядер / 28 потоков, до 5.6 GHz, TDP 125W", 125 },
                    { 112, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_11", true, "AMD Ryzen 5 7500F", 55, 16200m, 0, "DDR5", "AM5", "6 ядер / 12 потоков, до 5.0 GHz, без встр. видео, TDP 65W", 65 },
                    { 113, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_12", true, "AMD Ryzen 7 8700G", 60, 33000m, 0, "DDR5", "AM5", "8 ядер / 16 потоков, Radeon 780M Graphics, TDP 65W", 65 },
                    { 114, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_13", true, "Intel Core i5-13400F", 52, 19800m, 0, "DDR4", "LGA1700", "10 ядер / 16 потоков, до 4.6 GHz, TDP 65W", 65 },
                    { 115, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_14", true, "Intel Core i3-14100F", 35, 11500m, 0, "DDR4", "LGA1700", "4 ядра / 8 потоков, до 4.7 GHz, TDP 58W", 58 },
                    { 116, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_15", true, "AMD Ryzen 5 5600GT", 40, 13200m, 0, "DDR4", "AM4", "6 ядер / 12 потоков, Radeon Graphics, TDP 65W", 65 },
                    { 117, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_16", true, "AMD Ryzen 9 7900X3D", 94, 48500m, 0, "DDR5", "AM5", "12 ядер / 24 потока, 128MB L3 Cache, TDP 120W", 120 },
                    { 118, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_17", true, "Intel Core Ultra 5 245F", 68, 31000m, 0, "DDR5", "LGA1851", "14 ядер / 14 потоков, до 5.0 GHz, без видео, TDP 65W", 65 },
                    { 119, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_18", true, "AMD Ryzen 5 8500G", 45, 17900m, 0, "DDR5", "AM5", "6 ядер / 12 потоков, Radeon 740M, TDP 65W", 65 },
                    { 120, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_19", true, "AMD Ryzen 9 5950X", 85, 39500m, 0, "DDR4", "AM4", "16 ядер / 32 потока, до 4.9 GHz, TDP 105W", 105 },
                    { 121, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_20", true, "Intel Core i5-14400F", 55, 22500m, 0, "DDR5", "LGA1700", "10 ядер / 16 потоков, до 4.7 GHz, TDP 65W", 65 },
                    { 122, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_21", true, "AMD Ryzen 5 7600", 58, 21000m, 0, "DDR5", "AM5", "6 ядер / 12 потоков, до 5.1 GHz, TDP 65W", 65 },
                    { 123, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_22", true, "Intel Core i5-12600KF", 65, 18500m, 0, "DDR4", "LGA1700", "10 ядер / 16 потоков, до 4.9 GHz, TDP 125W", 125 },
                    { 124, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_23", true, "AMD Ryzen 5 5600X", 48, 14500m, 0, "DDR4", "AM4", "6 ядер / 12 потоков, до 4.6 GHz, TDP 65W", 65 },
                    { 125, "Процессор", "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_24", true, "Intel Core i7-12700F", 72, 28900m, 0, "DDR4", "LGA1700", "12 ядер / 20 потоков, до 4.9 GHz, TDP 65W", 65 },
                    { 201, "Материнская плата", "https://dlcdnrog.asus.com/rog/media/1728514113264.webp", true, "ASUS ROG Maximus Z890 Hero", 0, 72500m, 0, "DDR5", "LGA1851", "ATX, Intel Z890, 4xDDR5, 3xM.2 PCIe 5.0, Wi-Fi 7, Thunderbolt 4", 0 },
                    { 202, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/MEG-X870E-GODLIKE/godlike-box.png", true, "MSI MEG X870E GODLIKE", 0, 115000m, 0, "DDR5", "AM5", "E-ATX, AMD X870E, 24+2+1 VRM, PCIe 5.0, 10G LAN, Wi-Fi 7", 0 },
                    { 203, "Материнская плата", "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000", true, "Gigabyte X870 AORUS ELITE WIFI7", 0, 34000m, 0, "DDR5", "AM5", "ATX, AMD X870, PCIe 5.0 x16, 4xM.2, Wi-Fi 7, Ultra Durable", 0 },
                    { 204, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/MPG-Z890-CARBON-WIFI/carbon-box.png", true, "MSI MPG Z890 Carbon WiFi", 0, 48900m, 0, "DDR5", "LGA1851", "ATX, Intel Z890, 20 фаз питания, PCIe 5.0, 5G LAN", 0 },
                    { 205, "Материнская плата", "https://www.asrock.com/mb/photo/B850%20Steel%20Legend%20WiFi(L1).png", true, "ASRock B850 Steel Legend WiFi", 0, 24500m, 0, "DDR5", "AM5", "ATX, AMD B850, PCIe 5.0, Wi-Fi 7, 14+2+1 Phase", 0 },
                    { 206, "Материнская плата", "https://dlcdnrog.asus.com/rog/media/1728514113264.webp", true, "ASUS TUF Gaming Z890-PLUS WIFI", 0, 36000m, 0, "DDR5", "LGA1851", "ATX, Intel Z890, PCIe 5.0, 14+1 фаза VRM, Wi-Fi 7", 0 },
                    { 207, "Материнская плата", "https://dlcdnrog.asus.com/rog/media/1728514113264.webp", true, "ASUS ROG STRIX X870-F Gaming WiFi", 0, 42500m, 0, "DDR5", "AM5", "ATX, AMD X870, AI Overclocking, PCIe 5.0, DDR5-8000+", 0 },
                    { 208, "Материнская плата", "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000", true, "Gigabyte B860M GAMING X", 0, 16800m, 0, "DDR5", "LGA1851", "mATX, Intel B860, 2xM.2, PCIe 4.0 x16, 2.5G LAN", 0 },
                    { 209, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/MAG-B850-TOMAHAWK-WIFI/tomahawk-box.png", true, "MSI MAG B850 TOMAHAWK WIFI", 0, 28900m, 0, "DDR5", "AM5", "ATX, AMD B850, PCIe 5.0, Wi-Fi 7, расширенные радиаторы", 0 },
                    { 210, "Материнская плата", "https://www.asrock.com/mb/photo/Z890%20Taichi(L1).png", true, "ASRock Z890 Taichi", 0, 58000m, 0, "DDR5", "LGA1851", "E-ATX, Intel Z890, 27 фаз питания, USB4, Hi-Fi Audio", 0 },
                    { 211, "Материнская плата", "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000", true, "Gigabyte B850M DS3H", 0, 14200m, 0, "DDR5", "AM5", "mATX, AMD B850, 4xDDR5, 2xM.2 PCIe 4.0, бюджетный AM5", 0 },
                    { 212, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/PRO-B860-P-WIFI/pro-box.png", true, "MSI PRO B860-P WiFi", 0, 19500m, 0, "DDR5", "LGA1851", "ATX, Intel B860, 4xM.2, Wi-Fi 6E, 2.5G LAN", 0 },
                    { 213, "Материнская плата", "https://dlcdnrog.asus.com/rog/media/1728514113264.webp", true, "ASUS Prime X870-P WIFI", 0, 31500m, 0, "DDR5", "AM5", "ATX, AMD X870, PCIe 5.0, 3xM.2, серебристый дизайн", 0 },
                    { 214, "Материнская плата", "https://www.asrock.com/mb/photo/X870%20Steel%20Legend%20WiFi(L1).png", true, "ASRock X870 Steel Legend WiFi", 0, 33200m, 0, "DDR5", "AM5", "ATX, AMD X870, белый текстолит, PCIe 5.0, Wi-Fi 7", 0 },
                    { 215, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/MAG-B760-TOMAHAWK-WIFI/box.png", true, "MSI MAG B760 TOMAHAWK WIFI", 0, 21000m, 0, "DDR5", "LGA1700", "ATX, Intel B760, PCIe 5.0, Wi-Fi 6E, проверенная классика", 0 },
                    { 216, "Материнская плата", "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/25444/png/1000", true, "Gigabyte B550 AORUS ELITE V2", 0, 13500m, 0, "DDR4", "AM4", "ATX, AMD B550, PCIe 4.0, 12+2 фазы, лучший выбор для AM4", 0 },
                    { 217, "Материнская плата", "https://dlcdnrog.asus.com/rog/media/1728514113264.webp", true, "ASUS ROG Strix Z890-I Gaming WiFi", 0, 45500m, 0, "DDR5", "LGA1851", "Mini-ITX, Intel Z890, компактная, Wi-Fi 7, Thunderbolt 4", 0 },
                    { 218, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/PRO-B650M-A-WIFI/box.png", true, "MSI PRO B650M-A WIFI", 0, 15800m, 0, "DDR5", "AM5", "mATX, AMD B650, 2xM.2, Wi-Fi 6E, стабильный бюджет", 0 },
                    { 219, "Материнская плата", "https://www.asrock.com/mb/photo/B760M%20Pro4(L1).png", true, "ASRock B760M Pro4", 0, 11800m, 0, "DDR4", "LGA1700", "mATX, Intel B760, DDR4, 2xM.2 PCIe 4.0, доступный Intel", 0 },
                    { 220, "Материнская плата", "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000", true, "Gigabyte X870E AORUS MASTER", 0, 54000m, 0, "DDR5", "AM5", "ATX, AMD X870E, 16+2+2 фазы, USB4, Wi-Fi 7", 0 },
                    { 221, "Материнская плата", "https://www.asrock.com/mb/photo/B860%20Steel%20Legend%20WiFi(L1).png", true, "ASRock B860 Steel Legend WiFi", 0, 22000m, 0, "DDR5", "LGA1851", "ATX, Intel B860, PCIe 5.0, Wi-Fi 7, RGB", 0 },
                    { 222, "Материнская плата", "https://storage-asset.msi.com/global/picture/image/feature/mb/MAG-X870-TOMAHAWK-WIFI/tomahawk-box.png", true, "MSI MAG X870 TOMAHAWK WIFI", 0, 31000m, 0, "DDR5", "AM5", "ATX, AMD X870, PCIe 5.0, 5G LAN, 14+2+1 VRM", 0 },
                    { 223, "Материнская плата", "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000", true, "Gigabyte Z890 UD WIFI", 0, 26500m, 0, "DDR5", "LGA1851", "ATX, Intel Z890, бюджетный Z-чипсет, Wi-Fi 6E", 0 },
                    { 224, "Материнская плата", "https://dlcdnrog.asus.com/rog/media/1728514113264.webp", true, "ASUS ROG Crosshair X870E Hero", 0, 69000m, 0, "DDR5", "AM5", "ATX, AMD X870E, 18+2+2 VRM, USB4, флагман ROG", 0 },
                    { 225, "Материнская плата", "https://www.asrock.com/mb/photo/B650M-HDVM.2(L1).png", true, "ASRock B650M-HDV/M.2", 0, 12500m, 0, "DDR5", "AM5", "mATX, AMD B650, PCIe 5.0 M.2, лучший бюджетный выбор", 0 },
                    { 301, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg", true, "NVIDIA GeForce RTX 5090 Founders Edition", 100, 245000m, 1000, null, null, "32GB GDDR7, 512-bit, DLSS 4.5, Ray Tracing Gen 5", 450 },
                    { 302, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg", true, "NVIDIA GeForce RTX 5080", 85, 135000m, 850, null, null, "16GB GDDR7, 256-bit, Blackwell Architecture", 320 },
                    { 303, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "AMD Radeon RX 8900 XTX", 90, 118000m, 900, null, null, "24GB GDDR7, RDNA 4, FSR 4 AI", 350 },
                    { 304, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg", true, "NVIDIA GeForce RTX 5070 Ti", 75, 88000m, 750, null, null, "16GB GDDR7, 192-bit, Perfect for 1440p", 250 },
                    { 305, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg", true, "NVIDIA GeForce RTX 5070", 68, 69000m, 650, null, null, "12GB GDDR7, Low Power Blackwell", 200 },
                    { 306, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "AMD Radeon RX 8800 XT", 72, 62000m, 700, null, null, "16GB GDDR7, Ray Tracing Overhaul", 230 },
                    { 307, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg", true, "NVIDIA GeForce RTX 5060 Ti", 55, 52000m, 600, null, null, "12GB GDDR7, AI Workstation entry", 160 },
                    { 308, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg", true, "NVIDIA GeForce RTX 5060", 45, 38000m, 500, null, null, "8GB GDDR7, Ultra Efficient", 115 },
                    { 309, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "AMD Radeon RX 8700 XT", 58, 45000m, 600, null, null, "12GB GDDR6, Best Mid-range Value", 180 },
                    { 310, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg", true, "NVIDIA GeForce RTX 4090", 95, 195000m, 850, null, null, "24GB GDDR6X, Legend of 40-series", 450 },
                    { 311, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg/320px-Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg", true, "Intel Arc B580 (Battlemage)", 48, 32000m, 600, null, null, "12GB GDDR6, Xe2-HPG Architecture", 190 },
                    { 312, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg/320px-Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg", true, "Intel Arc B770 (Battlemage)", 60, 44500m, 700, null, null, "16GB GDDR6, High-end Intel GPU", 225 },
                    { 313, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg", true, "NVIDIA GeForce RTX 4070 Super", 65, 64000m, 650, null, null, "12GB GDDR6X, 192-bit, Efficiency King", 220 },
                    { 314, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "AMD Radeon RX 7900 GRE", 70, 59000m, 750, null, null, "16GB GDDR6, Golden Rabbit Edition", 260 },
                    { 315, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg", true, "NVIDIA GeForce RTX 4060", 40, 33000m, 500, null, null, "8GB GDDR6, Best for 1080p gaming", 115 },
                    { 316, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "AMD Radeon RX 7600 XT", 38, 36000m, 600, null, null, "16GB GDDR6, Large VRAM for budget", 190 },
                    { 317, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "ASUS ROG Strix RTX 5090 OC", 105, 289000m, 1200, null, null, "32GB GDDR7, Triple Fan, Max Overclock", 500 },
                    { 318, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg", true, "MSI Suprim X RTX 5080", 87, 152000m, 850, null, null, "16GB GDDR7, Premium Metal Design", 320 },
                    { 319, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg", true, "Gigabyte AORUS Master RTX 5070 Ti", 77, 98000m, 750, null, null, "16GB GDDR7, LCD Edge View", 250 },
                    { 320, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg", true, "Palit Dual RTX 5060", 44, 37500m, 500, null, null, "8GB GDDR7, Compact 2-slot", 115 },
                    { 321, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "Sapphire NITRO+ RX 8800 XT", 74, 68000m, 750, null, null, "16GB GDDR7, Best cooling for AMD", 250 },
                    { 322, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "PowerColor Hellhound RX 8700 XT", 59, 47000m, 650, null, null, "12GB GDDR6, Blue/Teal LED", 200 },
                    { 323, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg", true, "NVIDIA GeForce RTX 3060 12GB", 32, 28500m, 550, null, null, "12GB GDDR6, Budget Productivity King", 170 },
                    { 324, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg", true, "AMD Radeon RX 6600", 28, 21000m, 450, null, null, "8GB GDDR6, 1080p Entry Level", 132 },
                    { 325, "Видеокарта", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg", false, "NVIDIA GeForce RTX 5050", 35, 29500m, 400, null, null, "8GB GDDR7, DLSS 4.0 for everyone", 90 },
                    { 401, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "G.Skill Trident Z5 RGB 32GB (2x16GB) 6000MHz CL30", 85, 32500m, 0, "DDR5", null, "DDR5-6000, CL30-38-38-96, 1.35V, Intel XMP 3.0, RGB", 0 },
                    { 402, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Kingston FURY Beast Black 32GB (2x16GB) 5600MHz CL40", 75, 24800m, 0, "DDR5", null, "DDR5-5600, CL40-40-40, 1.25V, низкопрофильная", 0 },
                    { 403, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corsair_Vengeance_RGB_Pro_RAM.jpg/320px-Corsair_Vengeance_RGB_Pro_RAM.jpg", true, "Corsair Vengeance RGB 48GB (2x24GB) 7200MHz CL36", 92, 41200m, 0, "DDR5", null, "DDR5-7200, небинарный объем, iCUE поддержка", 0 },
                    { 404, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Patriot Viper Xtreme 5 32GB (2x16GB) 8000MHz", 100, 48500m, 0, "DDR5", null, "DDR5-8000, CL38, экстремальный разгон, CUDIMM ready", 0 },
                    { 405, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "Team Group T-Force Delta RGB 32GB (2x16GB) 6400MHz", 88, 29900m, 0, "DDR5", null, "DDR5-6400, CL32, белый радиатор, 120° RGB", 0 },
                    { 406, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "ADATA XPG Lancer Blade 32GB (2x16GB) 6000MHz", 84, 27600m, 0, "DDR5", null, "DDR5-6000, CL30, компактный радиатор (33мм)", 0 },
                    { 407, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Crucial Pro Overclocking 32GB (2x16GB) 6000MHz", 82, 26200m, 0, "DDR5", null, "DDR5-6000, CL36, поддержка Intel XMP и AMD EXPO", 0 },
                    { 408, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "G.Skill Flare X5 32GB (2x16GB) 6000MHz CL32", 85, 30400m, 0, "DDR5", null, "DDR5-6000, Оптимизировано для AMD AM5/Ryzen", 0 },
                    { 409, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Kingston FURY Renegade 32GB (2x16GB) 3600MHz CL16", 65, 16500m, 0, "DDR4", null, "DDR4-3600, CL16-20-20, высокая производительность", 0 },
                    { 410, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corsair_Vengeance_RGB_Pro_RAM.jpg/320px-Corsair_Vengeance_RGB_Pro_RAM.jpg", true, "Corsair Vengeance LPX 16GB (2x8GB) 3200MHz CL16", 50, 8400m, 0, "DDR4", null, "DDR4-3200, компактная, проверенная временем", 0 },
                    { 411, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corsair_Vengeance_RGB_Pro_RAM.jpg/320px-Corsair_Vengeance_RGB_Pro_RAM.jpg", true, "Corsair Dominator Titanium 64GB (2x32GB) 6600MHz", 95, 78000m, 0, "DDR5", null, "DDR5-6600, DHX охлаждение, сменные верхние планки", 0 },
                    { 412, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "Team Group T-Create Expert 64GB (2x32GB) 6000MHz", 90, 64500m, 0, "DDR5", null, "DDR5-6000, CL34, для рабочих станций и рендеринга", 0 },
                    { 413, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Patriot Viper Venom 16GB (2x8GB) 5200MHz", 60, 12900m, 0, "DDR5", null, "DDR5-5200, входной уровень в новое поколение", 0 },
                    { 414, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "ADATA XPG Spectrix D35G RGB 16GB (2x8GB) 3200MHz", 52, 9200m, 0, "DDR4", null, "DDR4-3200, CL16, стильный RGB", 0 },
                    { 415, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "G.Skill Trident Z5 Neo 32GB (2x16GB) 6400MHz", 89, 33800m, 0, "DDR5", null, "DDR5-6400, CL32, AMD EXPO профиль", 0 },
                    { 416, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Crucial 96GB (2x48GB) 5600MHz CL46", 88, 72000m, 0, "DDR5", null, "DDR5-5600, JEDEC стандарт, огромный объем", 0 },
                    { 417, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "Team Group T-Force Vulcan Z 32GB (2x16GB) 3200MHz", 60, 14200m, 0, "DDR4", null, "DDR4-3200, бюджетный выбор для рабочих ПК", 0 },
                    { 418, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Kingston FURY Renegade RGB 32GB (2x16GB) 7200MHz", 93, 43500m, 0, "DDR5", null, "DDR5-7200, CL38, премиальное охлаждение", 0 },
                    { 419, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Netac Shadow RGB 32GB (2x16GB) 6200MHz", 86, 28400m, 0, "DDR5", null, "DDR5-6200, CL32, агрессивный дизайн", 0 },
                    { 420, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "ASRock Phantom Gaming RGB 32GB (2x16GB) 6000MHz", 83, 29100m, 0, "DDR5", null, "DDR5-6000, брендированная память для плат ASRock", 0 },
                    { 421, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Lexar Ares RGB 32GB (2x16GB) 6400MHz", 89, 31500m, 0, "DDR5", null, "DDR5-6400, CL32, чипы SK Hynix A-Die", 0 },
                    { 422, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "G.Skill Ripjaws V 16GB (2x8GB) 3600MHz CL18", 55, 9800m, 0, "DDR4", null, "DDR4-3600, классика для Intel/AMD", 0 },
                    { 423, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", true, "Silicon Power Zenith RGB 32GB (2x16GB) 6000MHz", 81, 25900m, 0, "DDR5", null, "DDR5-6000, бюджетное решение с подсветкой", 0 },
                    { 424, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg", true, "GeIL Polaris RGB 32GB (2x16GB) 5200MHz", 70, 21400m, 0, "DDR5", null, "DDR5-5200, первый массовый DDR5 бренд", 0 },
                    { 425, "Оперативная память", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg", false, "Mushkin Redline ST 64GB (2x32GB) 6400MHz CL32", 94, 69800m, 0, "DDR5", null, "DDR5-6400, CL32, для энтузиастов", 0 },
                    { 501, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Samsung 990 Pro 2TB", 95, 28500m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7450 MB/s, Запись: 6900 MB/s", 0 },
                    { 502, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Crucial T705 1TB PCIe 5.0", 100, 34000m, 0, null, null, "M.2 NVMe, PCIe 5.0, Чтение: 13600 MB/s, Экстремальная скорость", 0 },
                    { 503, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Kingston FURY Renegade 1TB", 92, 14200m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7300 MB/s, С радиатором", 0 },
                    { 504, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Western Digital Black SN850X 2TB", 94, 26900m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7300 MB/s, Игровой SSD", 0 },
                    { 505, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "ADATA XPG GAMMIX S70 Blade 1TB", 89, 11500m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7400 MB/s, Совместим с PS5", 0 },
                    { 506, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Samsung 980 1TB", 70, 9800m, 0, null, null, "M.2 NVMe, PCIe 3.0, Безбуферный, Надежный выбор", 0 },
                    { 507, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "MSI Spatium M570 Pro 2TB", 99, 48000m, 0, null, null, "M.2 NVMe, PCIe 5.0, Чтение: 12400 MB/s, Frozr радиатор", 0 },
                    { 508, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Kingston NV3 2TB", 78, 16500m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 5000 MB/s, Бюджетный 2TB", 0 },
                    { 509, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Corsair MP700 PRO 1TB", 98, 29500m, 0, null, null, "M.2 NVMe, PCIe 5.0, Чтение: 11700 MB/s, Активное охлаждение", 0 },
                    { 510, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Team Group MP44L 1TB", 75, 8900m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 5000 MB/s, Тонкий радиатор", 0 },
                    { 511, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Lexar NM790 4TB", 96, 42000m, 0, null, null, "M.2 NVMe, PCIe 4.0, Огромный объем, Чтение: 7400 MB/s", 0 },
                    { 512, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Netac NV7000 1TB", 85, 10800m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7200 MB/s, Народный топ", 0 },
                    { 513, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Seagate FireCuda 530 1TB", 93, 15900m, 0, null, null, "M.2 NVMe, PCIe 4.0, Ресурс 1275 TBW", 0 },
                    { 514, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Crucial P3 Plus 1TB", 72, 8200m, 0, null, null, "M.2 NVMe, PCIe 4.0, Доступная скорость 4800 MB/s", 0 },
                    { 515, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Samsung 870 EVO 1TB", 40, 11200m, 0, null, null, "SATA III, 2.5\", Классика для хранения данных", 0 },
                    { 516, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Gigabyte AORUS Gen5 12000 2TB", 99, 45500m, 0, null, null, "M.2 NVMe, PCIe 5.0, Чтение: 12400 MB/s, Массивный радиатор", 0 },
                    { 517, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Patriot Viper VP4300 Lite 2TB", 91, 18400m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7400 MB/s, Тонкий графен", 0 },
                    { 518, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Sabrent Rocket 5 1TB", 98, 31000m, 0, null, null, "M.2 NVMe, PCIe 5.0, Сверхкомпактный контроллер", 0 },
                    { 519, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Western Digital Blue SN580 1TB", 65, 7900m, 0, null, null, "M.2 NVMe, PCIe 4.0, Энергоэффективный, 4150 MB/s", 0 },
                    { 520, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", true, "Kingston A400 480GB", 30, 3900m, 0, null, null, "SATA III, 2.5\", Ультрабюджетный для ОС", 0 },
                    { 521, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Silicon Power UD90 2TB", 82, 17200m, 0, null, null, "M.2 NVMe, PCIe 4.0, Баланс цены и объема", 0 },
                    { 522, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Acer Predator GM7000 2TB", 93, 21500m, 0, null, null, "M.2 NVMe, PCIe 4.0, Чтение: 7400 MB/s, DRAM буфер", 0 },
                    { 523, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Lexar NM710 500GB", 60, 5200m, 0, null, null, "M.2 NVMe, PCIe 4.0, Хороший старт для бюджетных ПК", 0 },
                    { 524, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg", true, "Team Group T-Force Cardea Z540 1TB", 97, 27800m, 0, null, null, "M.2 NVMe, PCIe 5.0, Чтение: 11700 MB/s, Графеновое охлаждение", 0 },
                    { 525, "Накопитель SSD", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg", false, "Western Digital Red SN700 1TB", 80, 16800m, 0, null, null, "M.2 NVMe, Специализирован для NAS и серверов", 0 },
                    { 601, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Corsair RM1000x SHIFT", 0, 24500m, 1000, null, null, "1000W, 80+ Gold, ATX 3.1, боковые разъемы, PCIe 5.1", 0 },
                    { 602, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Seasonic Vertex PX-1200", 0, 38000m, 1200, null, null, "1200W, 80+ Platinum, ATX 3.1, 12V-2x6 Native, Hybrid Fan", 0 },
                    { 603, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "be quiet! Dark Power Pro 13 1600W", 0, 52000m, 1600, null, null, "1600W, 80+ Titanium, Полностью цифровой, OC Key", 0 },
                    { 604, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "MSI MEG Ai1300P PCIE5.1", 0, 41500m, 1300, null, null, "1300W, 80+ Platinum, Мониторинг через USB, Software Sync", 0 },
                    { 605, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "ASUS ROG Thor 1200W Platinum II", 0, 46000m, 1200, null, null, "1200W, OLED-дисплей, Aura Sync, 80+ Platinum", 0 },
                    { 606, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Montech TITAN GOLD 850W", 0, 14800m, 850, null, null, "850W, 80+ Gold, ATX 3.0, Японские конденсаторы", 0 },
                    { 607, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Thermaltake Toughpower TF3 1550W", 0, 54900m, 1550, null, null, "1550W, 80+ Titanium, Для экстремального разгона, Turbo Mode", 0 },
                    { 608, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "DeepCool PX1000G", 0, 18900m, 1000, null, null, "1000W, 80+ Gold, ATX 3.0, Компактный корпус (160мм)", 0 },
                    { 609, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Corsair SF1000L (SFX-L)", 0, 27600m, 1000, null, null, "1000W, 80+ Gold, Для компактных ITX систем, ATX 3.0", 0 },
                    { 610, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "FSP Hydro Ti PRO 1000W", 0, 32000m, 1000, null, null, "1000W, 80+ Titanium, Ультра-тихий, Lambda A++", 0 },
                    { 611, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Super Flower Leadex VII Gold 1300W", 0, 29400m, 1300, null, null, "1300W, 80+ Gold, ATX 3.0, Запатентованные разъемы", 0 },
                    { 612, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Silverstone Hela 850R Platinum", 0, 21000m, 850, null, null, "850W, 80+ Platinum, Ультра-гибкие кабели, ATX 3.0", 0 },
                    { 613, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Gigabyte UD1000GM PG5", 0, 16200m, 1000, null, null, "1000W, 80+ Gold, Бюджетный PCIe 5.0 выбор", 0 },
                    { 614, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Enermax Revolution D.F. X 1200W", 0, 25800m, 1200, null, null, "1200W, ARGB-панель, Dust Free Rotation (очистка от пыли)", 0 },
                    { 615, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "ADATA XPG Core Reactor II 850W", 0, 13500m, 850, null, null, "850W, 80+ Gold, Полностью модульный, 10 лет гарантии", 0 },
                    { 616, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Phanteks Revolt 1200W Platinum", 0, 22400m, 1200, null, null, "1200W, Без кабелей в комплекте (CableMod ready)", 0 },
                    { 617, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Cooler Master V850 i Gold", 0, 17900m, 850, null, null, "850W, Полуцифровой, Управление через MasterPlus+", 0 },
                    { 618, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Lian Li SP850 (SFX)", 0, 19200m, 850, null, null, "850W, 80+ Gold, Белый цвет, Для O11 Dynamic Mini", 0 },
                    { 619, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Fractal Design Ion+ 2 Platinum 860W", 0, 23100m, 860, null, null, "860W, 80+ Platinum, Ultra-flex кабели", 0 },
                    { 620, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "ASRock Steel Legend SL-1000G", 0, 20500m, 1000, null, null, "1000W, 80+ Gold, Белый камуфляж, ATX 3.1", 0 },
                    { 621, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Chieftec Polaris 3.0 1250W", 0, 17800m, 1250, null, null, "1250W, 80+ Gold, Надежная рабочая лошадка", 0 },
                    { 622, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Antec Signature 1000W Titanium", 0, 36700m, 1000, null, null, "1000W, Топовая схемотехника от Seasonic", 0 },
                    { 623, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "NZXT C1200 Gold", 0, 24900m, 1200, null, null, "1200W, 80+ Gold, Тихий режим Zero Fan", 0 },
                    { 624, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", true, "Cougar GEX X2 1000", 0, 15400m, 1000, null, null, "1000W, 80+ Gold, Хорошее соотношение цена/качество", 0 },
                    { 625, "Блок питания", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg", false, "Zalman TeraMax II 1200W", 0, 16900m, 1200, null, null, "1200W, 80+ Gold, Доступный PCIe 5.1", 0 },
                    { 701, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg", true, "Fractal Design Meshify 3 XL", 0, 24500m, 0, null, null, "Full Tower, Mesh-панель, E-ATX, отличный обдув", 0 },
                    { 702, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg", true, "Lian Li Lancool 217", 0, 16800m, 0, null, null, "Mid Tower, Деревянные вставки, 170мм фанаты, BTF-ready", 0 },
                    { 703, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg", true, "HAVN HS 420", 0, 32000m, 0, null, null, "Dual Chamber, Панорамное стекло, вертикальный обдув", 0 },
                    { 704, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg", true, "NZXT H7 Flow (2026)", 0, 14500m, 0, null, null, "Mid Tower, перфорированная панель, поддержка 420мм СВО", 0 },
                    { 705, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg", true, "Hyte Y70 Touch", 0, 45000m, 0, null, null, "Интегрированный 4K экран, панорамный вид, E-ATX", 0 },
                    { 706, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg", true, "Phanteks Evolv X2", 0, 28900m, 0, null, null, "Алюминий, скрытая укладка кабелей, поддержка Project Zero", 0 },
                    { 707, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg", true, "Corsair Frame 4000D RS", 0, 13200m, 0, null, null, "Модульный каркас, высокая продуваемость, iCUE Link", 0 },
                    { 708, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg", true, "Be Quiet! Shadow Base 800 FX", 0, 21500m, 0, null, null, "Упор на тишину, ARGB, огромный внутренний объем", 0 },
                    { 709, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg", true, "Thermaltake Ceres 350 TG", 0, 11800m, 0, null, null, "LCD панель (опция), сетчатая структура, компактный ATX", 0 },
                    { 710, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg", true, "Montech King 95 Pro", 0, 15900m, 0, null, null, "Изогнутое стекло, 6 предустановленных ARGB фанов", 0 },
                    { 711, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg", true, "Fractal Design North XL", 0, 23400m, 0, null, null, "Стиль с натуральным деревом (орех/дуб), Mesh или Glass", 0 },
                    { 712, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg", true, "Lian Li O11 Vision Compact", 0, 18200m, 0, null, null, "Трехстороннее стекло без стоек, для шоу-сборок", 0 },
                    { 713, "Корпус", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg", true, "DeepCool CH560 Digital", 0, 10500m, 0, null, null, "Встроенный дисплей температуры, высокая продуваемость", 0 },
                    { 801, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Noctua NH-D15 G2", 0, 19500m, 0, null, "LGA1851,AM5,LGA1700", "Король воздуха, 8 теплотрубок, сверхтихий", 5 },
                    { 802, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Arctic Liquid Freezer III Pro 360", 0, 14200m, 0, null, "LGA1851,AM5,LGA1700", "СВО, Встроенный фан для VRM, толстый радиатор", 10 },
                    { 803, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Corsair iCUE Link TITAN 360 RX LCD", 0, 36800m, 0, null, "LGA1851,AM5,LGA1700", "СВО 360mm, IPS-дисплей, экосистема iCUE Link", 12 },
                    { 804, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Lian Li HydroShift II 360TL", 0, 28500m, 0, null, "LGA1851,AM5", "Скрытая укладка шлангов, LCD, высокая производительность", 11 },
                    { 805, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Thermalright Phantom Spirit 120 EVO", 0, 5800m, 0, null, "AM5,LGA1851", "Лучший бюджетный башня, 7 трубок, ARGB фаны", 5 },
                    { 806, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Be Quiet! Pure Loop 3 LX 360", 0, 16400m, 0, null, "LGA1851,AM5", "Тихая СВО, стильная белая подсветка, надежная помпа", 9 },
                    { 807, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "DeepCool AK620 Digital", 0, 8900m, 0, null, "AM5,LGA1700", "Башня с дисплеем температуры и загрузки CPU", 6 },
                    { 808, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "TRYX PANORAMA ARGB 360", 0, 42500m, 0, null, "LGA1851,AM5", "L-образный AMOLED экран, футуристичный дизайн", 15 },
                    { 809, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Arctic Freezer 36 A-RGB", 0, 4200m, 0, null, "AM5,LGA1851", "Бюджетный хит, контактная рамка в комплекте", 4 },
                    { 810, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "MSI MAG CoreLiquid A13 360", 0, 13800m, 0, null, "LGA1851,AM5", "СВО с поворотной крышкой, высокая совместимость", 10 },
                    { 811, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", true, "Noctua NH-L9a-AM5 chromax.black", 0, 7500m, 0, null, "AM5,LGA1700", "Low Profile (37мм), для ITX систем", 3 },
                    { 812, "Охлаждение", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg", false, "Cooler Master MasterLiquid 360 Atmos", 0, 18900m, 0, null, "LGA1851,AM5", "Эко-френдли материалы, топовая тишина и холод", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SessionId",
                table: "ChatMessages",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_UserId",
                table: "ChatSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedBuilds_UserId",
                table: "SavedBuilds",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "SavedBuilds");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChatSessions");

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BaseFrequency = table.Column<double>(type: "REAL", nullable: false),
                    Cores = table.Column<int>(type: "INTEGER", nullable: false),
                    HasIGPU = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PerformanceScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Socket = table.Column<string>(type: "TEXT", nullable: false),
                    TDP = table.Column<int>(type: "INTEGER", nullable: false),
                    Threads = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PerformanceScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CPUs",
                columns: new[] { "Id", "BaseFrequency", "Cores", "HasIGPU", "Name", "PerformanceScore", "Price", "Socket", "TDP", "Threads" },
                values: new object[,]
                {
                    { 1, 0.0, 0, false, "Intel Core i5-10400F", 40, 12000m, "LGA1200", 0, 0 },
                    { 2, 0.0, 0, false, "AMD Ryzen 5 5600x", 25, 16000m, "AM4", 0, 0 },
                    { 3, 0.0, 0, false, "Intel Core i9-14900K", 100, 65000m, "LGA1700", 0, 0 },
                    { 4, 0.0, 0, false, "AAAAA", 100, 444m, "LGA1700", 0, 0 },
                    { 5, 0.0, 0, false, "BBBBB", 100, 666m, "LGA1700", 0, 0 },
                    { 6, 0.0, 0, false, "CCCCCCCC", 100, 777m, "LGA1700", 0, 0 },
                    { 7, 0.0, 0, false, "DDDD", 100, 888m, "LGA1700", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "GPUs",
                columns: new[] { "Id", "Name", "PerformanceScore", "Price" },
                values: new object[] { 1, "NVIDIA RTX 5090", 100, 16000m });
        }
    }
}
