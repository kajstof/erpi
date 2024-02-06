using Erpi.Trucks.Domain.Trucks;
using MediatR;

namespace Erpi.Trucks.Application.Trucks.AddTruck;

public record AddTruckCommand(string Code, string Name, string Status, string? Description = null) : IRequest<string>;

public class AddTruckCommandHandler : IRequestHandler<AddTruckCommand, string>
{
    public Task<string> Handle(AddTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = Truck.Create(request.Code, request.Name, request.Status, request.Description);

        return Task.FromResult(truck.Code.ToString());
    }
}