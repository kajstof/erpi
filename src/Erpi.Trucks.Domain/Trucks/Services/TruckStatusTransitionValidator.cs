namespace Erpi.Trucks.Domain.Trucks.Services;

public static class TruckStatusTransitionValidator
{
    private static Dictionary<TruckStatus, IEnumerable<TruckStatus>> _possibleTransitions => new()
    {
        // Is changing to the same status OK?
        [TruckStatus.OutOfService] = TruckStatus.GetAllTypes().Where(x => x != TruckStatus.OutOfService),
        [TruckStatus.Loading] = new[] { TruckStatus.OutOfService, TruckStatus.ToJob },
        [TruckStatus.ToJob] = new[] { TruckStatus.OutOfService, TruckStatus.AtJob, },
        [TruckStatus.AtJob] = new[] { TruckStatus.OutOfService, TruckStatus.Returning, },
        [TruckStatus.Returning] = new[] { TruckStatus.OutOfService, TruckStatus.Loading, },
    };

    public static bool Validate(TruckStatus oldTruckStatus, TruckStatus newTruckStatus)
    {
        return _possibleTransitions.Single(x => x.Key == oldTruckStatus).Value.Contains(newTruckStatus);
    }
}