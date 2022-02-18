using System.Collections.Generic;

namespace ReservationSystem.DataAccess.Entities
{
    public class Room : EntityBase
    {
        private readonly List<Service> _servicesList = new();

        private Room(string roomName, Dormitory dormitory)
        {
            RoomName = roomName;
            Dormitory = dormitory;
        }

        public string RoomName { get; }
        
        public Dormitory Dormitory { get; }

        public ICollection<Service> Services => _servicesList.AsReadOnly();
    }
}