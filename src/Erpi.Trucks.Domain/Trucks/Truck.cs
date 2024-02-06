using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Exceptions;
using Erpi.Trucks.Domain.Trucks.Interfaces;
using Erpi.Trucks.Domain.Trucks.Services;

namespace Erpi.Trucks.Domain.Trucks;

public class Truck : IAggregateRoot
{
    public Guid Id { get; }

    public AlphanumericCode Code { get; private set; }

    public string Name { get; private set; }

    public TruckStatus Status { get; private set; }

    public string? Description { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Truck()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private Truck(
        Guid id,
        AlphanumericCode code,
        string name,
        TruckStatus status,
        string? description)
    {
        Id = id;
        Status = status;
        Code = code;
        Name = name;
        Description = description;
    }

    public static async Task<Truck> Create(
        string code,
        string name,
        string statusCode,
        string? description,
        ITruckUniquenessChecker truckUniquenessChecker)
    {
        if (string.IsNullOrEmpty(name))
        {
            // TODO: Improvements like:
            // Custom exception, like EmptyNameDomainException : Exception
            // some guard "interface" for such validation
            throw new DomainException("Name should be not empty");
        }
        
        var truck = new Truck(
            Guid.NewGuid(),
            AlphanumericCode.Of(code),
            name,
            TruckStatus.Of(statusCode),
            description);
        
        var isUnique = await truckUniquenessChecker.IsUnique(truck);
        
        if (!isUnique)
        {
            throw new DomainException("Truck with this code already exists");
        }

        return truck;
    }

    public void SetStatus(string truckStatusCode)
    {
        var newTruckStatus = TruckStatus.Of(truckStatusCode);
        if (!TruckStatusTransitionValidator.Validate(Status, newTruckStatus))
        {
            throw new TransitionTruckStatusException();
        }

        Status = newTruckStatus;
    }
}