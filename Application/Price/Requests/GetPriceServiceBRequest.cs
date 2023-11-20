using Application.Price.Models;
using Domain.Customer;
using MediatR;

namespace Application.Price.Requests
{
    public record GetPriceServiceBRequest(int CustomerId, DateTime StartDate, DateTime EndDate) : IRequest<CustomerChargeDto>;

    public class GetPriceServiceBRequestHandler : IRequestHandler<GetPriceServiceBRequest, CustomerChargeDto>
    {
        private readonly IPriceService priceService;

        public GetPriceServiceBRequestHandler(IPriceService priceService)
        {
            this.priceService = priceService;
        }

        public async Task<CustomerChargeDto> Handle(GetPriceServiceBRequest request, CancellationToken cancellationToken)
        {
            var price = await priceService.CalculatePrice(request.CustomerId, PriceServiceType.B, request.StartDate, request.EndDate);

            return new CustomerChargeDto
            {
                CustomerId = request.CustomerId,
                Price = price
            };
        }
    }
}
