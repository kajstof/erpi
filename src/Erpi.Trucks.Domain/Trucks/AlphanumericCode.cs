using Erpi.BuildingBlocks.Domain;
using Erpi.BuildingBlocks.Domain.Extensions;

namespace Erpi.Trucks.Domain.Trucks;

public record AlphanumericCode : ValueObject<AlphanumericCode>
{
    public string Code { get; private set; }

    private AlphanumericCode(string code)
    {
        Code = code;
    }

    public static AlphanumericCode Of(string name)
    {
        if (!name.IsAlphanumeric())
        {
            throw new DomainException("The code is no alphanumeric");
        }

        return new AlphanumericCode(name);
    }

    public override string ToString()
    {
        return Code;
    }
}