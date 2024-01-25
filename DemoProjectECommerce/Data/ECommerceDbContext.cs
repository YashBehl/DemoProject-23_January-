using DemoProjectECommerce.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Product> tbl_Products { get; set; }
    }
}
