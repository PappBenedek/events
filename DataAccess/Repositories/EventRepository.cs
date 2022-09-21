using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IEventRepository
    {
        public Task<List<Event>> GetAllEventWithProducts();

        public Task<Event?> GetEventByIdWithProducts(int id);

        public Task<ServiceFee?> GetCorrespondingServiceFeeById(int id);
    }
    public class EventRepository : IEventRepository
    {
        private readonly EventDBContext _dbContext;

        public EventRepository(EventDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Event>> GetAllEventWithProducts()
        {
            return _dbContext.Events.Include(e => e.Products).ToListAsync();
        }

        public Task<Event?> GetEventByIdWithProducts(int id)
        {
            return _dbContext.Events
                .Include(e => e.Products)
                .Select(e => new Event
                {
                    Id = e.Id,
                    AddedDate = e.AddedDate,
                    Products = e.Products!.ToArray()
                })
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ServiceFee?> GetCorrespondingServiceFeeById(int id)
        {
            return await _dbContext.Events.Include(e => e.BaseServiceFee)
                .Where(e => e.Id == id)
                .Select(e => e.BaseServiceFee)
                .FirstOrDefaultAsync();

        }
    }
}
