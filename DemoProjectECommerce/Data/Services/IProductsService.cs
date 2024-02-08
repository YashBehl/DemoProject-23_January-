using DemoProjectECommerce.productCategory.Base;
using DemoProjectECommerce.productCategory.ViewModels;
using DemoProjectECommerce.Models.Domain;
using System.Linq;


namespace DemoProjectECommerce.productCategory.Services
{
    public interface IProductsService:IEntityBaseRepository<Product>
    {
        Task<Product> getProductByIdAsync(Guid id);
        Task addNewProductAsync(NewProductViewModel data);

        Task UpdateProductAsync(NewProductViewModel data);
    }
}
