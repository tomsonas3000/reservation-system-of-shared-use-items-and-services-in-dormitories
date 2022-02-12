using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options): base(options)
        {
        }
        
        public DbSet<Dormitory> Dormitories { get; set; } = null!;

        public DbSet<Reservation> ReservationDates { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        }
    }
}