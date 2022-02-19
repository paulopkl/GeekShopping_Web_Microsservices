using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.DB.Model.Context
{

    public class MySQLContext : DbContext
    {        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public virtual DbSet<EmailLog> Emails { get; set; }
    }
}
