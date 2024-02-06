using Erpi.Trucks.Domain.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Erpi.Trucks.Application.Database;

public interface ITruckDbContext
{
    DbSet<Truck> Trucks { get; set; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}