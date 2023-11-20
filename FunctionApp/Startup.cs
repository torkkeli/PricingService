using Application;
using FunctionApp;
using Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
        }
    }
}
