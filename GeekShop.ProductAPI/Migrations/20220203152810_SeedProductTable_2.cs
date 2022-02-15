using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShop.ProductAPI.Migrations
{
    public partial class SeedProductTable_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TProduct",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[,]
                {
                    { 
                        22L, 
                        "T-Shirt", 
                        "Uma camisa do Jurassic Park muito estilosa", 
                        "https://raw.githubusercontent.com/paulopkl/microsservices-dotnet6/master/ShoppingImages/2_no_internet.jpg", 
                        "camisa jurassic park", 
                        69.9m 
                    },
                    { 
                        23L, 
                        "Helmet", 
                        "Um bonito capacete do dart vader com uma cor linda", 
                        "https://raw.githubusercontent.com/paulopkl/microsservices-dotnet6/master/ShoppingImages/3_vader.jpg", 
                        "Capacete Dark Vader", 
                        109.9m
                    },
                    { 
                        24L, 
                        "T-Shirt", 
                        "Um boneco jeday da cor branca muito foda", 
                        "https://raw.githubusercontent.com/paulopkl/microsservices-dotnet6/master/ShoppingImages/4_storm_tropper.jpg", 
                        "Boneco Jeday", 
                        59.9m 
                    },
                    { 
                        25L, 
                        "T-Shirt",
                        "Um verdadeiro gamer que se preste deve usar esta camisa", 
                        "https://raw.githubusercontent.com/paulopkl/microsservices-dotnet6/master/ShoppingImages/5_100_gamer.jpg", 
                        "Camisa Gamer", 
                        69.9m 
                    },
                    { 
                        26L, 
                        "T-Shirt",
                        "Apenas amantes do espaço tem o direito de vestir tal maravilha", 
                        "https://raw.githubusercontent.com/paulopkl/microsservices-dotnet6/master/ShoppingImages/6_spacex.jpg", 
                        "camisa spacex", 
                        79.9m 
                    },
                    { 
                        27L, 
                        "T-Shirt", 
                        "Programadores em geral não podem ficar sem possuir esta camisa, está no DNA dos verdadeiros codificadores", 
                        "https://raw.githubusercontent.com/paulopkl/microsservices-dotnet6/master/ShoppingImages/7_coffee.jpg", 
                        "camisa coffe", 
                        99.9m 
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 22L
            );

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 23L
            );

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 24L
            );

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 25L
            );

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 26L
            );

            migrationBuilder.DeleteData(
                table: "TProduct",
                keyColumn: "id",
                keyValue: 27L
            );
        }
    }
}
