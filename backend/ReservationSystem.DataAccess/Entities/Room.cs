using System;
using System.Collections.Generic;

namespace ReservationSystem.DataAccess.Entities
{
    public class Room : EntityBase
    {
        private readonly List<Service> servicesList = new();

        private Room()
        {
        }

        public string RoomName { get; }

        public Dormitory Dormitory { get; }

        public ICollection<Service> Services => servicesList.AsReadOnly();
    }
}