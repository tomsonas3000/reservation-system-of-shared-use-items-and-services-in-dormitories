using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class IdentityRoleTypeConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(new IdentityRole<Guid>
            {
                Id = Guid.Parse("8D411ABA-7E5E-4FF9-9AFF-430B89BEB461"),
                Name = UserRole.Admin.ToString(),
                ConcurrencyStamp = "1",
                NormalizedName = UserRole.Admin.ToString().ToUpper()
            }, new IdentityRole<Guid>
            {
                Id = Guid.Parse("F46A30FC-9425-4275-A245-D389387D84A8"),
                Name = UserRole.Manager.ToString(),
                ConcurrencyStamp = "3",
                NormalizedName = UserRole.Manager.ToString().ToUpper()
            }, new IdentityRole<Guid>
            {
                Id = Guid.Parse("89494DE6-9587-4EE5-B3F1-AC171C52911B"),
                Name = UserRole.Student.ToString(),
                ConcurrencyStamp = "4",
                NormalizedName = UserRole.Student.ToString().ToUpper()
            });
        }
    }
}