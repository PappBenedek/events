namespace DataAccess.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; } = null!;

        public int EventId { get; set; }

        public Event Event { get; set; } = null!;

        public int? ServiceFeeId { get; set; }

        public ServiceFee? ServiceFee { get; set; }
    }
}
