using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Trucks;
using FluentAssertions;

namespace Erpi.Trucks.Domain.UnitTests;

public class TrucksTests
{
    [Fact]
    public void GivenAlphanumericCode_WhenCreateTruck_ThenTruckShouldBeCreated()
    {
        // Given
        var name = "asdbfsdaf789asdhf3bfdas";

        // When
        var func = () => Truck.Create(name);

        // Then
        func.Should().NotThrow<DomainException>();
        func().Code.ToString().Should().Be(name);
    }
    
    [Fact]
    public void GivenNonAlphanumericCode_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var name = "SDAFd8fdfS+";

        // When
        var func = () => Truck.Create(name);

        // Then
        func.Should().Throw<DomainException>();
    }
}