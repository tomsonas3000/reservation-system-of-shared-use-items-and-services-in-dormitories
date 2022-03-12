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

            builder.HasOne(x => x.Dormitory)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.DormitoryId);

            builder.Property(x => x.MaxAmountUsers)
                .IsRequired();

            builder.Property(x => x.MaxTimeOfUse);

            builder.Property(x => x.Type);

            builder.HasMany(x => x.ReservationsList)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}