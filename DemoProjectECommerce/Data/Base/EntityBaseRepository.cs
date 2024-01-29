﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoProjectECommerce.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ECommerceDbContext _context;
        public EntityBaseRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task addAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task deleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.productId == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public async Task<IEnumerable<T>> getAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> getByIdAsync(Guid id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(n => n.productId == id);
            return result;
        }

        public async Task updateAsync(Guid id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }
    }
}