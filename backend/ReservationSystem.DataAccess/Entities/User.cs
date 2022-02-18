using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.DataAccess.Entities
{
    public class User : IdentityUser
    {
        private readonly List<Reservation> _reservationsList = new();

        public ICollection<Reservation> Reservations => _reservationsList.AsReadOnly();

        public string Name { get; }
        
        public string Surname { get; }

        public Dormitory? Dormitory { get; }
    }
}