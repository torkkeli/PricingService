using Application.Customer;
using Domain.Customer;

namespace Application.Price
{
    public class PriceService : IPriceService
    {
        private readonly ICustomerDiscountRepository customerDiscountRepository;
        private readonly ICustomerFreeDaysRepository customerFreeDaysRepository;
        private readonly ICustomerServiceRepository customerServiceRepository;

        public PriceService(
            ICustomerDiscountRepository customerDiscountRepository,
            ICustomerFreeDaysRepository customerFreeDaysRepository,
            ICustomerServiceRepository customerServiceRepository)
        {
            this.customerDiscountRepository = customerDiscountRepository;
            this.customerFreeDaysRepository = customerFreeDaysRepository;
            this.customerServiceRepository = customerServiceRepository;
        }

        public async Task<decimal> CalculatePrice(
            int customerId,
            PriceServiceType priceServiceType,
            DateTime startDate,
            DateTime endDate)
        {
            var customerService = await GetCustomerService(customerId, priceServiceType, startDate);
            var workingDayExemptions = priceServiceType.GetWorkingDayExcemptions();
            var customerFreeDays = await customerFreeDaysRepository.GetCustomerFreeDays(customerService.CustomerId);
            var numberOfFreeDays = customerFreeDays?.NumberOfFreeDays ?? 0;
            var allWorkingDays = GetWorkingDays(startDate, endDate, workingDayExemptions);

            if (numberOfFreeDays > allWorkingDays.Count())
            {
                return 0m;
            }

            var price = 0m;
            var remainingWorkingDays = allWorkingDays.OrderBy(d => d.Date).Skip(numberOfFreeDays).ToList();
            var customerDiscounts = await customerDiscountRepository.GetCustomerDiscounts(customerService.CustomerServiceId);
            var dailyPrice = customerService.Price ?? customerService.PriceServiceType.GetPriceServiceTypeBasePrice();

            foreach (var customerDiscount in customerDiscounts)
            {
                var discountedWorkingDays = remainingWorkingDays.Where(d => d >= customerDiscount.StartDate && d <= customerDiscount.EndDate);

                price += discountedWorkingDays.Count() * dailyPrice * (1m - customerDiscount.Discount);
                remainingWorkingDays.RemoveAll(dt => discountedWorkingDays.Contains(dt));
            }

            price += dailyPrice * remainingWorkingDays.Count;

            return price;
        }

        private async Task<CustomerService> GetCustomerService(
            int customerId,
            PriceServiceType priceServiceType,
            DateTime startDate)
        {
            var customerPriceService = await customerServiceRepository.GetCustomerService(customerId, priceServiceType);

            if (customerPriceService == null || customerPriceService.StartDate > startDate)
            {
                throw new Exception($"Customer with id {customerId} does not have an active price service A for the given time period");
            }

            return customerPriceService;
        }

        private static IEnumerable<DateTime> GetWorkingDays(DateTime startDate, DateTime endDate, IEnumerable<DayOfWeek> exemptions)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("startDate is greater than endDate");
            }

            return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d)).Where(d => !exemptions.Contains(d.DayOfWeek));
        }
    }
}
