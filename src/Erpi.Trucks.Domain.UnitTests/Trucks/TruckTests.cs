using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Exceptions;
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
        var truckStatusCode = TruckStatus.OutOfService.Code;
        var description = "description";

        // When
        var truck = Truck.Create(code, name, truckStatusCode, description);

        // Then
        truck.Code.ToString().Should().Be(code);
        truck.Name.Should().Be(name);
        truck.Description.Should().Be(description);
    }

    [Fact]
    public void GivenNoDescription_WhenCreateTruck_ThenTruckShouldBeCreated()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.OutOfService.Code;

        // When
        var truck = Truck.Create(code, name, truckStatusCode, description: null);

        // Then
        truck.Code.ToString().Should().Be(code);
        truck.Name.Should().Be(name);
        truck.Description.Should().BeNull();
    }

    [Fact]
    public void GivenNonAlphanumericCode_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "SDAFd8fdfS+";
        var name = "Name";
        var truckStatusCode = TruckStatus.OutOfService.Code;

        // When
        var func = () => Truck.Create(code, name, truckStatusCode);

        // Then
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void GivenEmptyString_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;
        var truckStatusCode = TruckStatus.OutOfService.Code;

        // When
        var func = () => Truck.Create(code, name, truckStatusCode);

        // Then
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void GivenNotValidStatus_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;
        var truckStatusCode = "NonExistsStatus";

        // When
        var func = () => Truck.Create(code, name, truckStatusCode);

        // Then
        func.Should().Throw<DomainException>();
    }
    
    [Fact]
    public void GivenTruck_WhenSetValidStatus_ThenShouldChangeStatus()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.Loading.Code;
        var newTruckStatus = TruckStatus.ToJob;
        var truck = Truck.Create(code, name, truckStatusCode);
        
        // When
        truck.SetStatus(newTruckStatus.Code);

        // Then
        truck.Status.Code.Should().Be(newTruckStatus.Code);
    }
        
    [Fact]
    public void GivenTruck_WhenSetInvalidStatus_ThenShouldThrowTransitionTruckStatusException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.Returning.Code;
        var newTruckStatus = TruckStatus.ToJob;
        var truck = Truck.Create(code, name, truckStatusCode);
        
        // When
        var func = () => truck.SetStatus(newTruckStatus.Code);

        // Then
        func.Should().Throw<TransitionTruckStatusException>();
    }
}