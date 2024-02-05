using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Exceptions;
using Erpi.Trucks.Domain.Trucks.Services;

namespace Erpi.Trucks.Domain.Trucks;

public record TruckStatus : ValueObject<TruckStatus>
{
    public static TruckStatus OutOfService => new(nameof(OutOfService), "Out of service");

    public static TruckStatus Loading => new(nameof(Loading), "Loading");

    public static TruckStatus ToJob => new(nameof(ToJob), "To Job");

    public static TruckStatus AtJob => new(nameof(AtJob), "At Job");

    public static TruckStatus Returning => new(nameof(Returning), "Returning");

    public string Code { get; private set; }

    public string Description { get; private set; }

    private TruckStatus(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public static TruckStatus Of(string truckStatusCode)
    {
        var availableTruckStatuses = GetAllTypes();
        var foundTruckStatus = availableTruckStatuses.SingleOrDefault(x => x.Code == truckStatusCode);

        if (foundTruckStatus is null)
        {
            throw new DomainException("Invalid Track Status Code");
        }

        return foundTruckStatus;
    }

    public void TransitionTo(string truckStatusCode)
    {
        if (TruckStatusTransitionValidator.Validate(this, Of(truckStatusCode)))
        {
            throw new TransitionTruckStatusException();
        }
    }
}