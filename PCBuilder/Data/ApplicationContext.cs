using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Models;

namespace PCBuilder.Data
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Component> Components { get; set; } = null!;
        public DbSet<SavedBuild> SavedBuilds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SavedBuild>().HasIndex(b => b.UserId);

            // ── SEED: каталог комплектующих ──────────────────────────────────────────
            // Картинки: Wikipedia Commons (публичные, без hotlink-блокировки)
            // Цены: рублёвые, актуальные ~2025
            // TDP / PsuWatts / PowerScore — используются для анализа сборки
            modelBuilder.Entity<Component>().HasData(

                // ── ПРОЦЕССОРЫ ────────────────────────────────────────────────────────
                new Component
                {
                    Id = 1,
                    Category = "Процессор",
                    Socket = "AM5",
                    RamType = "DDR5",
                    Name = "AMD Ryzen 5 7600X",
                    Specs = "6 ядер / 12 потоков, 4.7–5.3 GHz, TDP 105W",
                    TDP = 105,
                    PowerScore = 52,
                    PsuWatts = 0,
                    Price = 22990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Ryzen_5_7600X_top.jpg/320px-AMD_Ryzen_5_7600X_top.jpg"
                },
                new Component
                {
                    Id = 2,
                    Category = "Процессор",
                    Socket = "AM5",
                    RamType = "DDR5",
                    Name = "AMD Ryzen 7 7700X",
                    Specs = "8 ядер / 16 потоков, 4.5–5.4 GHz, TDP 105W",
                    TDP = 105,
                    PowerScore = 68,
                    PsuWatts = 0,
                    Price = 33990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Ryzen_5_7600X_top.jpg/320px-AMD_Ryzen_5_7600X_top.jpg"
                },
                new Component
                {
                    Id = 3,
                    Category = "Процессор",
                    Socket = "AM5",
                    RamType = "DDR5",
                    Name = "AMD Ryzen 9 7950X",
                    Specs = "16 ядер / 32 потока, 4.5–5.7 GHz, TDP 170W",
                    TDP = 170,
                    PowerScore = 90,
                    PsuWatts = 0,
                    Price = 59990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Ryzen_5_7600X_top.jpg/320px-AMD_Ryzen_5_7600X_top.jpg"
                },
                new Component
                {
                    Id = 4,
                    Category = "Процессор",
                    Socket = "LGA1700",
                    RamType = "DDR5",
                    Name = "Intel Core i5-13600K",
                    Specs = "14 ядер / 20 потоков, 3.5–5.1 GHz, TDP 125W",
                    TDP = 125,
                    PowerScore = 60,
                    PsuWatts = 0,
                    Price = 27990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Intel_i5-13600K_top.jpg/320px-Intel_i5-13600K_top.jpg"
                },
                new Component
                {
                    Id = 5,
                    Category = "Процессор",
                    Socket = "LGA1700",
                    RamType = "DDR5",
                    Name = "Intel Core i7-13700K",
                    Specs = "16 ядер / 24 потока, 3.4–5.4 GHz, TDP 125W",
                    TDP = 125,
                    PowerScore = 75,
                    PsuWatts = 0,
                    Price = 39990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Intel_i5-13600K_top.jpg/320px-Intel_i5-13600K_top.jpg"
                },
                new Component
                {
                    Id = 6,
                    Category = "Процессор",
                    Socket = "LGA1700",
                    RamType = "DDR5",
                    Name = "Intel Core i9-14900K",
                    Specs = "24 ядра / 32 потока, 3.2–6.0 GHz, TDP 125W",
                    TDP = 125,
                    PowerScore = 95,
                    PsuWatts = 0,
                    Price = 64990,
                    InStock = false,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Intel_i5-13600K_top.jpg/320px-Intel_i5-13600K_top.jpg"
                },

                // ── МАТЕРИНСКИЕ ПЛАТЫ ─────────────────────────────────────────────────
                new Component
                {
                    Id = 10,
                    Category = "Материнская плата",
                    Socket = "AM5",
                    RamType = "DDR5",
                    Name = "ASUS ROG STRIX B650-A Gaming WiFi",
                    Specs = "AM5, DDR5, ATX, PCIe 5.0, Wi-Fi 6E",
                    TDP = 50,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 21990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg"
                },
                new Component
                {
                    Id = 11,
                    Category = "Материнская плата",
                    Socket = "AM5",
                    RamType = "DDR5",
                    Name = "MSI MAG B650 TOMAHAWK WIFI",
                    Specs = "AM5, DDR5, ATX, Wi-Fi 6E, 2.5G LAN",
                    TDP = 50,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 18990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg"
                },
                new Component
                {
                    Id = 12,
                    Category = "Материнская плата",
                    Socket = "LGA1700",
                    RamType = "DDR5",
                    Name = "GIGABYTE Z790 AORUS Elite AX",
                    Specs = "LGA1700, DDR5, ATX, Wi-Fi 6E, Thunderbolt 4",
                    TDP = 50,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 24990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg"
                },
                new Component
                {
                    Id = 13,
                    Category = "Материнская плата",
                    Socket = "LGA1700",
                    RamType = "DDR5",
                    Name = "ASRock B760M Pro RS",
                    Specs = "LGA1700, DDR5, mATX, PCIe 4.0",
                    TDP = 50,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 11990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/ASUS_ROG_STRIX_B550-F_GAMING_top.jpg/320px-ASUS_ROG_STRIX_B550-F_GAMING_top.jpg"
                },

                // ── ВИДЕОКАРТЫ ────────────────────────────────────────────────────────
                new Component
                {
                    Id = 20,
                    Category = "Видеокарта",
                    Name = "Palit RTX 5060 Ti 16GB",
                    Specs = "16GB GDDR7, 4608 ядер CUDA, TDP 180W",
                    TDP = 180,
                    PowerScore = 62,
                    PsuWatts = 0,
                    Price = 49990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg"
                },
                new Component
                {
                    Id = 21,
                    Category = "Видеокарта",
                    Name = "ASUS TUF Gaming RTX 4070 Super",
                    Specs = "12GB GDDR6X, 7168 ядер CUDA, TDP 220W",
                    TDP = 220,
                    PowerScore = 75,
                    PsuWatts = 0,
                    Price = 69990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg"
                },
                new Component
                {
                    Id = 22,
                    Category = "Видеокарта",
                    Name = "MSI Gaming X RX 7800 XT",
                    Specs = "16GB GDDR6, 3840 ядер, TDP 263W",
                    TDP = 263,
                    PowerScore = 70,
                    PsuWatts = 0,
                    Price = 58990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg"
                },
                new Component
                {
                    Id = 23,
                    Category = "Видеокарта",
                    Name = "Gigabyte RTX 4090 Gaming OC",
                    Specs = "24GB GDDR6X, 16384 ядер CUDA, TDP 450W",
                    TDP = 450,
                    PowerScore = 99,
                    PsuWatts = 0,
                    Price = 189990,
                    InStock = false,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Nvidia_RTX_4090_top.jpg/320px-Nvidia_RTX_4090_top.jpg"
                },

                // ── ОПЕРАТИВНАЯ ПАМЯТЬ ────────────────────────────────────────────────
                new Component
                {
                    Id = 30,
                    Category = "Оперативная память",
                    RamType = "DDR5",
                    Name = "Kingston Fury Beast DDR5-5200 32GB",
                    Specs = "2×16GB, DDR5-5200, CL40",
                    TDP = 10,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 7990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/RAM_n.jpg/320px-RAM_n.jpg"
                },
                new Component
                {
                    Id = 31,
                    Category = "Оперативная память",
                    RamType = "DDR5",
                    Name = "G.Skill Trident Z5 RGB DDR5-6000 32GB",
                    Specs = "2×16GB, DDR5-6000, CL30, RGB подсветка",
                    TDP = 10,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 11990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/RAM_n.jpg/320px-RAM_n.jpg"
                },
                new Component
                {
                    Id = 32,
                    Category = "Оперативная память",
                    RamType = "DDR5",
                    Name = "Corsair Vengeance DDR5-5600 64GB",
                    Specs = "2×32GB, DDR5-5600, CL36",
                    TDP = 12,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 16990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/RAM_n.jpg/320px-RAM_n.jpg"
                },

                // ── НАКОПИТЕЛИ SSD ────────────────────────────────────────────────────
                new Component
                {
                    Id = 40,
                    Category = "Накопитель SSD",
                    Name = "Samsung 990 Pro 1TB NVMe",
                    Specs = "M.2 PCIe 4.0, чтение 7450 / запись 6900 MB/s",
                    TDP = 8,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 8990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg"
                },
                new Component
                {
                    Id = 41,
                    Category = "Накопитель SSD",
                    Name = "WD Black SN850X 2TB NVMe",
                    Specs = "M.2 PCIe 4.0, чтение 7300 / запись 6600 MB/s",
                    TDP = 8,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 14990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg"
                },
                new Component
                {
                    Id = 42,
                    Category = "Накопитель SSD",
                    Name = "Crucial T700 1TB NVMe PCIe 5.0",
                    Specs = "M.2 PCIe 5.0, чтение 12400 / запись 11800 MB/s",
                    TDP = 9,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 12490,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg"
                },
                new Component
                {
                    Id = 43,
                    Category = "Накопитель SSD",
                    Name = "Kingston A2000 500GB NVMe",
                    Specs = "M.2 PCIe 3.0, чтение 2200 / запись 2000 MB/s",
                    TDP = 5,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 3990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Samsung_970_EVO.jpg/320px-Samsung_970_EVO.jpg"
                },

                // ── БЛОКИ ПИТАНИЯ ─────────────────────────────────────────────────────
                new Component
                {
                    Id = 50,
                    Category = "Блок питания",
                    Name = "be quiet! Pure Power 12 M 750W",
                    Specs = "750W, 80+ Gold, ATX 3.0, модульный",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 750,
                    Price = 8990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg"
                },
                new Component
                {
                    Id = 51,
                    Category = "Блок питания",
                    Name = "Seasonic Focus GX-650 650W",
                    Specs = "650W, 80+ Gold, ATX, полностью модульный",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 650,
                    Price = 7490,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg"
                },
                new Component
                {
                    Id = 52,
                    Category = "Блок питания",
                    Name = "ASUS ROG Thor 850P2 850W",
                    Specs = "850W, 80+ Platinum, ATX 3.0, OLED дисплей",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 850,
                    Price = 22990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg"
                },
                new Component
                {
                    Id = 53,
                    Category = "Блок питания",
                    Name = "Corsair RM1000x 1000W",
                    Specs = "1000W, 80+ Gold, ATX 3.0, полностью модульный",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 1000,
                    Price = 18990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/PSU.jpg/320px-PSU.jpg"
                },

                // ── КОРПУСЫ ───────────────────────────────────────────────────────────
                new Component
                {
                    Id = 60,
                    Category = "Корпус",
                    Name = "NZXT H7 Flow",
                    Specs = "Mid-Tower ATX, закалённое стекло, 2×USB-A 3.2",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 8490,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg"
                },
                new Component
                {
                    Id = 61,
                    Category = "Корпус",
                    Name = "Fractal Design Meshify 2",
                    Specs = "Mid-Tower ATX, высокий поток воздуха, USB-C",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 11990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg"
                },
                new Component
                {
                    Id = 62,
                    Category = "Корпус",
                    Name = "Lian Li PC-O11 Dynamic EVO",
                    Specs = "Mid-Tower ATX/E-ATX, двойная камера, стекло",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 14990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg"
                },
                new Component
                {
                    Id = 63,
                    Category = "Корпус",
                    Name = "Deepcool CH510",
                    Specs = "Mid-Tower ATX, закалённое стекло, USB-C",
                    TDP = 0,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 5490,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/ABS_computer_case.jpg/320px-ABS_computer_case.jpg"
                },

                // ── ОХЛАЖДЕНИЕ ────────────────────────────────────────────────────────
                new Component
                {
                    Id = 70,
                    Category = "Охлаждение",
                    Socket = "AM5,LGA1700",
                    Name = "Noctua NH-D15 chromax.black",
                    Specs = "Башенный, 2×140mm вентилятора, до 250W TDP",
                    TDP = 5,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 9990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
                },
                new Component
                {
                    Id = 71,
                    Category = "Охлаждение",
                    Socket = "AM5,LGA1700",
                    Name = "be quiet! Dark Rock Pro 4",
                    Specs = "Башенный, 2×135mm вентилятора, до 250W TDP",
                    TDP = 5,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 8490,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
                },
                new Component
                {
                    Id = 72,
                    Category = "Охлаждение",
                    Socket = "AM5,LGA1700",
                    Name = "Corsair iCUE H150i Elite LCD 360mm",
                    Specs = "СВО 360mm, 3×120mm, LCD экран, RGB",
                    TDP = 10,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 19990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
                },
                new Component
                {
                    Id = 73,
                    Category = "Охлаждение",
                    Socket = "AM5,LGA1700",
                    Name = "DeepCool AK620",
                    Specs = "Башенный, 2×120mm вентилятора, до 260W TDP",
                    TDP = 5,
                    PowerScore = 0,
                    PsuWatts = 0,
                    Price = 4990,
                    InStock = true,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
                }
            );
        }
    }
}