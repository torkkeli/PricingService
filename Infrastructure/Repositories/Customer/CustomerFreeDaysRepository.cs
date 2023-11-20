using Application.Customer;
using Domain.Customer;

namespace Infrastructure.Repositories.Customer
{
    public class CustomerFreeDaysRepository : ICustomerFreeDaysRepository
    {
        private readonly List<CustomerFreeDays> freeDays;

        public CustomerFreeDaysRepository()
        {
            freeDays =
            [
                new()
                {
                    CustomerFreeDaysId = 1,
                    CustomerId = 1,
                    NumberOfFreeDays = 1
                },
                new()
                {
                    CustomerFreeDaysId = 2,
                    CustomerId = 2,
                    NumberOfFreeDays = 10
                }
            ];
        }

        public async Task<CustomerFreeDays?> GetCustomerFreeDays(int customerId)
        {
            return await Task.FromResult(freeDays.SingleOrDefault(d => d.CustomerId == customerId));
        }
    }
}
