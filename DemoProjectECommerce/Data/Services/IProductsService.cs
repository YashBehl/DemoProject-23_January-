using DemoProjectECommerce.productCategory.Base;
using DemoProjectECommerce.Models.Domain;

namespace DemoProjectECommerce.productCategory.Services
{
    public interface IProductsService:IEntityBaseRepository<Product>
    {
        Task<Product> getProductByIdAsync(Guid id);
        Task addNewProductAsync(NewProductViewModel data);

        Task updateProductAsync(NewProductViewModel data);
    }
}
