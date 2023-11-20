namespace Domain.Customer
{
    public class CustomerService
    {
        public int CustomerServiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }

        public decimal? Price { get; set; }

        public PriceServiceType PriceServiceType { get; set; }
    }
}
