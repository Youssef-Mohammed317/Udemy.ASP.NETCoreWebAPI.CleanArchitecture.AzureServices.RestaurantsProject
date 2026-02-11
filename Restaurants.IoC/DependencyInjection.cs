using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure;

namespace Restaurants.IoC;

public static class DependencyInjection
{
    public static void RegisterAllServices(this IServiceCollection services, IConfiguration configurations)
    {
        RegisterInfrastructureServices.AddInfrastructureServices(services, configurations);
    }
}
