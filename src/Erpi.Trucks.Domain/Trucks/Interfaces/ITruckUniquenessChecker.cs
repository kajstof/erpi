namespace Erpi.Trucks.Domain.Trucks.Interfaces;

public interface ITruckUniquenessChecker
{
    Task<bool> IsUnique(Truck truck);
}