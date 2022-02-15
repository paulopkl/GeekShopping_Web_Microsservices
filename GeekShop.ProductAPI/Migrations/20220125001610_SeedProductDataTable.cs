using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShop.ProductAPI.Migrations
{
    public partial class SeedProductDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TProduct",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[,]
                {
                    { 2L, "T-Shirt", "Uma camisa dahora", "", "camisa jurassic park", 69.9m },
                    { 3L, "T-Shirt", "Uma camisa dahora", "", "camisa jurassic park", 69.9m },
                    { 4L, "T-Shirt", "Uma camisa dahora", "", "camisa jurassic park", 69.9m },
                    { 5L, "T-Shirt", "Uma camisa dahora", "", "camisa jurassic park", 69.9m },
                    { 6L, "T-Shirt", "Uma camisa dahora", "", "camisa jurassic park", 69.9m },
                    { 7L, "T-Shirt", "Uma camisa dahora", "", "camisa jurassic park", 69.9m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 7L);
        }
    }
}
