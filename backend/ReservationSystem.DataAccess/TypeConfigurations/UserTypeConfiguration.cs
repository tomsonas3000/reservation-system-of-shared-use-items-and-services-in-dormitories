using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class UserTypeConfiguration : EntityBaseTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Surname)
                .HasMaxLength(100);

            builder.Property(x => x.EmailAddress)
                .HasMaxLength(100);

            builder.Property(x => x.TelephoneNumber)
                .HasMaxLength(100);

            builder.HasMany(x => x.Reservations)
                .WithOne(x => x.User);

            builder.Property(x => x.Role)
                .IsRequired();
        }
    }
}