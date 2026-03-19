using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Models;
using PCBuilder.Data;

namespace PCBuilder.Data
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated(); // БЕЗ EnsureDeleted — иначе аккаунты удаляются при каждом запуске!
        }

        public DbSet<Component>  Components  { get; set; } = null!;
        public DbSet<DataSource> DataSources { get; set; } = null!;
        public DbSet<SavedBuild> SavedBuilds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ОБЯЗАТЕЛЬНО для Identity

            modelBuilder.Entity<ProductOffer>()
                .HasOne(o => o.Component)
                .WithMany(c => c.Offers)
                .HasForeignKey(o => o.ComponentId);

            modelBuilder.Entity<DataSource>().ToTable("DataSources");
            modelBuilder.Entity<SavedBuild>().HasIndex(b => b.UserId);

            // Seed-данные компонентов (сокращённо — те же что были)
            modelBuilder.Entity<Component>().HasData(
                new Component { Id=1,  Category="Процессор",         Name="AMD Ryzen 5 7600X",              Specs="6 ядер / 12 потоков, 4.7–5.3 GHz, TDP 105W, AM5", Socket="AM5",     ImageUrl="https://m.media-amazon.com/images/I/71USsj3Pc6L._AC_SL1000_.jpg" },
                new Component { Id=2,  Category="Процессор",         Name="AMD Ryzen 7 7700X",              Specs="8 ядер / 16 потоков, 4.5–5.4 GHz, TDP 105W, AM5", Socket="AM5",     ImageUrl="https://m.media-amazon.com/images/I/61asdBR8VQL._AC_SL1500_.jpg" },
                new Component { Id=3,  Category="Процессор",         Name="Intel Core i5-13600K",           Specs="14 ядер / 20 потоков, 3.5–5.1 GHz, TDP 125W, LGA1700", Socket="LGA1700", ImageUrl="https://m.media-amazon.com/images/I/61l7eshifLL._AC_SL1500_.jpg" },
                new Component { Id=4,  Category="Процессор",         Name="Intel Core i9-14900K",           Specs="24 ядра / 32 потока, 3.2–6.0 GHz, TDP 125W, LGA1700", Socket="LGA1700", ImageUrl="https://m.media-amazon.com/images/I/61l7eshifLL._AC_SL1500_.jpg" },
                new Component { Id=10, Category="Материнская плата", Name="ASUS ROG STRIX B650-A Gaming WiFi", Specs="AM5, DDR5, ATX, PCIe 5.0, Wi-Fi 6E", Socket="AM5",     ImageUrl="https://m.media-amazon.com/images/I/81xS6TJLQAL._AC_SL1500_.jpg" },
                new Component { Id=11, Category="Материнская плата", Name="MSI MAG B650 TOMAHAWK WIFI",        Specs="AM5, DDR5, ATX, Wi-Fi 6E, 2.5G LAN", Socket="AM5",     ImageUrl="https://m.media-amazon.com/images/I/81wCyMKkCCL._AC_SL1500_.jpg" },
                new Component { Id=12, Category="Материнская плата", Name="GIGABYTE Z790 AORUS Elite AX",      Specs="LGA1700, DDR5, ATX, Wi-Fi 6E, TB4",  Socket="LGA1700", ImageUrl="https://m.media-amazon.com/images/I/91YCidnxJyL._AC_SL1500_.jpg" },
                new Component { Id=13, Category="Материнская плата", Name="ASRock B760M Pro RS",               Specs="LGA1700, DDR5, mATX, PCIe 4.0",      Socket="LGA1700", ImageUrl="https://m.media-amazon.com/images/I/71bRl5k3FaL._AC_SL1500_.jpg" },
                new Component { Id=20, Category="Видеокарта",        Name="Palit RTX 5060 Ti 16GB",         Specs="16GB GDDR7, 4608 ядер, TDP 180W",   ImageUrl="https://ir.ozone.ru/s3/multimedia-1/6884562541.jpg" },
                new Component { Id=21, Category="Видеокарта",        Name="ASUS TUF Gaming RTX 4070 Super", Specs="12GB GDDR6X, 7168 ядер, TDP 220W",  ImageUrl="https://m.media-amazon.com/images/I/81JDFB1GBNL._AC_SL1500_.jpg" },
                new Component { Id=22, Category="Видеокарта",        Name="MSI Gaming X RX 7800 XT",        Specs="16GB GDDR6, 3840 ядер, TDP 263W",   ImageUrl="https://m.media-amazon.com/images/I/71AtyQFp8AL._AC_SL1500_.jpg" },
                new Component { Id=23, Category="Видеокарта",        Name="Gigabyte RTX 4090 Gaming OC",    Specs="24GB GDDR6X, 16384 ядер, TDP 450W", ImageUrl="https://m.media-amazon.com/images/I/81sTlZomNPL._AC_SL1500_.jpg" },
                new Component { Id=30, Category="Оперативная память", Name="Kingston Fury Beast DDR5-5200 32GB",   Specs="2×16GB, DDR5-5200, CL40",      ImageUrl="https://m.media-amazon.com/images/I/61U7qQ9YIPL._AC_SL1500_.jpg" },
                new Component { Id=31, Category="Оперативная память", Name="G.Skill Trident Z5 RGB DDR5-6000 32GB",Specs="2×16GB, DDR5-6000, CL30, RGB", ImageUrl="https://m.media-amazon.com/images/I/71pVzT2l6jL._AC_SL1500_.jpg" },
                new Component { Id=32, Category="Оперативная память", Name="Corsair Vengeance DDR5-5600 64GB",     Specs="2×32GB, DDR5-5600, CL36",      ImageUrl="https://m.media-amazon.com/images/I/71XVoM7DaGL._AC_SL1500_.jpg" },
                new Component { Id=40, Category="Накопитель SSD", Name="Samsung 990 Pro 1TB NVMe",       Specs="M.2 PCIe 4.0, 7450/6900 MB/s", ImageUrl="https://m.media-amazon.com/images/I/71vTlqq2YQL._AC_SL1500_.jpg" },
                new Component { Id=41, Category="Накопитель SSD", Name="WD Black SN850X 2TB NVMe",       Specs="M.2 PCIe 4.0, 7300/6600 MB/s", ImageUrl="https://m.media-amazon.com/images/I/61ghFqMHnGL._AC_SL1500_.jpg" },
                new Component { Id=42, Category="Накопитель SSD", Name="Crucial T700 1TB NVMe PCIe 5.0", Specs="M.2 PCIe 5.0, 12400/11800 MB/s",ImageUrl="https://m.media-amazon.com/images/I/71TW2p9DIOL._AC_SL1500_.jpg" },
                new Component { Id=43, Category="Накопитель SSD", Name="Kingston A2000 500GB NVMe",      Specs="M.2 PCIe 3.0, 2200/2000 MB/s",  ImageUrl="https://m.media-amazon.com/images/I/61asdBR8VQL._AC_SL1500_.jpg" },
                new Component { Id=50, Category="Блок питания", Name="be quiet! Pure Power 12 M 750W", Specs="750W, 80+ Gold, ATX 3.0, модульный",            ImageUrl="https://m.media-amazon.com/images/I/61a-hAfGUsL._AC_SL1500_.jpg" },
                new Component { Id=51, Category="Блок питания", Name="Corsair RM1000x 1000W",          Specs="1000W, 80+ Gold, ATX 3.0, полностью модульный", ImageUrl="https://m.media-amazon.com/images/I/61V73GR37VL._AC_SL1500_.jpg" },
                new Component { Id=52, Category="Блок питания", Name="ASUS ROG Thor 850P2 850W",       Specs="850W, 80+ Platinum, ATX 3.0, OLED",             ImageUrl="https://m.media-amazon.com/images/I/71G6GlVRVxL._AC_SL1500_.jpg" },
                new Component { Id=53, Category="Блок питания", Name="Seasonic Focus GX-650 650W",     Specs="650W, 80+ Gold, ATX, полностью модульный",       ImageUrl="https://m.media-amazon.com/images/I/71a7oXlL0FL._AC_SL1500_.jpg" },
                new Component { Id=60, Category="Корпус", Name="NZXT H7 Flow",               Specs="Mid-Tower ATX, 2×USB-A 3.2, стекло",         ImageUrl="https://m.media-amazon.com/images/I/71fslXSULvL._AC_SL1500_.jpg" },
                new Component { Id=61, Category="Корпус", Name="Fractal Design Meshify 2",   Specs="Mid-Tower ATX, высокий поток воздуха, USB-C", ImageUrl="https://m.media-amazon.com/images/I/71Iqpf0CGQL._AC_SL1500_.jpg" },
                new Component { Id=62, Category="Корпус", Name="Lian Li PC-O11 Dynamic EVO", Specs="Mid-Tower ATX/E-ATX, двойная камера, стекло",  ImageUrl="https://m.media-amazon.com/images/I/81K2dD5lERL._AC_SL1500_.jpg" },
                new Component { Id=63, Category="Корпус", Name="Deepcool CH510",              Specs="Mid-Tower ATX, стекло, USB-C",                ImageUrl="https://m.media-amazon.com/images/I/71cRiMxXfRL._AC_SL1500_.jpg" },
                new Component { Id=70, Category="Охлаждение", Name="Noctua NH-D15 chromax.black",        Specs="Башенный, 2×140mm, TDP до 250W",  ImageUrl="https://m.media-amazon.com/images/I/71a7oXlL0FL._AC_SL1500_.jpg" },
                new Component { Id=71, Category="Охлаждение", Name="be quiet! Dark Rock Pro 4",          Specs="Башенный, 2×135mm, TDP до 250W",  ImageUrl="https://m.media-amazon.com/images/I/81wCyMKkCCL._AC_SL1500_.jpg" },
                new Component { Id=72, Category="Охлаждение", Name="Corsair iCUE H150i Elite LCD 360mm", Specs="СВО 360mm, 3×120mm, LCD экран",   ImageUrl="https://m.media-amazon.com/images/I/71TW2p9DIOL._AC_SL1500_.jpg" },
                new Component { Id=73, Category="Охлаждение", Name="DeepCool AK620",                    Specs="Башенный, 2×120mm, TDP до 260W",  ImageUrl="https://m.media-amazon.com/images/I/61U7qQ9YIPL._AC_SL1500_.jpg" }
            );
        }
    }
}
