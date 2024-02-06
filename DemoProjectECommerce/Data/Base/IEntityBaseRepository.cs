using System.Linq.Expressions;

namespace DemoProjectECommerce.productCategory.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase,new()
    {
        Task<IEnumerable<T>> getAllAsync();
        //Task<IEnumerable<T>> getAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> getByIdAsync(Guid id);
        Task addAsync(T entity);
        Task updateAsync(Guid id, T entity);
        Task deleteAsync(Guid id);
    }
}
