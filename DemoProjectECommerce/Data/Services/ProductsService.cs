using DemoProjectECommerce.Data.Base;
using DemoProjectECommerce.Models.Domain;

namespace DemoProjectECommerce.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        public ProductsService(ECommerceDbContext context):base(context)
        {
            
        }
    }
}
