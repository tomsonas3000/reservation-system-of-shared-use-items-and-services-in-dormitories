using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("8D411ABA-7E5E-4FF9-9AFF-430B89BEB461"),
                UserId = Guid.Parse("D774AF34-077A-4EC9-9A8F-0784A6E76DDE")
            });
        }
    }
}