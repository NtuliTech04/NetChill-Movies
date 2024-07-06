using NetChill.Application.Extensions;
using NetChill.Infrastructure.Extensions;
using NetChill.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetChill.Ioc.Dependency_Injection
{
    public static class DependencyContainer
    {
        //Groups All Services under One Service Collection
        public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistentLayer(configuration);
            services.AddInfrastructureLayer(configuration);
            services.AddApplicationLayer();

            return services;
        }
    }
}
