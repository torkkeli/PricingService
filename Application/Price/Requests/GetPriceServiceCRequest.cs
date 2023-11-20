using Application.Price.Models;
using Domain.Customer;
using MediatR;

namespace Application.Price.Requests
{
    public record GetPriceServiceCRequest(int CustomerId, DateTime StartDate, DateTime EndDate) : IRequest<CustomerChargeDto>;

    public class GetPriceServiceCRequestHandler : IRequestHandler<GetPriceServiceCRequest, CustomerChargeDto>
    {
        private readonly IPriceService priceService;

        public GetPriceServiceCRequestHandler(IPriceService priceService)
        {
            this.priceService = priceService;
        }

        public async Task<CustomerChargeDto> Handle(GetPriceServiceCRequest request, CancellationToken cancellationToken)
        {
            var price = await priceService.CalculatePrice(request.CustomerId, PriceServiceType.C, request.StartDate, request.EndDate);

            return new CustomerChargeDto
            {
                CustomerId = request.CustomerId,
                Price = price
            };
        }
    }
}
