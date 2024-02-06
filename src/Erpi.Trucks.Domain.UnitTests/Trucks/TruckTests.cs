using Erpi.BuildingBlocks.Domain;
using Erpi.Trucks.Domain.Exceptions;
using Erpi.Trucks.Domain.Trucks;
using Erpi.Trucks.Domain.Trucks.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace Erpi.Trucks.Domain.UnitTests.Trucks;

public class TruckTests
{
    [Fact]
    public async Task GivenValidData_WhenCreateTruck_ThenTruckShouldBeCreated()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.OutOfService.Code;
        var description = "description";

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        // When
        var truck = await Truck.Create(code, name, truckStatusCode, description, truckUniqueness);

        // Then
        truck.Code.ToString().Should().Be(code);
        truck.Name.Should().Be(name);
        truck.Description.Should().Be(description);
    }

    [Fact]
    public async Task GivenNoDescription_WhenCreateTruck_ThenTruckShouldBeCreated()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.OutOfService.Code;

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        // When
        var truck = await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

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

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        // When
        var func = async () => await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

        // Then
        func.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public void GivenEmptyString_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;
        var truckStatusCode = TruckStatus.OutOfService.Code;

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        // When
        var func = async () => await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

        // Then
        func.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public void GivenNotValidStatus_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = string.Empty;
        var truckStatusCode = "NonExistsStatus";

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        // When
        var func = async () => await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

        // Then
        func.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task GivenTruck_WhenSetValidStatus_ThenShouldChangeStatus()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.Loading.Code;
        var newTruckStatus = TruckStatus.ToJob;

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        var truck = await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

        // When
        truck.SetStatus(newTruckStatus.Code);

        // Then
        truck.Status.Code.Should().Be(newTruckStatus.Code);
    }

    [Fact]
    public async Task GivenTruck_WhenSetInvalidStatus_ThenShouldThrowTransitionTruckStatusException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.Returning.Code;
        var newTruckStatus = TruckStatus.ToJob;

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(true);

        var truck = await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

        // When
        var func = () => truck.SetStatus(newTruckStatus.Code);

        // Then
        func.Should().Throw<TransitionTruckStatusException>();
    }

    [Fact]
    public void GivenUniquenessFalse_WhenCreateTruck_ThenShouldThrowDomainException()
    {
        // Given
        var code = "asdbfsdaf789asdhf3bfdas";
        var name = "Name";
        var truckStatusCode = TruckStatus.OutOfService.Code;

        var truckUniqueness = Substitute.For<ITruckUniquenessChecker>();
        truckUniqueness.IsUnique(Arg.Any<Truck>()).Returns(false);

        // When
        var func = async () => await Truck.Create(code, name, truckStatusCode, description: null, truckUniqueness);

        // Then
        func.Should()
            .ThrowAsync<DomainException>()
            .Result.WithMessage("Truck with this code already exists");
    }
}