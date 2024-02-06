using Erpi.Trucks.Application.Database;
using Erpi.Trucks.Domain.Trucks;
using MediatR;

namespace Erpi.Trucks.Application.Trucks.AddTruck;

public record AddTruckCommand(string Code, string Name, string Status, string? Description = null) : IRequest<string>;

public class AddTruckCommandHandler(ITruckDbContext truckDbContext)
    : IRequestHandler<AddTruckCommand, string>
{
    public async Task<string> Handle(AddTruckCommand request, CancellationToken ct)
    {
        var truck = await Truck.Create(
            request.Code,
            request.Name,
            request.Status,
            request.Description,
            new TruckUniquenessCodeChecker(truckDbContext, ct));

        return truck.Code.Code;
    }
}