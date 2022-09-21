namespace DataAccess.Entities
{
    public class ServiceFee : BaseEntity
    {
        public string Currency { get; set; } = "eur";

        public int Amount { get; set; } = 10;

        public ICollection<Event> Events { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;
    }
}
