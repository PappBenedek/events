using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public static class Extensions
    {
        public static void MapEvent(this EntityTypeBuilder<Event> entityBuilder)
        {
            entityBuilder.HasKey(ev => ev.Id);
            entityBuilder.Property(ev => ev.Name).IsRequired();
            entityBuilder.HasOne<ServiceFee>(ev => ev.BaseServiceFee)
                .WithMany(fee => fee.Events)
                .HasForeignKey(ev => ev.BaseServiceFeeId);
        }

        public static void MapProduct(this EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.HasKey(product => product.Id);
            entityBuilder.Property(product => product.Name).IsRequired();
            entityBuilder.HasOne<Event>(product => product.Event)
                .WithMany(ev => ev.Products)
                .HasForeignKey(product => product.EventId);
            entityBuilder.HasOne<ServiceFee>(product => product.ServiceFee)
                .WithMany(fee => fee.Products)
                .HasForeignKey(product => product.ServiceFeeId);
        }

        public static void MapServiceFeeMap(this EntityTypeBuilder<ServiceFee> entityBuilder)
        {
            entityBuilder.HasKey(fee => fee.Id);
            entityBuilder.Property(fee => fee.Amount).IsRequired();
            entityBuilder.Property(fee => fee.Currency).IsRequired();
        }
    }
}
