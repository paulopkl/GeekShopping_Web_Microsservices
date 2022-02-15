using Microsoft.EntityFrameworkCore;

namespace GeekShop.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() {}
        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 2,
                    Name = "camisa jurassic park",
                    Price = new decimal(69.9),
                    Description = "Uma camisa dahora",
                    ImageURL = "",
                    CategoryName = "T-Shirt",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                    {
                        Id = 3,
                        Name = "camisa jurassic park",
                        Price = new decimal(69.9),
                        Description = "Uma camisa dahora",
                        ImageURL = "",
                        CategoryName = "T-Shirt",
                    }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 4,
                    Name = "camisa jurassic park",
                    Price = new decimal(69.9),
                    Description = "Uma camisa dahora",
                    ImageURL = "",
                    CategoryName = "T-Shirt",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 5,
                    Name = "camisa jurassic park",
                    Price = new decimal(69.9),
                    Description = "Uma camisa dahora",
                    ImageURL = "",
                    CategoryName = "T-Shirt",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 6,
                    Name = "camisa jurassic park",
                    Price = new decimal(69.9),
                    Description = "Uma camisa dahora",
                    ImageURL = "",
                    CategoryName = "T-Shirt",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 7,
                    Name = "camisa jurassic park",
                    Price = new decimal(69.9),
                    Description = "Uma camisa dahora",
                    ImageURL = "",
                    CategoryName = "T-Shirt",
                }
            );
        }
    }
}
