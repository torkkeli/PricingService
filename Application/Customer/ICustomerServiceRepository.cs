using Domain.Customer;

namespace Application.Customer
{
    public interface ICustomerServiceRepository
    {
        Task<CustomerService?> GetCustomerService(int customerId, PriceServiceType priceServiceType);
    }
}
