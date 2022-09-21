using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> 
        where T : BaseEntity 
    {
        private readonly EventDBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(EventDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public Task<T?> GetById(int id)
        {
            return _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(T entity)
        {
            entity.AddedDate = DateTime.Now;
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id);
            if (existingEntity is null)
            {
                throw new ArgumentException("Cannot find the existing entity");
            }

            existingEntity = entity;
            existingEntity.ModifiedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id);
            if (existingEntity is null)
            {
                throw new ArgumentException("Cannot find the existing entity");
            }

            _dbSet.Remove(existingEntity);
            await _dbContext.SaveChangesAsync();
        }

    }
}
