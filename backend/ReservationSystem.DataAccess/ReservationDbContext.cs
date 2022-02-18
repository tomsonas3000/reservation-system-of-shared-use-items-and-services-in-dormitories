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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        }
    }
}