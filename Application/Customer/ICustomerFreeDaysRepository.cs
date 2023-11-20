using Domain.Customer;

namespace Application.Customer
{
    public interface ICustomerFreeDaysRepository
    {
        Task<CustomerFreeDays?> GetCustomerFreeDays(int customerId);
    }
}
