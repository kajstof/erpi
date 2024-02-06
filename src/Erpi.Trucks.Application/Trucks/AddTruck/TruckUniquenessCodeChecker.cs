using Erpi.Trucks.Application.Database;
using Erpi.Trucks.Domain.Trucks;
using Erpi.Trucks.Domain.Trucks.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erpi.Trucks.Application.Trucks.AddTruck;

public class TruckUniquenessCodeChecker(ITruckDbContext truckDbContext, CancellationToken ct) : ITruckUniquenessChecker
{
    public async Task<bool> IsUnique(Truck truck)
        => await truckDbContext.Trucks.AnyAsync(x => x.Code.Code == truck.Code.Code, ct);
}