using Erpi.Trucks.Domain.Trucks;

namespace Erpi.Trucks.Application.Database;

public interface ITruckRepository
{
    Task<bool> Create(Truck resource);
    Task<Truck> Get(Guid resourceId);
    Task<IReadOnlyCollection<Truck>> GetAll();
    Task<bool> Update(Truck resource);
    Task<bool> Delete(Guid resourceId);
}