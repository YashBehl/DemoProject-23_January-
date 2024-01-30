using DemoProjectECommerce.Data.Base;
using DemoProjectECommerce.Models.Domain;

namespace DemoProjectECommerce.Data.Services
{
    public interface IProductsService:IEntityBaseRepository<Product>
    {
        Task<Product> getProductByIdAsync(Guid id);
        Task addNewProductAsync(NewProductViewModel data);

        Task updateProductAsync(NewProductViewModel data);
    }
}
