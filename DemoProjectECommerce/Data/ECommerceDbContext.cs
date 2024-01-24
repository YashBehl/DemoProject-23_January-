using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        // public DbSet<>  { get; set; }
    }
}
