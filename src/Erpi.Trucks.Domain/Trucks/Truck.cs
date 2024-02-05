using Erpi.BuildingBlocks.Domain;

namespace Erpi.Trucks.Domain.Trucks;

public class Truck : IAggregateRoot
{
    public Guid Id { get; }

    public AlphanumericCode Code { get; private set; }

    public string Name { get; private set; }

    public TruckStatus TruckStatus { get; private set; }

    private Truck(
        AlphanumericCode code,
        string name,
        TruckStatus truckStatus)
    {
        TruckStatus = truckStatus;
        Code = code;
        Name = name;
    }

    public static Truck Create(
        string code,
        string name,
        string truckStatus)
    {
        if (string.IsNullOrEmpty(name))
        {
            // TODO: Improvements like:
            // Custom exception, like EmptyNameDomainException : Exception
            // some guard "interface" for such validation
            throw new DomainException("Name should be not empty");
        }

        return new Truck(
            AlphanumericCode.Of(code),
            name,
            TruckStatus.Of(truckStatus));
    }
}