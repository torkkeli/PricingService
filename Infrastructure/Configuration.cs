using Application.Customer;
using Infrastructure.Repositories.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddTransient<ICustomerFreeDaysRepository, CustomerFreeDaysRepository>();
            services.AddTransient<ICustomerServiceRepository, CustomerServiceRepository>();

            return services;
        }
    }
}
