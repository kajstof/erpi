using Erpi.BuildingBlocks.Application;
using Erpi.BuildingBlocks.Infrastructure;
using Erpi.Trucks.Application.Database;
using Erpi.Trucks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Erpi.Trucks.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrucksInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TruckDbContext");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        services.AddDbContext<TruckDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
        });
        services.AddScoped<ITruckDbContext>(provider => provider.GetRequiredService<TruckDbContext>());

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}