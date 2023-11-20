using Domain.Customer;

namespace Application.Customer
{
    public interface ICustomerDiscountRepository
    {
        Task<IEnumerable<CustomerDiscount>> GetCustomerDiscounts(int customerServiceId);
    }
}
