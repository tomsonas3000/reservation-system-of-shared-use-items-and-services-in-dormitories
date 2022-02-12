using System.Collections.Generic;
using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.DataAccess.Entities
{
    public class User : EntityBase
    {
        private readonly List<Reservation> _reservationsList = new();

        public ICollection<Reservation> Reservations => _reservationsList.AsReadOnly();

        public string Name { get; }
        
        public string Surname { get; }
        
        public string EmailAddress { get; }
        
        public string TelephoneNumber { get; }
        
        public UserRole Role { get; }
        
        public Dormitory? Dormitory { get; }
    }
}