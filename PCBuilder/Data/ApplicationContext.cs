using PCBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace PCBuilder.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted(); // Пересоздаёт БД при каждом запуске (для разработки)
            Database.EnsureCreated();
        }

        public DbSet<Component> Components { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOffer>()
                .HasOne(o => o.Component)
                .WithMany(c => c.Offers)
                .HasForeignKey(o => o.ComponentId);

            // ══════════════════════════════════════════════
            //  КОМПОНЕНТЫ
            // ══════════════════════════════════════════════
            modelBuilder.Entity<Component>().HasData(

                // ── ПРОЦЕССОРЫ ──────────────────────────────
                new Component
                {
                    Id = 1,
                    Category = "Процессор",
                    Name = "AMD Ryzen 5 7600X",
                    Specs = "6 ядер / 12 потоков, 4.7–5.3 GHz, TDP 105W, AM5",
                    Socket = "AM5",
                    ImageUrl = "https://static.nix.ru/images/amd-ryzen-5-7600x-6852443159.jpg?good_id=685244&width=draft&height=draft&view_id=3159"
                },
                new Component
                {
                    Id = 2,
                    Category = "Процессор",
                    Name = "AMD Ryzen 7 7700X",
                    Specs = "8 ядер / 16 потоков, 4.5–5.4 GHz, TDP 105W, AM5",
                    Socket = "AM5",
                    ImageUrl = "https://cdn.citilink.ru/yg74GxWMYjR758UoTEmQbqcc0OxtzqRJ-ozGOSIBYEk/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/fc4073b7-24f2-40fa-8fa5-4ddd31b6130f.jpg"
                },
                new Component
                {
                    Id = 3,
                    Category = "Процессор",
                    Name = "Intel Core i5-13600K",
                    Specs = "14 ядер / 20 потоков, 3.5–5.1 GHz, TDP 125W, LGA1700",
                    Socket = "LGA1700",
                    ImageUrl = "https://cdn.citilink.ru/gF1CVTu1_zSsaeCef5h_oO_Th2D8-HdULXuGyDZtc5A/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/6f263d3c-23e3-4b62-ae2e-ecd7f62e83df.jpg"
                },
                new Component
                {
                    Id = 4,
                    Category = "Процессор",
                    Name = "Intel Core i9-14900K",
                    Specs = "24 ядра / 32 потока, 3.2–6.0 GHz, TDP 125W, LGA1700",
                    Socket = "LGA1700",
                    ImageUrl = "https://microless.com/cdn/products/a9b68aae9c892ccf529718c92498238d-hi.jpg"
                },

                // ── МАТЕРИНСКИЕ ПЛАТЫ ────────────────────────
                new Component
                {
                    Id = 10,
                    Category = "Материнская плата",
                    Name = "ASUS ROG STRIX B650-A Gaming",
                    Specs = "AM5, DDR5, ATX, 4×DIMM, PCIe 5.0",
                    Socket = "AM5",
                    ImageUrl = "https://m.onlinetrade.ru/img/items/m/materinskaya_plata_asus_rog_strix_b650_a_gaming_wifi_am5_atx__2489839_1.jpg"
                },
                new Component
                {
                    Id = 11,
                    Category = "Материнская плата",
                    Name = "MSI MAG B650 TOMAHAWK WIFI",
                    Specs = "AM5, DDR5, ATX, Wi-Fi 6E, 2.5G LAN",
                    Socket = "AM5",
                    ImageUrl = "https://storage-asset.msi.com/global/picture/image/feature/mb/B650/MAG-B650-TOMAHAWK-WIFI/mag-b650-tomahawk-wifi.png"
                },
                new Component
                {
                    Id = 12,
                    Category = "Материнская плата",
                    Name = "GIGABYTE Z790 AORUS Elite AX",
                    Specs = "LGA1700, DDR5, ATX, Wi-Fi 6E, Thunderbolt 4",
                    Socket = "LGA1700",
                    ImageUrl = "https://brigo.ru/upload/iblock/55b/ulxemycy3np0efuvn4co1ezgse5r69d4/1163682.png"
                },
                new Component
                {
                    Id = 13,
                    Category = "Материнская плата",
                    Name = "ASRock B760M Pro RS",
                    Specs = "LGA1700, DDR5, mATX, PCIe 4.0, 2.5G LAN",
                    Socket = "LGA1700",
                    ImageUrl = "https://cdn.citilink.ru/ZwNZqEW_0wXDcw8HlqKlU9AuT7qjoL1-3Sd0XNlPKQk/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/41adaf62-9ae1-4e9b-9ee9-cab641531007.jpg"
                },

                // ── ВИДЕОКАРТЫ ───────────────────────────────
                new Component
                {
                    Id = 20,
                    Category = "Видеокарта",
                    Name = "Palit RTX 5060 Ti 16GB",
                    Specs = "16GB GDDR7, 4608 ядер, TDP 180W, PCIe 5.0",
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/16454321/2a0000019715a6a64b63ad2230ead5977f5f/orig"
                },
                new Component
                {
                    Id = 21,
                    Category = "Видеокарта",
                    Name = "ASUS TUF RTX 4070 Super",
                    Specs = "12GB GDDR6X, 7168 ядер, TDP 220W, PCIe 4.0",
                    ImageUrl = "https://ir.ozone.ru/s3/multimedia-1-l/6984221133.jpg"
                },
                new Component
                {
                    Id = 22,
                    Category = "Видеокарта",
                    Name = "MSI Gaming X RX 7800 XT",
                    Specs = "16GB GDDR6, 3840 ядер, TDP 263W, PCIe 4.0",
                    ImageUrl = "https://e-katalog.kz/jpg_zoom1/2507814.jpg"
                },
                new Component
                {
                    Id = 23,
                    Category = "Видеокарта",
                    Name = "Gigabyte RTX 4090 Gaming OC",
                    Specs = "24GB GDDR6X, 16384 ядер, TDP 450W, PCIe 4.0",
                    ImageUrl = "https://www.mvideo.ru/Big/30065786bb.jpg"
                },

                // ── ОПЕРАТИВНАЯ ПАМЯТЬ ───────────────────────
                new Component
                {
                    Id = 30,
                    Category = "Оперативная память",
                    Name = "Kingston Fury Beast DDR5-5200 32GB",
                    Specs = "2×16GB, DDR5-5200, CL40, 1.1V",
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/16164715/2a0000019bb77a7aa0439599ceafbab2acfa/orig"
                },
                new Component
                {
                    Id = 31,
                    Category = "Оперативная память",
                    Name = "G.Skill Trident Z5 RGB DDR5-6000 32GB",
                    Specs = "2×16GB, DDR5-6000, CL30, 1.35V, RGB",
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/5209746/2a00000191a2a8185d4590183f1428f6ab8f/orig"
                },
                new Component
                {
                    Id = 32,
                    Category = "Оперативная память",
                    Name = "Corsair Vengeance DDR5-5600 64GB",
                    Specs = "2×32GB, DDR5-5600, CL36, 1.25V",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRnE8txeV81LIvmf6c5GoV5q8GAye5Myu_WDQ&s"
                },

                // ── НАКОПИТЕЛЬ SSD ───────────────────────────
                new Component
                {
                    Id = 40,
                    Category = "Накопитель SSD",
                    Name = "Samsung 990 Pro 1TB NVMe",
                    Specs = "M.2 PCIe 4.0, 7450/6900 MB/s, TLC",
                    ImageUrl = "https://c.dns-shop.ru/thumb/st4/fit/300/300/64b30a9c4e6ee0335ba87e4043f72b42/c0e0fdbb70b14fa69e223b44b1ea251d48ea8fde682cf2a535075882a6c7a1aa.jpg"
                },
                new Component
                {
                    Id = 41,
                    Category = "Накопитель SSD",
                    Name = "WD Black SN850X 2TB NVMe",
                    Specs = "M.2 PCIe 4.0, 7300/6600 MB/s, TLC",
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/4954989/2a00000195a3879f8494bfd3e7d4131175f3/orig"
                },
                new Component
                {
                    Id = 42,
                    Category = "Накопитель SSD",
                    Name = "Crucial T700 1TB NVMe",
                    Specs = "M.2 PCIe 5.0, 12400/11800 MB/s, TLC",
                    ImageUrl = "https://ir.ozone.ru/s3/multimedia-1-3/c1000/7210716483.jpg"
                },
                new Component
                {
                    Id = 43,
                    Category = "Накопитель SSD",
                    Name = "Kingston A2000 500GB NVMe",
                    Specs = "M.2 PCIe 3.0, 2200/2000 MB/s, 3D TLC",
                    ImageUrl = "https://main-cdn.sbermegamarket.ru/big1/hlr-system/12/48/94/53/81/10/7/100027328194b0.jpg"
                },

                // ── БЛОК ПИТАНИЯ ─────────────────────────────
                new Component
                {
                    Id = 50,
                    Category = "Блок питания",
                    Name = "be quiet! Pure Power 12 M 750W",
                    Specs = "750W, 80+ Gold, ATX 3.0, модульный",
                    ImageUrl = "https://main-cdn.sbermegamarket.ru/big2/hlr-system/665/464/971/511/103/1/600011998527b1.jpeg"
                },
                new Component
                {
                    Id = 51,
                    Category = "Блок питания",
                    Name = "Corsair RM1000x 1000W",
                    Specs = "1000W, 80+ Gold, ATX 3.0, полностью модульный",
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/11562667/2a0000019585e8ce9602b4a58941bc4f41ab/orig"
                },
                new Component
                {
                    Id = 52,
                    Category = "Блок питания",
                    Name = "ASUS ROG Thor 850P2 850W",
                    Specs = "850W, 80+ Platinum, ATX 3.0, OLED дисплей",
                    ImageUrl = "https://ir.ozone.ru/s3/multimedia-s/c1000/6703235344.jpg"
                },
                new Component
                {
                    Id = 53,
                    Category = "Блок питания",
                    Name = "Seasonic Focus GX-650 650W",
                    Specs = "650W, 80+ Gold, ATX, полностью модульный",
                    ImageUrl = "https://ir.ozone.ru/s3/multimedia-m/c1000/6029722342.jpg"
                },

                // ── КОРПУС ───────────────────────────────────
                new Component
                {
                    Id = 60,
                    Category = "Корпус",
                    Name = "NZXT H7 Flow",
                    Specs = "Mid-Tower ATX, 2×USB-A 3.2, стекло, белый/чёрный",
                    ImageUrl = "https://cdn.salla.sa/KOPVE/QruCKwxyzM82GeDcsFODcRfpiiFZBNE5VbwtDZhc.png"
                },
                new Component
                {
                    Id = 61,
                    Category = "Корпус",
                    Name = "Fractal Design Meshify 2",
                    Specs = "Mid-Tower ATX, высокий поток воздуха, USB-C",
                    ImageUrl = "https://c.dns-shop.ru/thumb/st4/fit/300/300/c7ea096df39da83ef0588af70d236744/556fae0ad534e0180268f196b618b5a9bb1d622ee66863d25beaa168fc172bce.jpg"
                },
                new Component
                {
                    Id = 62,
                    Category = "Корпус",
                    Name = "Lian Li PC-O11 Dynamic EVO",
                    Specs = "Mid-Tower ATX/E-ATX, двойная камера, стекло",
                    ImageUrl = "https://microless.com/cdn/products/cd2aa1aa4c5b2c51cb9d6792203ae846-hi.jpg"
                },
                new Component
                {
                    Id = 63,
                    Category = "Корпус",
                    Name = "Deepcool CH510",
                    Specs = "Mid-Tower ATX, панель из закалённого стекла, USB-C",
                    ImageUrl = "https://cdn.deepcool.com/public/ProductFile/DEEPCOOL/Cases/CH510/Gallery/800X800/01.jpg?fm=webp&q=60"
                },

                // ── ОХЛАЖДЕНИЕ ───────────────────────────────
                new Component
                {
                    Id = 70,
                    Category = "Охлаждение",
                    Name = "Noctua NH-D15 chromax.black",
                    Specs = "Башенный, 2×140mm, TDP до 250W, AM4/AM5/LGA1700",
                    ImageUrl = "https://m.media-amazon.com/images/I/71a7oXlL0FL._AC_SL1500_.jpg"
                },
                new Component
                {
                    Id = 71,
                    Category = "Охлаждение",
                    Name = "be quiet! Dark Rock Pro 4",
                    Specs = "Башенный, 2×135mm, TDP до 250W, AM4/LGA1700",
                    ImageUrl = "https://c.dns-shop.ru/thumb/st1/fit/300/300/682584633c3d3ddd85648acc925bbcee/0c911e1e4a7f16151ca0e61cfa9ef83539344e48072ed9b99759107ff60fb6be.jpg"
                },
                new Component
                {
                    Id = 72,
                    Category = "Охлаждение",
                    Name = "Corsair iCUE H150i Elite LCD 360mm",
                    Specs = "СВО 360mm, 3×120mm, LCD экран, AM4/AM5/LGA1700",
                    ImageUrl = "https://microless.com/cdn/products/b8585fc2c0ef8cf9753b7f06fab0da34-hi.jpg"
                },
                new Component
                {
                    Id = 73,
                    Category = "Охлаждение",
                    Name = "DeepCool AK620",
                    Specs = "Башенный, 2×120mm, TDP до 260W, AM4/AM5/LGA1700",
                    ImageUrl = "https://cdn.citilink.ru/c550dqletFfAOumat4TvBDzzO981neNcD2eXEfDakoc/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/76f46220-e7ec-46a4-8eb8-57fb2999ba98.jpg"
                }
            );

            // ══════════════════════════════════════════════
            //  ОФФЕРЫ (4 на каждый компонент)
            // ══════════════════════════════════════════════
            modelBuilder.Entity<ProductOffer>().HasData(

                // ── AMD Ryzen 5 7600X (Id=1) ─────────────────
                new ProductOffer { Id = 1, ComponentId = 1, MerchantName = "Citilink", Price = 25900 },
                new ProductOffer { Id = 2, ComponentId = 1, MerchantName = "DNS", Price = 26500 },
                new ProductOffer { Id = 3, ComponentId = 1, MerchantName = "Ozon", Price = 25400 },
                new ProductOffer { Id = 4, ComponentId = 1, MerchantName = "Местный", Price = 24800 },

                // ── AMD Ryzen 7 7700X (Id=2) ─────────────────
                new ProductOffer { Id = 5, ComponentId = 2, MerchantName = "Citilink", Price = 34900 },
                new ProductOffer { Id = 6, ComponentId = 2, MerchantName = "DNS", Price = 35500 },
                new ProductOffer { Id = 7, ComponentId = 2, MerchantName = "Ozon", Price = 34200 },
                new ProductOffer { Id = 8, ComponentId = 2, MerchantName = "Местный", Price = 33500 },

                // ── Intel Core i5-13600K (Id=3) ──────────────
                new ProductOffer { Id = 9, ComponentId = 3, MerchantName = "Citilink", Price = 29900 },
                new ProductOffer { Id = 10, ComponentId = 3, MerchantName = "DNS", Price = 30500 },
                new ProductOffer { Id = 11, ComponentId = 3, MerchantName = "Ozon", Price = 29200 },
                new ProductOffer { Id = 12, ComponentId = 3, MerchantName = "Местный", Price = 28700 },

                // ── Intel Core i9-14900K (Id=4) ──────────────
                new ProductOffer { Id = 13, ComponentId = 4, MerchantName = "Citilink", Price = 62900 },
                new ProductOffer { Id = 14, ComponentId = 4, MerchantName = "DNS", Price = 64000 },
                new ProductOffer { Id = 15, ComponentId = 4, MerchantName = "Ozon", Price = 61500 },
                new ProductOffer { Id = 16, ComponentId = 4, MerchantName = "Местный", Price = 60000 },

                // ── ASUS ROG STRIX B650-A (Id=10) ────────────
                new ProductOffer { Id = 17, ComponentId = 10, MerchantName = "Citilink", Price = 23900 },
                new ProductOffer { Id = 18, ComponentId = 10, MerchantName = "DNS", Price = 24500 },
                new ProductOffer { Id = 19, ComponentId = 10, MerchantName = "Ozon", Price = 23400 },
                new ProductOffer { Id = 20, ComponentId = 10, MerchantName = "Местный", Price = 22800 },

                // ── MSI MAG B650 TOMAHAWK (Id=11) ────────────
                new ProductOffer { Id = 21, ComponentId = 11, MerchantName = "Citilink", Price = 19900 },
                new ProductOffer { Id = 22, ComponentId = 11, MerchantName = "DNS", Price = 20500 },
                new ProductOffer { Id = 23, ComponentId = 11, MerchantName = "Ozon", Price = 19200 },
                new ProductOffer { Id = 24, ComponentId = 11, MerchantName = "Местный", Price = 18700 },

                // ── GIGABYTE Z790 AORUS Elite (Id=12) ────────
                new ProductOffer { Id = 25, ComponentId = 12, MerchantName = "Citilink", Price = 32900 },
                new ProductOffer { Id = 26, ComponentId = 12, MerchantName = "DNS", Price = 33500 },
                new ProductOffer { Id = 27, ComponentId = 12, MerchantName = "Ozon", Price = 32200 },
                new ProductOffer { Id = 28, ComponentId = 12, MerchantName = "Местный", Price = 31500 },

                // ── ASRock B760M Pro RS (Id=13) ───────────────
                new ProductOffer { Id = 29, ComponentId = 13, MerchantName = "Citilink", Price = 11900 },
                new ProductOffer { Id = 30, ComponentId = 13, MerchantName = "DNS", Price = 12200 },
                new ProductOffer { Id = 31, ComponentId = 13, MerchantName = "Ozon", Price = 11500 },
                new ProductOffer { Id = 32, ComponentId = 13, MerchantName = "Местный", Price = 11000 },

                // ── Palit RTX 5060 Ti (Id=20) ────────────────
                new ProductOffer { Id = 33, ComponentId = 20, MerchantName = "Citilink", Price = 58900 },
                new ProductOffer { Id = 34, ComponentId = 20, MerchantName = "DNS", Price = 60000 },
                new ProductOffer { Id = 35, ComponentId = 20, MerchantName = "Ozon", Price = 57500 },
                new ProductOffer { Id = 36, ComponentId = 20, MerchantName = "Местный", Price = 56000 },

                // ── ASUS TUF RTX 4070 Super (Id=21) ──────────
                new ProductOffer { Id = 37, ComponentId = 21, MerchantName = "Citilink", Price = 64900 },
                new ProductOffer { Id = 38, ComponentId = 21, MerchantName = "DNS", Price = 66000 },
                new ProductOffer { Id = 39, ComponentId = 21, MerchantName = "Ozon", Price = 63500 },
                new ProductOffer { Id = 40, ComponentId = 21, MerchantName = "Местный", Price = 62000 },

                // ── MSI RX 7800 XT (Id=22) ────────────────────
                new ProductOffer { Id = 41, ComponentId = 22, MerchantName = "Citilink", Price = 49900 },
                new ProductOffer { Id = 42, ComponentId = 22, MerchantName = "DNS", Price = 51000 },
                new ProductOffer { Id = 43, ComponentId = 22, MerchantName = "Ozon", Price = 48500 },
                new ProductOffer { Id = 44, ComponentId = 22, MerchantName = "Местный", Price = 47500 },

                // ── Gigabyte RTX 4090 (Id=23) ────────────────
                new ProductOffer { Id = 45, ComponentId = 23, MerchantName = "Citilink", Price = 189900 },
                new ProductOffer { Id = 46, ComponentId = 23, MerchantName = "DNS", Price = 192000 },
                new ProductOffer { Id = 47, ComponentId = 23, MerchantName = "Ozon", Price = 187000 },
                new ProductOffer { Id = 48, ComponentId = 23, MerchantName = "Местный", Price = 185000 },

                // ── Kingston Fury Beast 32GB (Id=30) ─────────
                new ProductOffer { Id = 49, ComponentId = 30, MerchantName = "Citilink", Price = 8900 },
                new ProductOffer { Id = 50, ComponentId = 30, MerchantName = "DNS", Price = 9200 },
                new ProductOffer { Id = 51, ComponentId = 30, MerchantName = "Ozon", Price = 8600 },
                new ProductOffer { Id = 52, ComponentId = 30, MerchantName = "Местный", Price = 8400 },

                // ── G.Skill Trident Z5 32GB (Id=31) ──────────
                new ProductOffer { Id = 53, ComponentId = 31, MerchantName = "Citilink", Price = 14900 },
                new ProductOffer { Id = 54, ComponentId = 31, MerchantName = "DNS", Price = 15500 },
                new ProductOffer { Id = 55, ComponentId = 31, MerchantName = "Ozon", Price = 14400 },
                new ProductOffer { Id = 56, ComponentId = 31, MerchantName = "Местный", Price = 14000 },

                // ── Corsair Vengeance 64GB (Id=32) ───────────
                new ProductOffer { Id = 57, ComponentId = 32, MerchantName = "Citilink", Price = 19900 },
                new ProductOffer { Id = 58, ComponentId = 32, MerchantName = "DNS", Price = 20500 },
                new ProductOffer { Id = 59, ComponentId = 32, MerchantName = "Ozon", Price = 19200 },
                new ProductOffer { Id = 60, ComponentId = 32, MerchantName = "Местный", Price = 18800 },

                // ── Samsung 990 Pro 1TB (Id=40) ──────────────
                new ProductOffer { Id = 61, ComponentId = 40, MerchantName = "Citilink", Price = 9900 },
                new ProductOffer { Id = 62, ComponentId = 40, MerchantName = "DNS", Price = 10200 },
                new ProductOffer { Id = 63, ComponentId = 40, MerchantName = "Ozon", Price = 9600 },
                new ProductOffer { Id = 64, ComponentId = 40, MerchantName = "Местный", Price = 9300 },

                // ── WD Black SN850X 2TB (Id=41) ──────────────
                new ProductOffer { Id = 65, ComponentId = 41, MerchantName = "Citilink", Price = 16900 },
                new ProductOffer { Id = 66, ComponentId = 41, MerchantName = "DNS", Price = 17400 },
                new ProductOffer { Id = 67, ComponentId = 41, MerchantName = "Ozon", Price = 16500 },
                new ProductOffer { Id = 68, ComponentId = 41, MerchantName = "Местный", Price = 16000 },

                // ── Crucial T700 1TB (Id=42) ─────────────────
                new ProductOffer { Id = 69, ComponentId = 42, MerchantName = "Citilink", Price = 13900 },
                new ProductOffer { Id = 70, ComponentId = 42, MerchantName = "DNS", Price = 14300 },
                new ProductOffer { Id = 71, ComponentId = 42, MerchantName = "Ozon", Price = 13500 },
                new ProductOffer { Id = 72, ComponentId = 42, MerchantName = "Местный", Price = 13000 },

                // ── Kingston A2000 500GB (Id=43) ─────────────
                new ProductOffer { Id = 73, ComponentId = 43, MerchantName = "Citilink", Price = 4900 },
                new ProductOffer { Id = 74, ComponentId = 43, MerchantName = "DNS", Price = 5100 },
                new ProductOffer { Id = 75, ComponentId = 43, MerchantName = "Ozon", Price = 4700 },
                new ProductOffer { Id = 76, ComponentId = 43, MerchantName = "Местный", Price = 4500 },

                // ── be quiet! Pure Power 12 M 750W (Id=50) ──
                new ProductOffer { Id = 77, ComponentId = 50, MerchantName = "Citilink", Price = 10900 },
                new ProductOffer { Id = 78, ComponentId = 50, MerchantName = "DNS", Price = 11200 },
                new ProductOffer { Id = 79, ComponentId = 50, MerchantName = "Ozon", Price = 10600 },
                new ProductOffer { Id = 80, ComponentId = 50, MerchantName = "Местный", Price = 10200 },

                // ── Corsair RM1000x 1000W (Id=51) ────────────
                new ProductOffer { Id = 81, ComponentId = 51, MerchantName = "Citilink", Price = 21900 },
                new ProductOffer { Id = 82, ComponentId = 51, MerchantName = "DNS", Price = 22500 },
                new ProductOffer { Id = 83, ComponentId = 51, MerchantName = "Ozon", Price = 21200 },
                new ProductOffer { Id = 84, ComponentId = 51, MerchantName = "Местный", Price = 20800 },

                // ── ASUS ROG Thor 850W (Id=52) ────────────────
                new ProductOffer { Id = 85, ComponentId = 52, MerchantName = "Citilink", Price = 24900 },
                new ProductOffer { Id = 86, ComponentId = 52, MerchantName = "DNS", Price = 25500 },
                new ProductOffer { Id = 87, ComponentId = 52, MerchantName = "Ozon", Price = 24200 },
                new ProductOffer { Id = 88, ComponentId = 52, MerchantName = "Местный", Price = 23700 },

                // ── Seasonic Focus GX-650 (Id=53) ────────────
                new ProductOffer { Id = 89, ComponentId = 53, MerchantName = "Citilink", Price = 9900 },
                new ProductOffer { Id = 90, ComponentId = 53, MerchantName = "DNS", Price = 10200 },
                new ProductOffer { Id = 91, ComponentId = 53, MerchantName = "Ozon", Price = 9600 },
                new ProductOffer { Id = 92, ComponentId = 53, MerchantName = "Местный", Price = 9300 },

                // ── NZXT H7 Flow (Id=60) ─────────────────────
                new ProductOffer { Id = 93, ComponentId = 60, MerchantName = "Citilink", Price = 12900 },
                new ProductOffer { Id = 94, ComponentId = 60, MerchantName = "DNS", Price = 13400 },
                new ProductOffer { Id = 95, ComponentId = 60, MerchantName = "Ozon", Price = 12500 },
                new ProductOffer { Id = 96, ComponentId = 60, MerchantName = "Местный", Price = 12000 },

                // ── Fractal Design Meshify 2 (Id=61) ─────────
                new ProductOffer { Id = 97, ComponentId = 61, MerchantName = "Citilink", Price = 14900 },
                new ProductOffer { Id = 98, ComponentId = 61, MerchantName = "DNS", Price = 15400 },
                new ProductOffer { Id = 99, ComponentId = 61, MerchantName = "Ozon", Price = 14500 },
                new ProductOffer { Id = 100, ComponentId = 61, MerchantName = "Местный", Price = 14000 },

                // ── Lian Li O11 Dynamic EVO (Id=62) ──────────
                new ProductOffer { Id = 101, ComponentId = 62, MerchantName = "Citilink", Price = 17900 },
                new ProductOffer { Id = 102, ComponentId = 62, MerchantName = "DNS", Price = 18400 },
                new ProductOffer { Id = 103, ComponentId = 62, MerchantName = "Ozon", Price = 17500 },
                new ProductOffer { Id = 104, ComponentId = 62, MerchantName = "Местный", Price = 17000 },

                // ── Deepcool CH510 (Id=63) ────────────────────
                new ProductOffer { Id = 105, ComponentId = 63, MerchantName = "Citilink", Price = 6900 },
                new ProductOffer { Id = 106, ComponentId = 63, MerchantName = "DNS", Price = 7200 },
                new ProductOffer { Id = 107, ComponentId = 63, MerchantName = "Ozon", Price = 6600 },
                new ProductOffer { Id = 108, ComponentId = 63, MerchantName = "Местный", Price = 6300 },

                // ── Noctua NH-D15 (Id=70) ────────────────────
                new ProductOffer { Id = 109, ComponentId = 70, MerchantName = "Citilink", Price = 9900 },
                new ProductOffer { Id = 110, ComponentId = 70, MerchantName = "DNS", Price = 10200 },
                new ProductOffer { Id = 111, ComponentId = 70, MerchantName = "Ozon", Price = 9500 },
                new ProductOffer { Id = 112, ComponentId = 70, MerchantName = "Местный", Price = 9200 },

                // ── be quiet! Dark Rock Pro 4 (Id=71) ────────
                new ProductOffer { Id = 113, ComponentId = 71, MerchantName = "Citilink", Price = 8900 },
                new ProductOffer { Id = 114, ComponentId = 71, MerchantName = "DNS", Price = 9200 },
                new ProductOffer { Id = 115, ComponentId = 71, MerchantName = "Ozon", Price = 8600 },
                new ProductOffer { Id = 116, ComponentId = 71, MerchantName = "Местный", Price = 8300 },

                // ── Corsair iCUE H150i 360mm (Id=72) ─────────
                new ProductOffer { Id = 117, ComponentId = 72, MerchantName = "Citilink", Price = 22900 },
                new ProductOffer { Id = 118, ComponentId = 72, MerchantName = "DNS", Price = 23500 },
                new ProductOffer { Id = 119, ComponentId = 72, MerchantName = "Ozon", Price = 22200 },
                new ProductOffer { Id = 120, ComponentId = 72, MerchantName = "Местный", Price = 21800 },

                // ── DeepCool AK620 (Id=73) ────────────────────
                new ProductOffer { Id = 121, ComponentId = 73, MerchantName = "Citilink", Price = 4900 },
                new ProductOffer { Id = 122, ComponentId = 73, MerchantName = "DNS", Price = 5100 },
                new ProductOffer { Id = 123, ComponentId = 73, MerchantName = "Ozon", Price = 4700 },
                new ProductOffer { Id = 124, ComponentId = 73, MerchantName = "Местный", Price = 4500 }
            );
        }
    }
}