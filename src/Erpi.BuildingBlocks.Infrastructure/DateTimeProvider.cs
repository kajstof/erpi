using Erpi.BuildingBlocks.Application;

namespace Erpi.BuildingBlocks.Infrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}