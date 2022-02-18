using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.DataAccess.Entities
{
    public sealed class User : IdentityUser
    {
        private readonly List<Reservation> _reservationsList = new();

        private User(string name, string surname, Dormitory? dormitory, string email, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            Dormitory = dormitory;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public ICollection<Reservation> Reservations => _reservationsList.AsReadOnly();

        public string Name { get; }
        
        public string Surname { get; set; }

        public Dormitory? Dormitory { get; }
    }
}