using System;
using Microsoft.AspNetCore.Identity;
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

            builder.HasOne(x => x.Dormitory);
            
            var hasher = new PasswordHasher<User>();
            var adminId = Guid.Parse("D774AF34-077A-4EC9-9A8F-0784A6E76DDE");
            
            builder.HasData(new User
            {
                Id = adminId,
                Name = "Admin",
                Surname = "Admin",
                PhoneNumber = "",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@reservations.com",
                NormalizedEmail = "ADMIN@RESERVATIONS.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = string.Empty
            });
        }
    }
}