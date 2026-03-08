using PCBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace PCBuilder.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted(); // ВНИМАНИЕ: Удалит старую базу, чтобы применить новые HasData
            Database.EnsureCreated();
        }

        public DbSet<Component> Components { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOffer>()
                .HasOne(o => o.Component)
                .WithMany(c => c.Offers)
                .HasForeignKey(o => o.ComponentId);

            modelBuilder.Entity<Component>().HasData(
                new Component
                {
                    Id = 1,
                    Category = "Процессор",
                    Name = "AMD Ryzen 5 7600X",
                    OzonSku = "7600", // ДОБАВЛЕНО
                    Specs = "6 ядер, AM5",
                    Socket = "AM5",
                    ImageUrl = "https://m.media-amazon.com/images/I/71USsj3Pc6L._AC_SL1000_.jpg"
                },
                new Component
                {
                    Id = 2,
                    Category = "Видеокарта",
                    Name = "Palit RTX 5060 Ti",
                    OzonSku = "2080175880", // ДОБАВЛЕНО
                    Specs = "8GB GDDR6",
                    ImageUrl = "https://ir.ozone.ru/s3/multimedia-1/6884562541.jpg"
                }
            );
            //

            modelBuilder.Entity<ProductOffer>().HasData(
                new ProductOffer { Id = 1, ComponentId = 1, MerchantName = "Citilink", Price = 25900 },
                new ProductOffer { Id = 2, ComponentId = 1, MerchantName = "DNS", Price = 27000 },
                new ProductOffer { Id = 3, ComponentId = 2, MerchantName = "DNS", Price = 61000 }
            );
        }
    }
}