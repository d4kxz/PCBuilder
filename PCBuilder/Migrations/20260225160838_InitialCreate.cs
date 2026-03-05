using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PCBuilder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Socket = table.Column<string>(type: "TEXT", nullable: false),
                    Cores = table.Column<int>(type: "INTEGER", nullable: false),
                    Threads = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseFrequency = table.Column<double>(type: "REAL", nullable: false),
                    TDP = table.Column<int>(type: "INTEGER", nullable: false),
                    HasIGPU = table.Column<bool>(type: "INTEGER", nullable: false),
                    PerformanceScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "GPUs");
        }
    }
}
