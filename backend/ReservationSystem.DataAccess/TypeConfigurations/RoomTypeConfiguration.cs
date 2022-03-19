using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class RoomTypeConfiguration : EntityBaseTypeConfiguration<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.RoomName).HasMaxLength(100);

            builder.HasMany(x => x.Services)
                .WithOne(x => x.Room)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Dormitory)
                .WithMany(x => x.Rooms);

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}