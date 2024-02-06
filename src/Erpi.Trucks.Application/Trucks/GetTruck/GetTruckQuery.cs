using Erpi.BuildingBlocks.Application;
using Erpi.Trucks.Application.Database;
using Erpi.Trucks.Application.Trucks.AddTruck;
using Erpi.Trucks.Domain.Trucks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Erpi.Trucks.Application.Trucks.GetTruck;

public record GetTruckQuery(string Code) : IRequest<TruckResponse>;

public class AddTruckCommandHandler(ITruckDbContext truckDbContext)
    : IRequestHandler<GetTruckQuery, TruckResponse>
{
    public async Task<string> Handle(AddTruckCommand request, CancellationToken ct)
    {
        var truck = await Truck.Create(
            request.Code,
            request.Name,
            request.Status,
            request.Description,
            new TruckUniquenessCodeChecker(truckDbContext, ct));

        await truckDbContext.Trucks.AddAsync(truck, ct);
        await truckDbContext.SaveChangesAsync(ct);

        return truck.Code.Code;
    }

    public async Task<TruckResponse> Handle(GetTruckQuery request, CancellationToken ct)
    {
        var truck = await truckDbContext.Trucks.FirstOrDefaultAsync(x => x.Code.Code == request.Code, ct);

        if (truck is null)
        {
            throw new ApplicationLogicException("Truck not exists");
        }

        return new TruckResponse(
            truck.Code.Code,
            truck.Name,
            truck.Status.Code,
            truck.Status.Description,
            truck.Description);
    }
}