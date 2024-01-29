using DemoProjectECommerce.Data.Base;
using DemoProjectECommerce.Models.Domain;

namespace DemoProjectECommerce.Data.Services
{
    public interface IProductsService:IEntityBaseRepository<Product>
    {
    }
}
