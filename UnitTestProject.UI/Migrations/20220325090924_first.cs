using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitTestProject.UI.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Product1", 1000m, 100 },
                    { 2, "Product2", 2000m, 100 },
                    { 3, "Product3", 3000m, 100 },
                    { 4, "Product4", 4000m, 100 },
                    { 5, "Product4", 5000m, 100 },
                    { 6, "Product5", 6000m, 100 },
                    { 7, "Product6", 7000m, 100 },
                    { 8, "Product7", 8000m, 100 },
                    { 9, "Product8", 9000m, 100 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
