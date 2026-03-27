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

    new Component
    {
        Id = 101,
        Category = "Процессор",
        Socket = "LGA1851",
        RamType = "DDR5",
        Name = "Intel Core Ultra 9 285K",
        Specs = "24 ядра / 24 потока, до 5.7 GHz, TDP 125W",
        TDP = 125,
        PowerScore = 98,
        PsuWatts = 0,
        Price = 68900,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_0"
    },
    new Component
    {
        Id = 102,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 9 9950X",
        Specs = "16 ядер / 32 потока, до 5.7 GHz, Zen 5, TDP 170W",
        TDP = 170,
        PowerScore = 100,
        PsuWatts = 0,
        Price = 74500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_1"
    },
    new Component
    {
        Id = 103,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 7 9800X3D",
        Specs = "8 ядер / 16 потока, 96MB L3 Cache, TDP 120W",
        TDP = 120,
        PowerScore = 99,
        PsuWatts = 0,
        Price = 82000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_2"
    },
    new Component
    {
        Id = 104,
        Category = "Процессор",
        Socket = "LGA1851",
        RamType = "DDR5",
        Name = "Intel Core Ultra 7 265K",
        Specs = "20 ядер / 20 потоков, до 5.5 GHz, TDP 125W",
        TDP = 125,
        PowerScore = 88,
        PsuWatts = 0,
        Price = 49900,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_3"
    },
    new Component
    {
        Id = 105,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 9 9900X",
        Specs = "12 ядер / 24 потока, до 5.6 GHz, TDP 120W",
        TDP = 120,
        PowerScore = 92,
        PsuWatts = 0,
        Price = 56000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_4"
    },
    new Component
    {
        Id = 106,
        Category = "Процессор",
        Socket = "LGA1851",
        RamType = "DDR5",
        Name = "Intel Core Ultra 5 245K",
        Specs = "14 ядер / 14 потоков, до 5.2 GHz, TDP 125W",
        TDP = 125,
        PowerScore = 75,
        PsuWatts = 0,
        Price = 36500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_5"
    },
    new Component
    {
        Id = 107,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 7 9700X",
        Specs = "8 ядер / 16 потоков, до 5.5 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 80,
        PsuWatts = 0,
        Price = 41000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_6"
    },
    new Component
    {
        Id = 108,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 5 9600X",
        Specs = "6 ядер / 12 потоков, до 5.4 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 65,
        PsuWatts = 0,
        Price = 31000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_7"
    },
    new Component
    {
        Id = 109,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR5",
        Name = "Intel Core i9-14900KS",
        Specs = "24 ядра / 32 потока, до 6.2 GHz, TDP 150W",
        TDP = 150,
        PowerScore = 98,
        PsuWatts = 0,
        Price = 78000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_8"
    },
    new Component
    {
        Id = 110,
        Category = "Процессор",
        Socket = "AM4",
        RamType = "DDR4",
        Name = "AMD Ryzen 7 5700X3D",
        Specs = "8 ядер / 16 потоков, 96MB L3, TDP 105W",
        TDP = 105,
        PowerScore = 70,
        PsuWatts = 0,
        Price = 24500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_9"
    },
    new Component
    {
        Id = 111,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR5",
        Name = "Intel Core i7-14700KF",
        Specs = "20 ядер / 28 потоков, до 5.6 GHz, TDP 125W",
        TDP = 125,
        PowerScore = 85,
        PsuWatts = 0,
        Price = 44000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_10"
    },
    new Component
    {
        Id = 112,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 5 7500F",
        Specs = "6 ядер / 12 потоков, до 5.0 GHz, без встр. видео, TDP 65W",
        TDP = 65,
        PowerScore = 55,
        PsuWatts = 0,
        Price = 16200,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_11"
    },
    new Component
    {
        Id = 113,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 7 8700G",
        Specs = "8 ядер / 16 потоков, Radeon 780M Graphics, TDP 65W",
        TDP = 65,
        PowerScore = 60,
        PsuWatts = 0,
        Price = 33000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_12"
    },
    new Component
    {
        Id = 114,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR4",
        Name = "Intel Core i5-13400F",
        Specs = "10 ядер / 16 потоков, до 4.6 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 52,
        PsuWatts = 0,
        Price = 19800,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_13"
    },
    new Component
    {
        Id = 115,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR4",
        Name = "Intel Core i3-14100F",
        Specs = "4 ядра / 8 потоков, до 4.7 GHz, TDP 58W",
        TDP = 58,
        PowerScore = 35,
        PsuWatts = 0,
        Price = 11500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_14"
    },
    new Component
    {
        Id = 116,
        Category = "Процессор",
        Socket = "AM4",
        RamType = "DDR4",
        Name = "AMD Ryzen 5 5600GT",
        Specs = "6 ядер / 12 потоков, Radeon Graphics, TDP 65W",
        TDP = 65,
        PowerScore = 40,
        PsuWatts = 0,
        Price = 13200,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_15"
    },
    new Component
    {
        Id = 117,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 9 7900X3D",
        Specs = "12 ядер / 24 потока, 128MB L3 Cache, TDP 120W",
        TDP = 120,
        PowerScore = 94,
        PsuWatts = 0,
        Price = 48500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_16"
    },
    new Component
    {
        Id = 118,
        Category = "Процессор",
        Socket = "LGA1851",
        RamType = "DDR5",
        Name = "Intel Core Ultra 5 245F",
        Specs = "14 ядер / 14 потоков, до 5.0 GHz, без видео, TDP 65W",
        TDP = 65,
        PowerScore = 68,
        PsuWatts = 0,
        Price = 31000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_17"
    },
    new Component
    {
        Id = 119,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 5 8500G",
        Specs = "6 ядер / 12 потоков, Radeon 740M, TDP 65W",
        TDP = 65,
        PowerScore = 45,
        PsuWatts = 0,
        Price = 17900,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_18"
    },
    new Component
    {
        Id = 120,
        Category = "Процессор",
        Socket = "AM4",
        RamType = "DDR4",
        Name = "AMD Ryzen 9 5950X",
        Specs = "16 ядер / 32 потока, до 4.9 GHz, TDP 105W",
        TDP = 105,
        PowerScore = 85,
        PsuWatts = 0,
        Price = 39500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_19"
    },
    new Component
    {
        Id = 121,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR5",
        Name = "Intel Core i5-14400F",
        Specs = "10 ядер / 16 потоков, до 4.7 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 55,
        PsuWatts = 0,
        Price = 22500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_20"
    },
    new Component
    {
        Id = 122,
        Category = "Процессор",
        Socket = "AM5",
        RamType = "DDR5",
        Name = "AMD Ryzen 5 7600",
        Specs = "6 ядер / 12 потоков, до 5.1 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 58,
        PsuWatts = 0,
        Price = 21000,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_21"
    },
    new Component
    {
        Id = 123,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR4",
        Name = "Intel Core i5-12600KF",
        Specs = "10 ядер / 16 потоков, до 4.9 GHz, TDP 125W",
        TDP = 125,
        PowerScore = 65,
        PsuWatts = 0,
        Price = 18500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_22"
    },
    new Component
    {
        Id = 124,
        Category = "Процессор",
        Socket = "AM4",
        RamType = "DDR4",
        Name = "AMD Ryzen 5 5600X",
        Specs = "6 ядер / 12 потоков, до 4.6 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 48,
        PsuWatts = 0,
        Price = 14500,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_23"
    },
    new Component
    {
        Id = 125,
        Category = "Процессор",
        Socket = "LGA1700",
        RamType = "DDR4",
        Name = "Intel Core i7-12700F",
        Specs = "12 ядер / 20 потоков, до 4.9 GHz, TDP 65W",
        TDP = 65,
        PowerScore = 72,
        PsuWatts = 0,
        Price = 28900,
        InStock = true,
        ImageUrl = "http://googleusercontent.com/image_collection/image_retrieval/10202364076808480821_24"
    },

// ── МАТЕРИНСКИЕ ПЛАТЫ ─────────────────────────────────────────────────
// ── МАТЕРИНСКИЕ ПЛАТЫ (25 новых компонентов) ──────────────────────────────────────────────────
new Component
{
    Id = 201,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "ASUS ROG Maximus Z890 Hero",
    Specs = "ATX, Intel Z890, 4xDDR5, 3xM.2 PCIe 5.0, Wi-Fi 7, Thunderbolt 4",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 72500,
    InStock = true,
    ImageUrl = "https://dlcdnrog.asus.com/rog/media/1728514113264.webp"
},
new Component
{
    Id = 202,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "MSI MEG X870E GODLIKE",
    Specs = "E-ATX, AMD X870E, 24+2+1 VRM, PCIe 5.0, 10G LAN, Wi-Fi 7",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 115000,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/MEG-X870E-GODLIKE/godlike-box.png"
},
new Component
{
    Id = 203,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "Gigabyte X870 AORUS ELITE WIFI7",
    Specs = "ATX, AMD X870, PCIe 5.0 x16, 4xM.2, Wi-Fi 7, Ultra Durable",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 34000,
    InStock = true,
    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000"
},
new Component
{
    Id = 204,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "MSI MPG Z890 Carbon WiFi",
    Specs = "ATX, Intel Z890, 20 фаз питания, PCIe 5.0, 5G LAN",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 48900,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/MPG-Z890-CARBON-WIFI/carbon-box.png"
},
new Component
{
    Id = 205,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "ASRock B850 Steel Legend WiFi",
    Specs = "ATX, AMD B850, PCIe 5.0, Wi-Fi 7, 14+2+1 Phase",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 24500,
    InStock = true,
    ImageUrl = "https://www.asrock.com/mb/photo/B850%20Steel%20Legend%20WiFi(L1).png"
},
new Component
{
    Id = 206,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "ASUS TUF Gaming Z890-PLUS WIFI",
    Specs = "ATX, Intel Z890, PCIe 5.0, 14+1 фаза VRM, Wi-Fi 7",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 36000,
    InStock = true,
    ImageUrl = "https://dlcdnrog.asus.com/rog/media/1728514113264.webp"
},
new Component
{
    Id = 207,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "ASUS ROG STRIX X870-F Gaming WiFi",
    Specs = "ATX, AMD X870, AI Overclocking, PCIe 5.0, DDR5-8000+",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 42500,
    InStock = true,
    ImageUrl = "https://dlcdnrog.asus.com/rog/media/1728514113264.webp"
},
new Component
{
    Id = 208,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "Gigabyte B860M GAMING X",
    Specs = "mATX, Intel B860, 2xM.2, PCIe 4.0 x16, 2.5G LAN",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 16800,
    InStock = true,
    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000"
},
new Component
{
    Id = 209,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "MSI MAG B850 TOMAHAWK WIFI",
    Specs = "ATX, AMD B850, PCIe 5.0, Wi-Fi 7, расширенные радиаторы",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 28900,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/MAG-B850-TOMAHAWK-WIFI/tomahawk-box.png"
},
new Component
{
    Id = 210,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "ASRock Z890 Taichi",
    Specs = "E-ATX, Intel Z890, 27 фаз питания, USB4, Hi-Fi Audio",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 58000,
    InStock = true,
    ImageUrl = "https://www.asrock.com/mb/photo/Z890%20Taichi(L1).png"
},
new Component
{
    Id = 211,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "Gigabyte B850M DS3H",
    Specs = "mATX, AMD B850, 4xDDR5, 2xM.2 PCIe 4.0, бюджетный AM5",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 14200,
    InStock = true,
    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000"
},
new Component
{
    Id = 212,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "MSI PRO B860-P WiFi",
    Specs = "ATX, Intel B860, 4xM.2, Wi-Fi 6E, 2.5G LAN",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 19500,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/PRO-B860-P-WIFI/pro-box.png"
},
new Component
{
    Id = 213,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "ASUS Prime X870-P WIFI",
    Specs = "ATX, AMD X870, PCIe 5.0, 3xM.2, серебристый дизайн",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 31500,
    InStock = true,
    ImageUrl = "https://dlcdnrog.asus.com/rog/media/1728514113264.webp"
},
new Component
{
    Id = 214,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "ASRock X870 Steel Legend WiFi",
    Specs = "ATX, AMD X870, белый текстолит, PCIe 5.0, Wi-Fi 7",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 33200,
    InStock = true,
    ImageUrl = "https://www.asrock.com/mb/photo/X870%20Steel%20Legend%20WiFi(L1).png"
},
new Component
{
    Id = 215,
    Category = "Материнская плата",
    Socket = "LGA1700",
    RamType = "DDR5",
    Name = "MSI MAG B760 TOMAHAWK WIFI",
    Specs = "ATX, Intel B760, PCIe 5.0, Wi-Fi 6E, проверенная классика",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 21000,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/MAG-B760-TOMAHAWK-WIFI/box.png"
},
new Component
{
    Id = 216,
    Category = "Материнская плата",
    Socket = "AM4",
    RamType = "DDR4",
    Name = "Gigabyte B550 AORUS ELITE V2",
    Specs = "ATX, AMD B550, PCIe 4.0, 12+2 фазы, лучший выбор для AM4",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 13500,
    InStock = true,
    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/25444/png/1000"
},
new Component
{
    Id = 217,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "ASUS ROG Strix Z890-I Gaming WiFi",
    Specs = "Mini-ITX, Intel Z890, компактная, Wi-Fi 7, Thunderbolt 4",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 45500,
    InStock = true,
    ImageUrl = "https://dlcdnrog.asus.com/rog/media/1728514113264.webp"
},
new Component
{
    Id = 218,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "MSI PRO B650M-A WIFI",
    Specs = "mATX, AMD B650, 2xM.2, Wi-Fi 6E, стабильный бюджет",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 15800,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/PRO-B650M-A-WIFI/box.png"
},
new Component
{
    Id = 219,
    Category = "Материнская плата",
    Socket = "LGA1700",
    RamType = "DDR4",
    Name = "ASRock B760M Pro4",
    Specs = "mATX, Intel B760, DDR4, 2xM.2 PCIe 4.0, доступный Intel",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 11800,
    InStock = true,
    ImageUrl = "https://www.asrock.com/mb/photo/B760M%20Pro4(L1).png"
},
new Component
{
    Id = 220,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "Gigabyte X870E AORUS MASTER",
    Specs = "ATX, AMD X870E, 16+2+2 фазы, USB4, Wi-Fi 7",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 54000,
    InStock = true,
    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000"
},
new Component
{
    Id = 221,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "ASRock B860 Steel Legend WiFi",
    Specs = "ATX, Intel B860, PCIe 5.0, Wi-Fi 7, RGB",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 22000,
    InStock = true,
    ImageUrl = "https://www.asrock.com/mb/photo/B860%20Steel%20Legend%20WiFi(L1).png"
},
new Component
{
    Id = 222,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "MSI MAG X870 TOMAHAWK WIFI",
    Specs = "ATX, AMD X870, PCIe 5.0, 5G LAN, 14+2+1 VRM",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 31000,
    InStock = true,
    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/MAG-X870-TOMAHAWK-WIFI/tomahawk-box.png"
},
new Component
{
    Id = 223,
    Category = "Материнская плата",
    Socket = "LGA1851",
    RamType = "DDR5",
    Name = "Gigabyte Z890 UD WIFI",
    Specs = "ATX, Intel Z890, бюджетный Z-чипсет, Wi-Fi 6E",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 26500,
    InStock = true,
    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/e3b6e8f4c3c3e8e8e8e8e8e8e8e8e8e8/Product/34111/png/1000"
},
new Component
{
    Id = 224,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "ASUS ROG Crosshair X870E Hero",
    Specs = "ATX, AMD X870E, 18+2+2 VRM, USB4, флагман ROG",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 69000,
    InStock = true,
    ImageUrl = "https://dlcdnrog.asus.com/rog/media/1728514113264.webp"
},
new Component
{
    Id = 225,
    Category = "Материнская плата",
    Socket = "AM5",
    RamType = "DDR5",
    Name = "ASRock B650M-HDV/M.2",
    Specs = "mATX, AMD B650, PCIe 5.0 M.2, лучший бюджетный выбор",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 12500,
    InStock = true,
    ImageUrl = "https://www.asrock.com/mb/photo/B650M-HDVM.2(L1).png"
},
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
// ── ВИДЕОКАРТЫ (25 новых компонентов) ────────────────────────────────────────────────────────
new Component
{
    Id = 301,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5090 Founders Edition",
    Specs = "32GB GDDR7, 512-bit, DLSS 4.5, Ray Tracing Gen 5",
    TDP = 450,
    PowerScore = 100,
    PsuWatts = 1000,
    Price = 245000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg"
},
new Component
{
    Id = 302,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5080",
    Specs = "16GB GDDR7, 256-bit, Blackwell Architecture",
    TDP = 320,
    PowerScore = 85,
    PsuWatts = 850,
    Price = 135000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg"
},
new Component
{
    Id = 303,
    Category = "Видеокарта",
    Name = "AMD Radeon RX 8900 XTX",
    Specs = "24GB GDDR7, RDNA 4, FSR 4 AI",
    TDP = 350,
    PowerScore = 90,
    PsuWatts = 900,
    Price = 118000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 304,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5070 Ti",
    Specs = "16GB GDDR7, 192-bit, Perfect for 1440p",
    TDP = 250,
    PowerScore = 75,
    PsuWatts = 750,
    Price = 88000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg"
},
new Component
{
    Id = 305,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5070",
    Specs = "12GB GDDR7, Low Power Blackwell",
    TDP = 200,
    PowerScore = 68,
    PsuWatts = 650,
    Price = 69000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg"
},
new Component
{
    Id = 306,
    Category = "Видеокарта",
    Name = "AMD Radeon RX 8800 XT",
    Specs = "16GB GDDR7, Ray Tracing Overhaul",
    TDP = 230,
    PowerScore = 72,
    PsuWatts = 700,
    Price = 62000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 307,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5060 Ti",
    Specs = "12GB GDDR7, AI Workstation entry",
    TDP = 160,
    PowerScore = 55,
    PsuWatts = 600,
    Price = 52000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg"
},
new Component
{
    Id = 308,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5060",
    Specs = "8GB GDDR7, Ultra Efficient",
    TDP = 115,
    PowerScore = 45,
    PsuWatts = 500,
    Price = 38000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg"
},
new Component
{
    Id = 309,
    Category = "Видеокарта",
    Name = "AMD Radeon RX 8700 XT",
    Specs = "12GB GDDR6, Best Mid-range Value",
    TDP = 180,
    PowerScore = 58,
    PsuWatts = 600,
    Price = 45000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 310,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 4090",
    Specs = "24GB GDDR6X, Legend of 40-series",
    TDP = 450,
    PowerScore = 95,
    PsuWatts = 850,
    Price = 195000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg"
},
new Component
{
    Id = 311,
    Category = "Видеокарта",
    Name = "Intel Arc B580 (Battlemage)",
    Specs = "12GB GDDR6, Xe2-HPG Architecture",
    TDP = 190,
    PowerScore = 48,
    PsuWatts = 600,
    Price = 32000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg/320px-Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg"
},
new Component
{
    Id = 312,
    Category = "Видеокарта",
    Name = "Intel Arc B770 (Battlemage)",
    Specs = "16GB GDDR6, High-end Intel GPU",
    TDP = 225,
    PowerScore = 60,
    PsuWatts = 700,
    Price = 44500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg/320px-Intel_Arc_A770_Limited_Edition_Graphics_Card.jpg"
},
new Component
{
    Id = 313,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 4070 Super",
    Specs = "12GB GDDR6X, 192-bit, Efficiency King",
    TDP = 220,
    PowerScore = 65,
    PsuWatts = 650,
    Price = 64000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg"
},
new Component
{
    Id = 314,
    Category = "Видеокарта",
    Name = "AMD Radeon RX 7900 GRE",
    Specs = "16GB GDDR6, Golden Rabbit Edition",
    TDP = 260,
    PowerScore = 70,
    PsuWatts = 750,
    Price = 59000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 315,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 4060",
    Specs = "8GB GDDR6, Best for 1080p gaming",
    TDP = 115,
    PowerScore = 40,
    PsuWatts = 500,
    Price = 33000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg"
},
new Component
{
    Id = 316,
    Category = "Видеокарта",
    Name = "AMD Radeon RX 7600 XT",
    Specs = "16GB GDDR6, Large VRAM for budget",
    TDP = 190,
    PowerScore = 38,
    PsuWatts = 600,
    Price = 36000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 317,
    Category = "Видеокарта",
    Name = "ASUS ROG Strix RTX 5090 OC",
    Specs = "32GB GDDR7, Triple Fan, Max Overclock",
    TDP = 500,
    PowerScore = 105,
    PsuWatts = 1200,
    Price = 289000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 318,
    Category = "Видеокарта",
    Name = "MSI Suprim X RTX 5080",
    Specs = "16GB GDDR7, Premium Metal Design",
    TDP = 320,
    PowerScore = 87,
    PsuWatts = 850,
    Price = 152000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Nvidia_RTX_3090_FE.jpg/320px-Nvidia_RTX_3090_FE.jpg"
},
new Component
{
    Id = 319,
    Category = "Видеокарта",
    Name = "Gigabyte AORUS Master RTX 5070 Ti",
    Specs = "16GB GDDR7, LCD Edge View",
    TDP = 250,
    PowerScore = 77,
    PsuWatts = 750,
    Price = 98000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Nvidia_GeForce_RTX_3070_Founders_Edition.jpg/320px-Nvidia_GeForce_RTX_3070_Founders_Edition.jpg"
},
new Component
{
    Id = 320,
    Category = "Видеокарта",
    Name = "Palit Dual RTX 5060",
    Specs = "8GB GDDR7, Compact 2-slot",
    TDP = 115,
    PowerScore = 44,
    PsuWatts = 500,
    Price = 37500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg"
},
new Component
{
    Id = 321,
    Category = "Видеокарта",
    Name = "Sapphire NITRO+ RX 8800 XT",
    Specs = "16GB GDDR7, Best cooling for AMD",
    TDP = 250,
    PowerScore = 74,
    PsuWatts = 750,
    Price = 68000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 322,
    Category = "Видеокарта",
    Name = "PowerColor Hellhound RX 8700 XT",
    Specs = "12GB GDDR6, Blue/Teal LED",
    TDP = 200,
    PowerScore = 59,
    PsuWatts = 650,
    Price = 47000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 323,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 3060 12GB",
    Specs = "12GB GDDR6, Budget Productivity King",
    TDP = 170,
    PowerScore = 32,
    PsuWatts = 550,
    Price = 28500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg"
},
new Component
{
    Id = 324,
    Category = "Видеокарта",
    Name = "AMD Radeon RX 6600",
    Specs = "8GB GDDR6, 1080p Entry Level",
    TDP = 132,
    PowerScore = 28,
    PsuWatts = 450,
    Price = 21000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/ASUS_Radeon_RX_6800_XT_TUFGaming.jpg/320px-ASUS_Radeon_RX_6800_XT_TUFGaming.jpg"
},
new Component
{
    Id = 325,
    Category = "Видеокарта",
    Name = "NVIDIA GeForce RTX 5050",
    Specs = "8GB GDDR7, DLSS 4.0 for everyone",
    TDP = 90,
    PowerScore = 35,
    PsuWatts = 400,
    Price = 29500,
    InStock = false,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/GeForce_RTX_3060_Ti.jpg/320px-GeForce_RTX_3060_Ti.jpg"
},
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
// ── ОПЕРАТИВНАЯ ПАМЯТЬ (25 новых компонентов) ────────────────────────────────────────────────
new Component
{
    Id = 401,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "G.Skill Trident Z5 RGB 32GB (2x16GB) 6000MHz CL30",
    Specs = "DDR5-6000, CL30-38-38-96, 1.35V, Intel XMP 3.0, RGB",
    TDP = 0,
    PowerScore = 85,
    PsuWatts = 0,
    Price = 32500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 402,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Kingston FURY Beast Black 32GB (2x16GB) 5600MHz CL40",
    Specs = "DDR5-5600, CL40-40-40, 1.25V, низкопрофильная",
    TDP = 0,
    PowerScore = 75,
    PsuWatts = 0,
    Price = 24800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 403,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Corsair Vengeance RGB 48GB (2x24GB) 7200MHz CL36",
    Specs = "DDR5-7200, небинарный объем, iCUE поддержка",
    TDP = 0,
    PowerScore = 92,
    PsuWatts = 0,
    Price = 41200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corsair_Vengeance_RGB_Pro_RAM.jpg/320px-Corsair_Vengeance_RGB_Pro_RAM.jpg"
},
new Component
{
    Id = 404,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Patriot Viper Xtreme 5 32GB (2x16GB) 8000MHz",
    Specs = "DDR5-8000, CL38, экстремальный разгон, CUDIMM ready",
    TDP = 0,
    PowerScore = 100,
    PsuWatts = 0,
    Price = 48500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 405,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Team Group T-Force Delta RGB 32GB (2x16GB) 6400MHz",
    Specs = "DDR5-6400, CL32, белый радиатор, 120° RGB",
    TDP = 0,
    PowerScore = 88,
    PsuWatts = 0,
    Price = 29900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 406,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "ADATA XPG Lancer Blade 32GB (2x16GB) 6000MHz",
    Specs = "DDR5-6000, CL30, компактный радиатор (33мм)",
    TDP = 0,
    PowerScore = 84,
    PsuWatts = 0,
    Price = 27600,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 407,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Crucial Pro Overclocking 32GB (2x16GB) 6000MHz",
    Specs = "DDR5-6000, CL36, поддержка Intel XMP и AMD EXPO",
    TDP = 0,
    PowerScore = 82,
    PsuWatts = 0,
    Price = 26200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 408,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "G.Skill Flare X5 32GB (2x16GB) 6000MHz CL32",
    Specs = "DDR5-6000, Оптимизировано для AMD AM5/Ryzen",
    TDP = 0,
    PowerScore = 85,
    PsuWatts = 0,
    Price = 30400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 409,
    Category = "Оперативная память",
    RamType = "DDR4",
    Name = "Kingston FURY Renegade 32GB (2x16GB) 3600MHz CL16",
    Specs = "DDR4-3600, CL16-20-20, высокая производительность",
    TDP = 0,
    PowerScore = 65,
    PsuWatts = 0,
    Price = 16500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 410,
    Category = "Оперативная память",
    RamType = "DDR4",
    Name = "Corsair Vengeance LPX 16GB (2x8GB) 3200MHz CL16",
    Specs = "DDR4-3200, компактная, проверенная временем",
    TDP = 0,
    PowerScore = 50,
    PsuWatts = 0,
    Price = 8400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corsair_Vengeance_RGB_Pro_RAM.jpg/320px-Corsair_Vengeance_RGB_Pro_RAM.jpg"
},
new Component
{
    Id = 411,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Corsair Dominator Titanium 64GB (2x32GB) 6600MHz",
    Specs = "DDR5-6600, DHX охлаждение, сменные верхние планки",
    TDP = 0,
    PowerScore = 95,
    PsuWatts = 0,
    Price = 78000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corsair_Vengeance_RGB_Pro_RAM.jpg/320px-Corsair_Vengeance_RGB_Pro_RAM.jpg"
},
new Component
{
    Id = 412,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Team Group T-Create Expert 64GB (2x32GB) 6000MHz",
    Specs = "DDR5-6000, CL34, для рабочих станций и рендеринга",
    TDP = 0,
    PowerScore = 90,
    PsuWatts = 0,
    Price = 64500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 413,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Patriot Viper Venom 16GB (2x8GB) 5200MHz",
    Specs = "DDR5-5200, входной уровень в новое поколение",
    TDP = 0,
    PowerScore = 60,
    PsuWatts = 0,
    Price = 12900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 414,
    Category = "Оперативная память",
    RamType = "DDR4",
    Name = "ADATA XPG Spectrix D35G RGB 16GB (2x8GB) 3200MHz",
    Specs = "DDR4-3200, CL16, стильный RGB",
    TDP = 0,
    PowerScore = 52,
    PsuWatts = 0,
    Price = 9200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 415,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "G.Skill Trident Z5 Neo 32GB (2x16GB) 6400MHz",
    Specs = "DDR5-6400, CL32, AMD EXPO профиль",
    TDP = 0,
    PowerScore = 89,
    PsuWatts = 0,
    Price = 33800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 416,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Crucial 96GB (2x48GB) 5600MHz CL46",
    Specs = "DDR5-5600, JEDEC стандарт, огромный объем",
    TDP = 0,
    PowerScore = 88,
    PsuWatts = 0,
    Price = 72000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 417,
    Category = "Оперативная память",
    RamType = "DDR4",
    Name = "Team Group T-Force Vulcan Z 32GB (2x16GB) 3200MHz",
    Specs = "DDR4-3200, бюджетный выбор для рабочих ПК",
    TDP = 0,
    PowerScore = 60,
    PsuWatts = 0,
    Price = 14200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 418,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Kingston FURY Renegade RGB 32GB (2x16GB) 7200MHz",
    Specs = "DDR5-7200, CL38, премиальное охлаждение",
    TDP = 0,
    PowerScore = 93,
    PsuWatts = 0,
    Price = 43500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 419,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Netac Shadow RGB 32GB (2x16GB) 6200MHz",
    Specs = "DDR5-6200, CL32, агрессивный дизайн",
    TDP = 0,
    PowerScore = 86,
    PsuWatts = 0,
    Price = 28400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 420,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "ASRock Phantom Gaming RGB 32GB (2x16GB) 6000MHz",
    Specs = "DDR5-6000, брендированная память для плат ASRock",
    TDP = 0,
    PowerScore = 83,
    PsuWatts = 0,
    Price = 29100,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 421,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Lexar Ares RGB 32GB (2x16GB) 6400MHz",
    Specs = "DDR5-6400, CL32, чипы SK Hynix A-Die",
    TDP = 0,
    PowerScore = 89,
    PsuWatts = 0,
    Price = 31500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 422,
    Category = "Оперативная память",
    RamType = "DDR4",
    Name = "G.Skill Ripjaws V 16GB (2x8GB) 3600MHz CL18",
    Specs = "DDR4-3600, классика для Intel/AMD",
    TDP = 0,
    PowerScore = 55,
    PsuWatts = 0,
    Price = 9800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 423,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Silicon Power Zenith RGB 32GB (2x16GB) 6000MHz",
    Specs = "DDR5-6000, бюджетное решение с подсветкой",
    TDP = 0,
    PowerScore = 81,
    PsuWatts = 0,
    Price = 25900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
new Component
{
    Id = 424,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "GeIL Polaris RGB 32GB (2x16GB) 5200MHz",
    Specs = "DDR5-5200, первый массовый DDR5 бренд",
    TDP = 0,
    PowerScore = 70,
    PsuWatts = 0,
    Price = 21400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/G.Skill_Trident_Z_RGB_Memory.jpg/320px-G.Skill_Trident_Z_RGB_Memory.jpg"
},
new Component
{
    Id = 425,
    Category = "Оперативная память",
    RamType = "DDR5",
    Name = "Mushkin Redline ST 64GB (2x32GB) 6400MHz CL32",
    Specs = "DDR5-6400, CL32, для энтузиастов",
    TDP = 0,
    PowerScore = 94,
    PsuWatts = 0,
    Price = 69800,
    InStock = false,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Kingston_FURY_Beast_DDR5_Memory.jpg/320px-Kingston_FURY_Beast_DDR5_Memory.jpg"
},
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
// ── НАКОПИТЕЛИ SSD (25 новых компонентов) ──────────────────────────────────────────────────
new Component
{
    Id = 501,
    Category = "Накопитель SSD",
    Name = "Samsung 990 Pro 2TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7450 MB/s, Запись: 6900 MB/s",
    TDP = 0,
    PowerScore = 95,
    PsuWatts = 0,
    Price = 28500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 502,
    Category = "Накопитель SSD",
    Name = "Crucial T705 1TB PCIe 5.0",
    Specs = "M.2 NVMe, PCIe 5.0, Чтение: 13600 MB/s, Экстремальная скорость",
    TDP = 0,
    PowerScore = 100,
    PsuWatts = 0,
    Price = 34000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 503,
    Category = "Накопитель SSD",
    Name = "Kingston FURY Renegade 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7300 MB/s, С радиатором",
    TDP = 0,
    PowerScore = 92,
    PsuWatts = 0,
    Price = 14200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 504,
    Category = "Накопитель SSD",
    Name = "Western Digital Black SN850X 2TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7300 MB/s, Игровой SSD",
    TDP = 0,
    PowerScore = 94,
    PsuWatts = 0,
    Price = 26900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 505,
    Category = "Накопитель SSD",
    Name = "ADATA XPG GAMMIX S70 Blade 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7400 MB/s, Совместим с PS5",
    TDP = 0,
    PowerScore = 89,
    PsuWatts = 0,
    Price = 11500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 506,
    Category = "Накопитель SSD",
    Name = "Samsung 980 1TB",
    Specs = "M.2 NVMe, PCIe 3.0, Безбуферный, Надежный выбор",
    TDP = 0,
    PowerScore = 70,
    PsuWatts = 0,
    Price = 9800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 507,
    Category = "Накопитель SSD",
    Name = "MSI Spatium M570 Pro 2TB",
    Specs = "M.2 NVMe, PCIe 5.0, Чтение: 12400 MB/s, Frozr радиатор",
    TDP = 0,
    PowerScore = 99,
    PsuWatts = 0,
    Price = 48000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 508,
    Category = "Накопитель SSD",
    Name = "Kingston NV3 2TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 5000 MB/s, Бюджетный 2TB",
    TDP = 0,
    PowerScore = 78,
    PsuWatts = 0,
    Price = 16500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 509,
    Category = "Накопитель SSD",
    Name = "Corsair MP700 PRO 1TB",
    Specs = "M.2 NVMe, PCIe 5.0, Чтение: 11700 MB/s, Активное охлаждение",
    TDP = 0,
    PowerScore = 98,
    PsuWatts = 0,
    Price = 29500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 510,
    Category = "Накопитель SSD",
    Name = "Team Group MP44L 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 5000 MB/s, Тонкий радиатор",
    TDP = 0,
    PowerScore = 75,
    PsuWatts = 0,
    Price = 8900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 511,
    Category = "Накопитель SSD",
    Name = "Lexar NM790 4TB",
    Specs = "M.2 NVMe, PCIe 4.0, Огромный объем, Чтение: 7400 MB/s",
    TDP = 0,
    PowerScore = 96,
    PsuWatts = 0,
    Price = 42000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 512,
    Category = "Накопитель SSD",
    Name = "Netac NV7000 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7200 MB/s, Народный топ",
    TDP = 0,
    PowerScore = 85,
    PsuWatts = 0,
    Price = 10800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 513,
    Category = "Накопитель SSD",
    Name = "Seagate FireCuda 530 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Ресурс 1275 TBW",
    TDP = 0,
    PowerScore = 93,
    PsuWatts = 0,
    Price = 15900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 514,
    Category = "Накопитель SSD",
    Name = "Crucial P3 Plus 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Доступная скорость 4800 MB/s",
    TDP = 0,
    PowerScore = 72,
    PsuWatts = 0,
    Price = 8200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 515,
    Category = "Накопитель SSD",
    Name = "Samsung 870 EVO 1TB",
    Specs = "SATA III, 2.5\", Классика для хранения данных",
    TDP = 0,
    PowerScore = 40,
    PsuWatts = 0,
    Price = 11200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 516,
    Category = "Накопитель SSD",
    Name = "Gigabyte AORUS Gen5 12000 2TB",
    Specs = "M.2 NVMe, PCIe 5.0, Чтение: 12400 MB/s, Массивный радиатор",
    TDP = 0,
    PowerScore = 99,
    PsuWatts = 0,
    Price = 45500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 517,
    Category = "Накопитель SSD",
    Name = "Patriot Viper VP4300 Lite 2TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7400 MB/s, Тонкий графен",
    TDP = 0,
    PowerScore = 91,
    PsuWatts = 0,
    Price = 18400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 518,
    Category = "Накопитель SSD",
    Name = "Sabrent Rocket 5 1TB",
    Specs = "M.2 NVMe, PCIe 5.0, Сверхкомпактный контроллер",
    TDP = 0,
    PowerScore = 98,
    PsuWatts = 0,
    Price = 31000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 519,
    Category = "Накопитель SSD",
    Name = "Western Digital Blue SN580 1TB",
    Specs = "M.2 NVMe, PCIe 4.0, Энергоэффективный, 4150 MB/s",
    TDP = 0,
    PowerScore = 65,
    PsuWatts = 0,
    Price = 7900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 520,
    Category = "Накопитель SSD",
    Name = "Kingston A400 480GB",
    Specs = "SATA III, 2.5\", Ультрабюджетный для ОС",
    TDP = 0,
    PowerScore = 30,
    PsuWatts = 0,
    Price = 3900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
new Component
{
    Id = 521,
    Category = "Накопитель SSD",
    Name = "Silicon Power UD90 2TB",
    Specs = "M.2 NVMe, PCIe 4.0, Баланс цены и объема",
    TDP = 0,
    PowerScore = 82,
    PsuWatts = 0,
    Price = 17200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 522,
    Category = "Накопитель SSD",
    Name = "Acer Predator GM7000 2TB",
    Specs = "M.2 NVMe, PCIe 4.0, Чтение: 7400 MB/s, DRAM буфер",
    TDP = 0,
    PowerScore = 93,
    PsuWatts = 0,
    Price = 21500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 523,
    Category = "Накопитель SSD",
    Name = "Lexar NM710 500GB",
    Specs = "M.2 NVMe, PCIe 4.0, Хороший старт для бюджетных ПК",
    TDP = 0,
    PowerScore = 60,
    PsuWatts = 0,
    Price = 5200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 524,
    Category = "Накопитель SSD",
    Name = "Team Group T-Force Cardea Z540 1TB",
    Specs = "M.2 NVMe, PCIe 5.0, Чтение: 11700 MB/s, Графеновое охлаждение",
    TDP = 0,
    PowerScore = 97,
    PsuWatts = 0,
    Price = 27800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/M.2_SSD_with_heatsink.jpg/320px-M.2_SSD_with_heatsink.jpg"
},
new Component
{
    Id = 525,
    Category = "Накопитель SSD",
    Name = "Western Digital Red SN700 1TB",
    Specs = "M.2 NVMe, Специализирован для NAS и серверов",
    TDP = 0,
    PowerScore = 80,
    PsuWatts = 0,
    Price = 16800,
    InStock = false,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Samsung_980_Pro_1TB_SSD.jpg/320px-Samsung_980_Pro_1TB_SSD.jpg"
},
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
// ── БЛОКИ ПИТАНИЯ (25 новых компонентов) ──────────────────────────────────────────────────
new Component
{
    Id = 601,
    Category = "Блок питания",
    Name = "Corsair RM1000x SHIFT",
    Specs = "1000W, 80+ Gold, ATX 3.1, боковые разъемы, PCIe 5.1",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 24500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 602,
    Category = "Блок питания",
    Name = "Seasonic Vertex PX-1200",
    Specs = "1200W, 80+ Platinum, ATX 3.1, 12V-2x6 Native, Hybrid Fan",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1200,
    Price = 38000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 603,
    Category = "Блок питания",
    Name = "be quiet! Dark Power Pro 13 1600W",
    Specs = "1600W, 80+ Titanium, Полностью цифровой, OC Key",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1600,
    Price = 52000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 604,
    Category = "Блок питания",
    Name = "MSI MEG Ai1300P PCIE5.1",
    Specs = "1300W, 80+ Platinum, Мониторинг через USB, Software Sync",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1300,
    Price = 41500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 605,
    Category = "Блок питания",
    Name = "ASUS ROG Thor 1200W Platinum II",
    Specs = "1200W, OLED-дисплей, Aura Sync, 80+ Platinum",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1200,
    Price = 46000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 606,
    Category = "Блок питания",
    Name = "Montech TITAN GOLD 850W",
    Specs = "850W, 80+ Gold, ATX 3.0, Японские конденсаторы",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 850,
    Price = 14800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 607,
    Category = "Блок питания",
    Name = "Thermaltake Toughpower TF3 1550W",
    Specs = "1550W, 80+ Titanium, Для экстремального разгона, Turbo Mode",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1550,
    Price = 54900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 608,
    Category = "Блок питания",
    Name = "DeepCool PX1000G",
    Specs = "1000W, 80+ Gold, ATX 3.0, Компактный корпус (160мм)",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 18900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 609,
    Category = "Блок питания",
    Name = "Corsair SF1000L (SFX-L)",
    Specs = "1000W, 80+ Gold, Для компактных ITX систем, ATX 3.0",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 27600,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 610,
    Category = "Блок питания",
    Name = "FSP Hydro Ti PRO 1000W",
    Specs = "1000W, 80+ Titanium, Ультра-тихий, Lambda A++",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 32000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 611,
    Category = "Блок питания",
    Name = "Super Flower Leadex VII Gold 1300W",
    Specs = "1300W, 80+ Gold, ATX 3.0, Запатентованные разъемы",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1300,
    Price = 29400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 612,
    Category = "Блок питания",
    Name = "Silverstone Hela 850R Platinum",
    Specs = "850W, 80+ Platinum, Ультра-гибкие кабели, ATX 3.0",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 850,
    Price = 21000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 613,
    Category = "Блок питания",
    Name = "Gigabyte UD1000GM PG5",
    Specs = "1000W, 80+ Gold, Бюджетный PCIe 5.0 выбор",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 16200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 614,
    Category = "Блок питания",
    Name = "Enermax Revolution D.F. X 1200W",
    Specs = "1200W, ARGB-панель, Dust Free Rotation (очистка от пыли)",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1200,
    Price = 25800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 615,
    Category = "Блок питания",
    Name = "ADATA XPG Core Reactor II 850W",
    Specs = "850W, 80+ Gold, Полностью модульный, 10 лет гарантии",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 850,
    Price = 13500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 616,
    Category = "Блок питания",
    Name = "Phanteks Revolt 1200W Platinum",
    Specs = "1200W, Без кабелей в комплекте (CableMod ready)",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1200,
    Price = 22400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 617,
    Category = "Блок питания",
    Name = "Cooler Master V850 i Gold",
    Specs = "850W, Полуцифровой, Управление через MasterPlus+",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 850,
    Price = 17900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 618,
    Category = "Блок питания",
    Name = "Lian Li SP850 (SFX)",
    Specs = "850W, 80+ Gold, Белый цвет, Для O11 Dynamic Mini",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 850,
    Price = 19200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 619,
    Category = "Блок питания",
    Name = "Fractal Design Ion+ 2 Platinum 860W",
    Specs = "860W, 80+ Platinum, Ultra-flex кабели",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 860,
    Price = 23100,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 620,
    Category = "Блок питания",
    Name = "ASRock Steel Legend SL-1000G",
    Specs = "1000W, 80+ Gold, Белый камуфляж, ATX 3.1",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 20500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 621,
    Category = "Блок питания",
    Name = "Chieftec Polaris 3.0 1250W",
    Specs = "1250W, 80+ Gold, Надежная рабочая лошадка",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1250,
    Price = 17800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 622,
    Category = "Блок питания",
    Name = "Antec Signature 1000W Titanium",
    Specs = "1000W, Топовая схемотехника от Seasonic",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 36700,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 623,
    Category = "Блок питания",
    Name = "NZXT C1200 Gold",
    Specs = "1200W, 80+ Gold, Тихий режим Zero Fan",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1200,
    Price = 24900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 624,
    Category = "Блок питания",
    Name = "Cougar GEX X2 1000",
    Specs = "1000W, 80+ Gold, Хорошее соотношение цена/качество",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1000,
    Price = 15400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
new Component
{
    Id = 625,
    Category = "Блок питания",
    Name = "Zalman TeraMax II 1200W",
    Specs = "1200W, 80+ Gold, Доступный PCIe 5.1",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 1200,
    Price = 16900,
    InStock = false,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Power_supply_unit_2010.jpg/320px-Power_supply_unit_2010.jpg"
},
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
// ── КОРПУСА (13 компонентов) ──────────────────────────────────────────────────────────────
new Component
{
    Id = 701,
    Category = "Корпус",
    Name = "Fractal Design Meshify 3 XL",
    Specs = "Full Tower, Mesh-панель, E-ATX, отличный обдув",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 24500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg"
},
new Component
{
    Id = 702,
    Category = "Корпус",
    Name = "Lian Li Lancool 217",
    Specs = "Mid Tower, Деревянные вставки, 170мм фанаты, BTF-ready",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 16800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg"
},
new Component
{
    Id = 703,
    Category = "Корпус",
    Name = "HAVN HS 420",
    Specs = "Dual Chamber, Панорамное стекло, вертикальный обдув",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 32000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg"
},
new Component
{
    Id = 704,
    Category = "Корпус",
    Name = "NZXT H7 Flow (2026)",
    Specs = "Mid Tower, перфорированная панель, поддержка 420мм СВО",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 14500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg"
},
new Component
{
    Id = 705,
    Category = "Корпус",
    Name = "Hyte Y70 Touch",
    Specs = "Интегрированный 4K экран, панорамный вид, E-ATX",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 45000,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg"
},
new Component
{
    Id = 706,
    Category = "Корпус",
    Name = "Phanteks Evolv X2",
    Specs = "Алюминий, скрытая укладка кабелей, поддержка Project Zero",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 28900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg"
},
new Component
{
    Id = 707,
    Category = "Корпус",
    Name = "Corsair Frame 4000D RS",
    Specs = "Модульный каркас, высокая продуваемость, iCUE Link",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 13200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg"
},
new Component
{
    Id = 708,
    Category = "Корпус",
    Name = "Be Quiet! Shadow Base 800 FX",
    Specs = "Упор на тишину, ARGB, огромный внутренний объем",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 21500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg"
},
new Component
{
    Id = 709,
    Category = "Корпус",
    Name = "Thermaltake Ceres 350 TG",
    Specs = "LCD панель (опция), сетчатая структура, компактный ATX",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 11800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg"
},
new Component
{
    Id = 710,
    Category = "Корпус",
    Name = "Montech King 95 Pro",
    Specs = "Изогнутое стекло, 6 предустановленных ARGB фанов",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 15900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg"
},
new Component
{
    Id = 711,
    Category = "Корпус",
    Name = "Fractal Design North XL",
    Specs = "Стиль с натуральным деревом (орех/дуб), Mesh или Glass",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 23400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Fractal_Design_Meshify_C_Case.jpg/320px-Fractal_Design_Meshify_C_Case.jpg"
},
new Component
{
    Id = 712,
    Category = "Корпус",
    Name = "Lian Li O11 Vision Compact",
    Specs = "Трехстороннее стекло без стоек, для шоу-сборок",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 18200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Lian_Li_PC-O11_Dynamic_White.jpg/320px-Lian_Li_PC-O11_Dynamic_White.jpg"
},
new Component
{
    Id = 713,
    Category = "Корпус",
    Name = "DeepCool CH560 Digital",
    Specs = "Встроенный дисплей температуры, высокая продуваемость",
    TDP = 0,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 10500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/NZXT_H510_Elite_Case.jpg/320px-NZXT_H510_Elite_Case.jpg"
},

// ── ОХЛАЖДЕНИЕ (12 компонентов) ───────────────────────────────────────────────────────────
new Component
{
    Id = 801,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5,LGA1700",
    Name = "Noctua NH-D15 G2",
    Specs = "Король воздуха, 8 теплотрубок, сверхтихий",
    TDP = 5,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 19500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 802,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5,LGA1700",
    Name = "Arctic Liquid Freezer III Pro 360",
    Specs = "СВО, Встроенный фан для VRM, толстый радиатор",
    TDP = 10,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 14200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 803,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5,LGA1700",
    Name = "Corsair iCUE Link TITAN 360 RX LCD",
    Specs = "СВО 360mm, IPS-дисплей, экосистема iCUE Link",
    TDP = 12,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 36800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 804,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5",
    Name = "Lian Li HydroShift II 360TL",
    Specs = "Скрытая укладка шлангов, LCD, высокая производительность",
    TDP = 11,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 28500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 805,
    Category = "Охлаждение",
    Socket = "AM5,LGA1851",
    Name = "Thermalright Phantom Spirit 120 EVO",
    Specs = "Лучший бюджетный башня, 7 трубок, ARGB фаны",
    TDP = 5,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 5800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 806,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5",
    Name = "Be Quiet! Pure Loop 3 LX 360",
    Specs = "Тихая СВО, стильная белая подсветка, надежная помпа",
    TDP = 9,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 16400,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 807,
    Category = "Охлаждение",
    Socket = "AM5,LGA1700",
    Name = "DeepCool AK620 Digital",
    Specs = "Башня с дисплеем температуры и загрузки CPU",
    TDP = 6,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 8900,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 808,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5",
    Name = "TRYX PANORAMA ARGB 360",
    Specs = "L-образный AMOLED экран, футуристичный дизайн",
    TDP = 15,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 42500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 809,
    Category = "Охлаждение",
    Socket = "AM5,LGA1851",
    Name = "Arctic Freezer 36 A-RGB",
    Specs = "Бюджетный хит, контактная рамка в комплекте",
    TDP = 4,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 4200,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 810,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5",
    Name = "MSI MAG CoreLiquid A13 360",
    Specs = "СВО с поворотной крышкой, высокая совместимость",
    TDP = 10,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 13800,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 811,
    Category = "Охлаждение",
    Socket = "AM5,LGA1700",
    Name = "Noctua NH-L9a-AM5 chromax.black",
    Specs = "Low Profile (37мм), для ITX систем",
    TDP = 3,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 7500,
    InStock = true,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
new Component
{
    Id = 812,
    Category = "Охлаждение",
    Socket = "LGA1851,AM5",
    Name = "Cooler Master MasterLiquid 360 Atmos",
    Specs = "Эко-френдли материалы, топовая тишина и холод",
    TDP = 10,
    PowerScore = 0,
    PsuWatts = 0,
    Price = 18900,
    InStock = false,
    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Noctua_NH-D15_cooler.jpg/320px-Noctua_NH-D15_cooler.jpg"
},
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