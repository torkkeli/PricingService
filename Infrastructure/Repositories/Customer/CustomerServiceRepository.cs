using Application.Customer;
using Domain.Customer;

namespace Infrastructure.Repositories.Customer
{
    public class CustomerServiceRepository : ICustomerServiceRepository
    {
        private readonly List<CustomerService> customerServices;

        public CustomerServiceRepository()
        {
            customerServices =
            [
                new CustomerService
                {
                    CustomerServiceId = 1,
                    StartDate = new DateTime(2023, 11, 19),
                    CustomerId = 2,
                    PriceServiceType = PriceServiceType.A,
                    Price = 0.3m
                },
                new CustomerService
                {
                    CustomerServiceId = 1,
                    StartDate = new DateTime(2024, 11, 19),
                    CustomerId = 2,
                    PriceServiceType = PriceServiceType.B,
                    Price = 0.3m
                }
            ];
        }

        public async Task<CustomerService?> GetCustomerService(int customerId, PriceServiceType priceServiceType)
        {
            return await Task.FromResult(customerServices.SingleOrDefault(p => p.CustomerId == customerId && p.PriceServiceType == priceServiceType));
        }
    }
}
