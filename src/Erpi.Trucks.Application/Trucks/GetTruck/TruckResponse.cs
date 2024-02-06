namespace Erpi.Trucks.Application.Trucks.GetTruck;

public record TruckResponse(string Code, string Name, string StatusCode, string StatusDescription, string? Description);