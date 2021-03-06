using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class ServiceTypeConfiguration : EntityBaseTypeConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Room)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.RoomId);

            builder.Property(x => x.Name).HasMaxLength(200);

            builder.HasMany(x => x.ReservationsList)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}