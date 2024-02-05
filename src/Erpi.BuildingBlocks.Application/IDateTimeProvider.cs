namespace Erpi.BuildingBlocks.Application;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}
