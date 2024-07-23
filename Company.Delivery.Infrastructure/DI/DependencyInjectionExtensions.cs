using Company.Delivery.Database.Repos;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Delivery.Infrastructure.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IWaybillService, WaybillService>();
            services.AddScoped<IWaybillRepository, WaybillRepository>();
            return services;
        }
    }
}
