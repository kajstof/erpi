using Erpi.Trucks.Application.Database;
using Erpi.Trucks.Domain.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Erpi.Trucks.Infrastructure.Database;

public class TruckDbContext : DbContext, ITruckDbContext
{
    private const string SchemaName = "public";

    public DbSet<Truck> Trucks { get; set; } = default!;

    public TruckDbContext(DbContextOptions<TruckDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    
        builder.Entity<Truck>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.OwnsOne<AlphanumericCode>(t => t.Code, code =>
            {
                code.Property(c => c.Code).HasColumnName("code").IsRequired();
            });
            entity.OwnsOne<TruckStatus>(t => t.Status, status =>
            {
                status.Property(x => x.Code).HasColumnName("status_code").IsRequired();
            });
            entity.Property(p => p.Name).IsRequired();
            // entity.Property(p => p.Status).IsRequired();
            entity.Property(p => p.Description);
            // entity.Property(p => p.RowVersion).IsRowVersion();
        }).HasDefaultSchema(SchemaName);
    }
}