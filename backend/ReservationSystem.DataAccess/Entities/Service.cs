using System;
using System.Collections.Generic;
using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.DataAccess.Entities
{
    public class Service : EntityBase
    {
        private readonly List<Reservation> _reservations = new();

        private Service(ServiceType type, Room room, Dormitory dormitory, TimeSpan? maxTimeOfUse, int maxAmountUsers)
        {
            Type = type;
            Room = room;
            Dormitory = dormitory;
            MaxTimeOfUse = maxTimeOfUse;
            MaxAmountUsers = maxAmountUsers;
        }

        public ServiceType Type { get; }

        public TimeSpan? MaxTimeOfUse { get; }

        public int MaxAmountUsers { get; }
        
        public Room Room { get; }
        
        public Dormitory Dormitory { get; }

        public ICollection<Reservation> Reservations => _reservations.AsReadOnly();
    }
}