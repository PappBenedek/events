namespace DataAccess.Entities
{
    public class Event : BaseEntity
    {

        public string Name { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }

        public int BaseServiceFeeId { get; set; }
        public ServiceFee BaseServiceFee { get; set; } = null!;
    }
}
