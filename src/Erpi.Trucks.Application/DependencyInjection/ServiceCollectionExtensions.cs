using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Erpi.Trucks.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrucksApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}