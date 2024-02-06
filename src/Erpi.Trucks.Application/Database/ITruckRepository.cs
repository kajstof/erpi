using Erpi.Trucks.Domain.Trucks;

namespace Erpi.Trucks.Application.Database;

// TODO: WIP
public interface ITruckRepository
{
    Task Create(Truck truck, CancellationToken ct);
    Task<Truck> Get(string truckCode, CancellationToken ct);
    Task<List<Truck>> GetAll(CancellationToken ct);
    Task Update(Truck truck);
    Task Delete(string truckCode, CancellationToken ct);
}