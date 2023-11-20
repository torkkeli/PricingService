using Application.Customer;
using Domain.Customer;

namespace Infrastructure.Repositories.Customer
{
    public class CustomerDiscountRepository : ICustomerDiscountRepository
    {
        private readonly List<CustomerDiscount> discounts;

        public CustomerDiscountRepository()
        {
            discounts =
            [
                new()
                {
                    CustomerDiscountId = 1,
                    CustomerServiceId = 1,
                    Discount = 0.2m,
                    PriceServiceType = PriceServiceType.A
                },
                new()
                {
                    CustomerDiscountId = 1,
                    CustomerServiceId = 1,
                    Discount = 0.15m,
                    PriceServiceType = PriceServiceType.B
                },
                new()
                {
                    CustomerDiscountId = 1,
                    CustomerServiceId = 2,
                    Discount = 0.4m,
                    PriceServiceType = PriceServiceType.C
                }
            ];
        }

        public async Task<IEnumerable<CustomerDiscount>> GetCustomerDiscounts(int customerServiceId)
        {
            return await Task.FromResult(discounts.Where(d => d.CustomerServiceId == customerServiceId).ToList());
        }
    }
}
