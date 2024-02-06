// using Erpi.Trucks.Domain.Trucks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Erpi.Trucks.Infrastructure.Database.Domain;
//
// public class TruckEntityTypeConfiguration : IEntityTypeConfiguration<Truck>
// {
//     public void Configure(EntityTypeBuilder<Truck> entity)
//     {
//             entity.HasKey(x => x.Id);
//             entity.OwnsOne<AlphanumericCode>(t => t.Code, code =>
//             {
//                 code.Property(c => c.Code).HasColumnName("code").IsRequired();
//             });
//             entity.OwnsOne<TruckStatus>(t => t.Status, status =>
//             {
//                 status.Property(x => x.Code).HasColumnName("status_code").IsRequired();
//             });
//             entity.Property(p => p.Name).IsRequired();
//             entity.Property(p => p.Description);
//             // entity.Property(p => p.RowVersion).IsRowVersion();
//     }
// }