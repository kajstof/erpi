using Erpi.BuildingBlocks.Domain;

namespace Erpi.Trucks.Domain.Trucks;

public class Truck : IAggregateRoot
{
    public Guid Id { get; }

    public AlphanumericCode Code { get; private set; }

    private Truck(
        AlphanumericCode code)
    {
        Code = code;
    }

    public static Truck Create(
        string code)
    {
        return new Truck(
            AlphanumericCode.Of(code));
    }
}