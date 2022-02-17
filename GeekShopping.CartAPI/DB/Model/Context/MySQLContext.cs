﻿using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.DB.Model.Context
{
    public class MySQLContext : DbContext
    {        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<CartHeader> CartHeaders { get; set; }
    }
}
