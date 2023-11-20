using Domain.Customer;

namespace Application.Price
{
    public interface IPriceService
    {
        Task<decimal> CalculatePrice(
            int customerId,
            PriceServiceType priceServiceType,
            DateTime startDate,
            DateTime endDate);
    }
}
