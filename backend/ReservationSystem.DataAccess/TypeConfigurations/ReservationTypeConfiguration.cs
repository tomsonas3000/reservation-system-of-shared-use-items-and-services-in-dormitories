using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class ReservationTypeConfiguration : EntityBaseTypeConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Reservations);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.ReservationsList)
                .HasForeignKey(x => x.ServiceId);

            builder.Property(x => x.BeginTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.IsFinished)
                .IsRequired();
        }
    }
}