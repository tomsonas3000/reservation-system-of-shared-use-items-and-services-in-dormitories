using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess
{
    public class ReservationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
        {
        }

        public DbSet<Dormitory> Dormitories { get; set; } = null!;

        public DbSet<Reservation> ReservationDates { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public override DbSet<User?> Users { get; set; } = null!;

        public DbSet<Room> Rooms { get; set; } = null!;

        public override DbSet<IdentityRole<Guid>> Roles { get; set; } = null!;

        public override DbSet<IdentityUserRole<Guid>> UserRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        }
    }
}