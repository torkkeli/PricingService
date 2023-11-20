using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using Application.Price.Requests;
using System;
using System.Web.Http;
using Application.Price.Models;

namespace FunctionApp
{
    public class PriceFunctions
    {
        private readonly IMediator mediator;

        public PriceFunctions(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [FunctionName("get-price-service-a")]
        public async Task<IActionResult> GetPriceServiceA(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "prices/a/{customerId}")] HttpRequest req,
            ILogger log,
            int customerId)
        {
            var start = req.Query["start"];
            var end = req.Query["end"];

            log.LogInformation($"Get price service a called, customerId: {customerId}, start: {start}, end: {end}");

            if (!DateTime.TryParse(start, out var startDate))
            {
                return new BadRequestErrorMessageResult("Parameter 'start' must be a DateTime.");
            }

            if (!DateTime.TryParse(end, out var endDate))
            {
                return new BadRequestErrorMessageResult("Parameter 'end' must be a DateTime.");
            }

            var result = await mediator.Send(new GetPriceServiceARequest(customerId, startDate, endDate));

            return new OkObjectResult(result);
        }

        [FunctionName("get-price-service-b")]
        public async Task<IActionResult> GetPriceServiceB(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "prices/b/{customerId}")] HttpRequest req,
            ILogger log,
            int customerId)
        {
            var start = req.Query["start"];
            var end = req.Query["end"];

            log.LogInformation($"Get price service b called, customerId: {customerId}, start: {start}, end: {end}");

            if (!DateTime.TryParse(start, out var startDate))
            {
                return new BadRequestErrorMessageResult("Parameter 'start' must be a DateTime.");
            }

            if (!DateTime.TryParse(end, out var endDate))
            {
                return new BadRequestErrorMessageResult("Parameter 'end' must be a DateTime.");
            }

            var result = await mediator.Send(new GetPriceServiceBRequest(customerId, startDate, endDate));

            return new OkObjectResult(result);
        }

        [FunctionName("get-price-service-c")]
        public async Task<IActionResult> GetPriceServiceC(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "prices/c/{customerId}")] HttpRequest req,
            ILogger log,
            int customerId)
        {
            var start = req.Query["start"];
            var end = req.Query["end"];

            log.LogInformation($"Get price service c called, customerId: {customerId}, start: {start}, end: {end}");

            if (!DateTime.TryParse(start, out var startDate))
            {
                return new BadRequestErrorMessageResult("Parameter 'start' must be a DateTime.");
            }

            if (!DateTime.TryParse(end, out var endDate))
            {
                return new BadRequestErrorMessageResult("Parameter 'end' must be a DateTime.");
            }

            var result = await mediator.Send(new GetPriceServiceCRequest(customerId, startDate, endDate));

            return new OkObjectResult(result);
        }
    }
}
