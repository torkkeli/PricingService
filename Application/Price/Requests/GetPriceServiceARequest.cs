using Application.Price.Models;
using Domain.Customer;
using MediatR;

namespace Application.Price.Requests
{
    public record GetPriceServiceARequest(int CustomerId, DateTime StartDate, DateTime EndDate) : IRequest<CustomerChargeDto>;

    public class GetPriceServiceARequestHandler : IRequestHandler<GetPriceServiceARequest, CustomerChargeDto>
    {
        private readonly IPriceService priceService;

        public GetPriceServiceARequestHandler(IPriceService priceService)
        {
            this.priceService = priceService;
        }

        public async Task<CustomerChargeDto> Handle(GetPriceServiceARequest request, CancellationToken cancellationToken)
        {
            var price = await priceService.CalculatePrice(request.CustomerId, PriceServiceType.A, request.StartDate, request.EndDate);

            return new CustomerChargeDto
            {
                CustomerId = request.CustomerId,
                Price = price
            };
        }
    }
}
