using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EventDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ServiceFee> ServiceFees { get; set; }

        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>().MapEvent();
            modelBuilder.Entity<Product>().MapProduct();
            modelBuilder.Entity<ServiceFee>().MapServiceFeeMap();
        }

    }
}
