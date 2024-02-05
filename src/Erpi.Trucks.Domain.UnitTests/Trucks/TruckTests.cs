using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Trucks;
using FluentAssertions;

namespace Erpi.Trucks.Domain.UnitTests.Trucks;

public class TruckTests
{
    [Fact]
    public void GivenValidData_WhenCreateTruck_ThenTruckShouldBeCreated()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatus = "OutOfService";

        // When
        var func = () => Truck.Create(code, name, truckStatus);
        
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
        var truckStatus = "OutOfService";

        // When
        var func = () => Truck.Create(code, name, truckStatus);

        // Then
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void GivenEmptyString_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;
        var truckStatus = "OutOfService";

        // When
        var func = () => Truck.Create(code, name, truckStatus);

        // Then
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void GivenNotValidStatus_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;
        var truckStatus = "NonExistsStatus";

        // When
        var func = () => Truck.Create(code, name, truckStatus);

        // Then
        func.Should().Throw<DomainException>();
    }
}