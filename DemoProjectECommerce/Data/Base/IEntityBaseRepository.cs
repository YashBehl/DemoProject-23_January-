namespace DemoProjectECommerce.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase,new()
    {
        Task<IEnumerable<T>> getAllAsync();
        Task<T> getByIdAsync(Guid id);
        Task addAsync(T entity);
        Task updateAsync(Guid id, T entity);
        Task deleteAsync(Guid id);
    }
}
