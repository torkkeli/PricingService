using Domain.Customer;

namespace Application.Price
{
    public static class PriceExtensions
    {
        public static decimal GetPriceServiceTypeBasePrice(this PriceServiceType priceServiceType)
        {
            switch (priceServiceType)
            {
                case PriceServiceType.A:
                    return 0.2m;
                case PriceServiceType.B:
                    return 0.24m;
                case PriceServiceType.C:
                    return 0.4m;
                default:
                    throw new NotImplementedException();
            }
        }

        public static IEnumerable<DayOfWeek> GetWorkingDayExcemptions(this PriceServiceType priceServiceType)
        {
            switch (priceServiceType)
            {
                case PriceServiceType.A:
                case PriceServiceType.B:
                    yield return DayOfWeek.Saturday;
                    yield return DayOfWeek.Sunday;
                    yield break;
                case PriceServiceType.C:
                    yield break;
                default:
                    throw new NotImplementedException();

            }
        }
    }
}
