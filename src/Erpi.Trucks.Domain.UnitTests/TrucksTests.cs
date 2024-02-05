using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Trucks;
using FluentAssertions;

namespace Erpi.Trucks.Domain.UnitTests;

public class TrucksTests
{
    [Fact]
    public void GivenValidData_WhenCreateTruck_ThenTruckShouldBeCreated()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";

        // When
        var func = () => Truck.Create(code, name);

        // Then
        func.Should().NotThrow<DomainException>();
        func().Code.ToString().Should().Be(code);
    }
    
    [Fact]
    public void GivenNonAlphanumericCode_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "SDAFd8fdfS+";
        var name = "Name";

        // When
        var func = () => Truck.Create(code, name);

        // Then
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void GivenEmptyString_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;

        // When
        var func = () => Truck.Create(code, name);

        // Then
        func.Should().Throw<DomainException>();
    }
}