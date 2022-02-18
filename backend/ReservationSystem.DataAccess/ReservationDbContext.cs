using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.DataAccess
{
    public class ReservationDbContext : IdentityDbContext<User>
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
        {
        }

        public DbSet<Dormitory> Dormitories { get; set; } = null!;

        public DbSet<Reservation> ReservationDates { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public override DbSet<User> Users { get; set; } = null!;

        public DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(x => x.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<User>().Property(x => x.Surname)
                .HasMaxLength(100);

            modelBuilder.Entity<User>().HasMany(x => x.Reservations)
                .WithOne(x => x.User);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "8D411ABA-7E5E-4FF9-9AFF-430B89BEB461",
                Name = UserRole.Admin.ToString(),
                ConcurrencyStamp = "1",
                NormalizedName = UserRole.Admin.ToString().ToUpper()
            }, new IdentityRole
            {
                Id = "32928326-7E01-47E7-9427-E7742B691F6B",
                Name = UserRole.Guard.ToString(),
                ConcurrencyStamp = "2",
                NormalizedName = UserRole.Guard.ToString().ToUpper()
            }, new IdentityRole
            {
                Id = "F46A30FC-9425-4275-A245-D389387D84A8",
                Name = UserRole.Manager.ToString(),
                ConcurrencyStamp = "3",
                NormalizedName = UserRole.Manager.ToString().ToUpper()
            }, new IdentityRole
            {
                Id = "89494DE6-9587-4EE5-B3F1-AC171C52911B",
                Name = UserRole.Student.ToString(),
                ConcurrencyStamp = "4",
                NormalizedName = UserRole.Student.ToString().ToUpper()
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        }
    }
}