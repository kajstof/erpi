using Erpi.Trucks.Domain.Trucks;
using Erpi.Trucks.Domain.Trucks.Services;
using FluentAssertions;

namespace Erpi.Trucks.Domain.UnitTests.Trucks.Services;

public class TruckStatusTransitionValidatorTests
{
    [Theory]
    [MemberData(nameof(ValidTransitionsData))]
    public void GivenValidState_WhenValidateTransition_ShouldReturnTrue(
        TruckStatus oldStatus, TruckStatus newStatus)
    {
        // Given via parameters
        // When
        var isValid = TruckStatusTransitionValidator.Validate(oldStatus, newStatus);

        // Then
        isValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidTransitionsData))]
    public void GivenValidState_WhenValidateTransition_ShouldReturnFalse(
        TruckStatus oldStatus, TruckStatus newStatus)
    {
        // Given via parameters
        // When
        var isValid = TruckStatusTransitionValidator.Validate(oldStatus, newStatus);

        // Then
        isValid.Should().BeFalse();
    }

    public static IEnumerable<object[]> ValidTransitionsData =>
        new List<object[]>
        {
            new object[] { TruckStatus.Loading, TruckStatus.OutOfService },
            new object[] { TruckStatus.ToJob, TruckStatus.OutOfService },
            new object[] { TruckStatus.AtJob, TruckStatus.OutOfService },
            new object[] { TruckStatus.Returning, TruckStatus.OutOfService },
            new object[] { TruckStatus.OutOfService, TruckStatus.Loading },
            new object[] { TruckStatus.OutOfService, TruckStatus.ToJob },
            new object[] { TruckStatus.OutOfService, TruckStatus.AtJob },
            new object[] { TruckStatus.OutOfService, TruckStatus.Returning },
            new object[] { TruckStatus.Loading, TruckStatus.ToJob },
            new object[] { TruckStatus.ToJob, TruckStatus.AtJob },
            new object[] { TruckStatus.AtJob, TruckStatus.Returning },
            new object[] { TruckStatus.Returning, TruckStatus.Loading },
        };

    public static IEnumerable<object[]> InvalidTransitionsData =>
        new List<object[]>
        {
            new object[] { TruckStatus.OutOfService, TruckStatus.OutOfService },
            new object[] { TruckStatus.Loading, TruckStatus.Loading },
            new object[] { TruckStatus.Loading, TruckStatus.AtJob },
            new object[] { TruckStatus.Loading, TruckStatus.Returning },
            new object[] { TruckStatus.ToJob, TruckStatus.Loading },
            new object[] { TruckStatus.ToJob, TruckStatus.ToJob },
            new object[] { TruckStatus.ToJob, TruckStatus.Returning },
            new object[] { TruckStatus.AtJob, TruckStatus.Loading },
            new object[] { TruckStatus.AtJob, TruckStatus.ToJob },
            new object[] { TruckStatus.Returning, TruckStatus.ToJob },
            new object[] { TruckStatus.Returning, TruckStatus.AtJob },
            new object[] { TruckStatus.Returning, TruckStatus.Returning },
        };
}