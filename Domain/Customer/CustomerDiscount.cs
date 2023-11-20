namespace Domain.Customer
{
    public class CustomerDiscount
    {
        public int CustomerDiscountId { get; set; }

        public int CustomerServiceId { get; set; }

        public decimal Discount { get; set; }

        public PriceServiceType PriceServiceType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
