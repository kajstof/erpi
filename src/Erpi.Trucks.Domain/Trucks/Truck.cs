using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Exceptions;
using Erpi.Trucks.Domain.Trucks.Services;

namespace Erpi.Trucks.Domain.Trucks;

public class Truck : IAggregateRoot
{
    public Guid Id { get; }

    public AlphanumericCode Code { get; private set; }

    public string Name { get; private set; }

    public TruckStatus Status { get; private set; }

    public string? Description { get; private set; }

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

    public static Truck Create(
        string code,
        string name,
        string truckStatusCode,
        string? description = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            // TODO: Improvements like:
            // Custom exception, like EmptyNameDomainException : Exception
            // some guard "interface" for such validation
            throw new DomainException("Name should be not empty");
        }

        return new Truck(
            Guid.NewGuid(),
            AlphanumericCode.Of(code),
            name,
            TruckStatus.Of(truckStatusCode),
            description);
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