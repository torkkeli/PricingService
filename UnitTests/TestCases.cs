using Application.Customer;
using Application.Price;
using Domain.Customer;


namespace UnitTests
{
    public class TestCases
    {
        private readonly IPriceService priceService;
        private readonly Mock<ICustomerDiscountRepository> customerDiscountRepositoryMock;
        private readonly Mock<ICustomerFreeDaysRepository> customerFreeDaysRepositoryMock;
        private readonly Mock<ICustomerServiceRepository> customerServiceRepositoryMock;
        private readonly Fixture fixture;

        public TestCases()
        {
            customerDiscountRepositoryMock = new Mock<ICustomerDiscountRepository>();
            customerFreeDaysRepositoryMock = new Mock<ICustomerFreeDaysRepository>();
            customerServiceRepositoryMock = new Mock<ICustomerServiceRepository>();
            fixture = new Fixture();

            priceService = new PriceService(
                customerDiscountRepositoryMock.Object,
                customerFreeDaysRepositoryMock.Object,
                customerServiceRepositoryMock.Object);
        }

        [Fact]
        public async Task TestCaseOne()
        {
            var customerId = fixture.Create<int>();
            var startDate = new DateTime(2019, 9, 20);
            var endDate = new DateTime(2019, 10, 1);
            var customerServiceA = fixture.Build<CustomerService>()
                .With(s => s.StartDate, new DateTime(2019, 9, 20))
                .Without(s => s.Price)
                .Create();
            var customerServiceC = fixture.Build<CustomerService>()
                .With(s => s.StartDate, new DateTime(2019, 9, 20))
                .Without(s => s.Price)
                .Create();

            customerServiceRepositoryMock.Setup(r => r.GetCustomerService(customerId, PriceServiceType.A)).ReturnsAsync(customerServiceA);
            customerServiceRepositoryMock.Setup(r => r.GetCustomerService(customerId, PriceServiceType.C)).ReturnsAsync(customerServiceC);
            customerDiscountRepositoryMock.Setup(r => r.GetCustomerDiscounts(customerServiceC.CustomerServiceId)).ReturnsAsync(
                new List<CustomerDiscount>
                {
                    new() {
                        Discount = 0.2m,
                        StartDate = new DateTime(2019, 9, 22),
                        EndDate = new DateTime(2019, 9, 24)
                    }
                });

            var priceServiceA = await priceService.CalculatePrice(customerId, PriceServiceType.A, startDate, endDate);
            var priceServiceB = await priceService.CalculatePrice(customerId, PriceServiceType.C, startDate, endDate);
            var result = priceServiceA + priceServiceB;

            result.Should().Be(4.336m);
        }

        [Fact]
        public async Task TestCaseTwo()
        {
            var customerId = fixture.Create<int>();
            var startDate = new DateTime(2018, 1, 1);
            var endDate = new DateTime(2019, 10, 1);
            var customerServiceB = fixture.Build<CustomerService>()
                .With(s => s.StartDate, new DateTime(2019, 9, 20))
                .Without(s => s.Price)
                .Create();
            var customerServiceC = fixture.Build<CustomerService>()
                .With(s => s.StartDate, new DateTime(2019, 9, 20))
                .Without(s => s.Price)
                .Create();
            var customerFreeDays = fixture.Build<CustomerFreeDays>()
                .With(fd => fd.NumberOfFreeDays, 200)
                .Create();

            customerServiceRepositoryMock.Setup(r => r.GetCustomerService(customerId, PriceServiceType.B)).ReturnsAsync(customerServiceB);
            customerServiceRepositoryMock.Setup(r => r.GetCustomerService(customerId, PriceServiceType.C)).ReturnsAsync(customerServiceC);
            customerDiscountRepositoryMock.Setup(r => r.GetCustomerDiscounts(customerServiceB.CustomerServiceId)).ReturnsAsync(
                new List<CustomerDiscount>
                {
                    new() {
                        Discount = 0.3m,
                        StartDate = startDate,
                        EndDate = endDate
                    }
                });
            customerDiscountRepositoryMock.Setup(r => r.GetCustomerDiscounts(customerServiceC.CustomerServiceId)).ReturnsAsync(
                new List<CustomerDiscount>
                {
                    new() {
                        Discount = 0.3m,
                        StartDate = startDate,
                        EndDate = endDate
                    }
                });

            var priceServiceB = await priceService.CalculatePrice(customerId, PriceServiceType.B, startDate, endDate);
            var priceServiceC = await priceService.CalculatePrice(customerId, PriceServiceType.C, startDate, endDate);
            var result = priceServiceB + priceServiceC;

            result.Should().Be(171.332m);
        }
    }
}
