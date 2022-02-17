using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.DB.Model.Context
{
    public class MySQLContext : DbContext
    {        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public virtual DbSet<OrderDetail> Details { get; set; }
        public virtual DbSet<OrderHeader> Headers { get; set; }
    }
}
