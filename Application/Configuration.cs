using Application.Price;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Configuration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPriceService, PriceService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Configuration).Assembly));

            return services;
        }
    }
}
