using Erpi.BuildingBlocks.Application;
using Erpi.Trucks.Application.Database;
using Erpi.Trucks.Domain.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Erpi.Trucks.Infrastructure.Database;

// TODO: WIP
public class TruckRepository(ITruckDbContext truckDbContext) : ITruckRepository
{
    public async Task Create(Truck truck, CancellationToken ct)
    {
        await truckDbContext.Trucks.AddAsync(truck, ct);
    }

    public async Task<Truck> Get(string truckCode, CancellationToken ct)
    {
        return await truckDbContext.Trucks.FirstOrDefaultAsync(x => x.Code.Code == truckCode, ct)
               ?? throw new ApplicationLogicException("Truck entity not fount");
    }

    public async Task<List<Truck>> GetAll(CancellationToken ct)
    {
        return await truckDbContext.Trucks.ToListAsync(ct);
    }

    public Task Update(Truck truck)
    {
        truckDbContext.Trucks.Update(truck);
        return Task.CompletedTask;
    }

    public async Task Delete(string truckCode, CancellationToken ct)
    {
        var truck = await Get(truckCode, ct);
        truckDbContext.Trucks.Remove(truck);
    }
}