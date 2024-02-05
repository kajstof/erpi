using Erpi.BuildingBlocks.Domain;

namespace Erpi.Trucks.Domain.Exceptions;

public class TransitionTruckStatusException : DomainException
{
    private static readonly string ExceptionMessage = "Transition is not possible";

    public TransitionTruckStatusException() : base(ExceptionMessage)
    {
    }

    public TransitionTruckStatusException(Exception e) : base(ExceptionMessage, e)
    {
    }
}