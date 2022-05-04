using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.DataAccess.Entities;
#pragma warning disable 8634, 8621, 8622

namespace ReservationSystem.DataAccess.TypeConfigurations
{
    public class DormitoryTypeConfiguration : EntityBaseTypeConfiguration<Dormitory>
    {
        public override void Configure(EntityTypeBuilder<Dormitory> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.Residents)
                .WithOne(x => x!.Dormitory);

            builder.HasMany(x => x.Services)
                .WithOne(x => x.Dormitory);

            builder.HasMany(x => x.Rooms)
                .WithOne(x => x.Dormitory);

            builder.HasOne(x => x.Manager)
                .WithMany()
                .HasForeignKey(x => x.ManagerId);
        }
    }
}