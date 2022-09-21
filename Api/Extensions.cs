using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public static class Extensions
    {
        public static void PopulateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EventDBContext>();

            db.Database.Migrate();

            var defaultServiceFeeForLondon = new ServiceFee
            {
                AddedDate = DateTime.Now,
                Amount = 15,
                Currency = "EUR"
            };
            var defaultServiceFeeForBudapest = new ServiceFee
            {
                AddedDate = DateTime.Now,
                Amount = 10,
                Currency = "EUR"
            };
            var defaultEvents = new List<Event>
            {
                new()
                {
                    AddedDate = DateTime.Now,
                    BaseServiceFee = defaultServiceFeeForLondon,
                    BaseServiceFeeId = defaultServiceFeeForLondon.Id,
                    Name = "Ndc london"
                },
                new()
                {
                    AddedDate = DateTime.Now,
                    BaseServiceFee = defaultServiceFeeForBudapest,
                    BaseServiceFeeId = defaultServiceFeeForBudapest.Id,
                    Name = "Ndc Budapest"
                }
            };
            var defaultProducts = new List<Product>
            {
                new()
                {
                    AddedDate = DateTime.Now,
                    Event = defaultEvents[1],
                    EventId = defaultEvents[1].Id,
                    Name = ".NET gc",
                    ServiceFee = new ()
                    {
                        Amount = 8,
                        Currency = "EUR"
                    }
                },
                new()
                {
                    AddedDate = DateTime.Now,
                    Event = defaultEvents[1],
                    EventId = defaultEvents[1].Id,
                    Name = ".NET gc2"
                },
                new()
                {
                    AddedDate = DateTime.Now,
                    Event = defaultEvents[0],
                    EventId = defaultEvents[0].Id,
                    Name = ".NET gc3",
                    ServiceFee = new ()
                    {
                        Amount = 12,
                        Currency = "EUR"
                    }
                },
                new()
                {
                    AddedDate = DateTime.Now,
                    Event = defaultEvents[0],
                    EventId = defaultEvents[0].Id,
                    Name = ".NET gc4"
                }
            };
            db.Products.AddRange(defaultProducts);
            db.Events.AddRange(defaultEvents);
            db.SaveChanges();
        }
    }
}
