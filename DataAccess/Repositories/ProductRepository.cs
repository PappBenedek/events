using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProductByEventIdWithFees(int eventId);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly EventDBContext _dbContext;

        public ProductRepository(EventDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProductByEventIdWithFees(int eventId)
        {
            var products = await _dbContext.Products
                .Include(p => p.ServiceFee)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    EventId = p.EventId,
                    ServiceFee = p.ServiceFee,
                    ServiceFeeId = p.ServiceFeeId,
                })
                .Where(p => p.EventId == eventId).ToListAsync();

            foreach (var product in products)
            {
                if (product.ServiceFee is null)
                {
                    var correspondingEvent = await _dbContext.Events.Select(e => new{e.Id,e.BaseServiceFeeId})
                        .FirstOrDefaultAsync(e => e.Id == eventId);
                    var actualFee =
                        await _dbContext.ServiceFees.FirstOrDefaultAsync(f =>
                            f.Id == correspondingEvent!.BaseServiceFeeId);
                    product.ServiceFee = actualFee;
                }
            }

            return products;
        }
    }
}
