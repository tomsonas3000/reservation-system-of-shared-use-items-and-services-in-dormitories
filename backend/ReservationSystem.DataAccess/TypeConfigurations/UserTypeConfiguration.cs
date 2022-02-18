using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Surname)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.NormalizedEmail)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasMaxLength(200);

            builder.Property(x => x.NormalizedUserName)
                .HasMaxLength(200);

            builder.HasMany(x => x.Reservations)
                .WithOne(x => x.User);
        }
    }
}