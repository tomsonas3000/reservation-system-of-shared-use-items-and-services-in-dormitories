using System.Collections.Generic;

namespace ReservationSystem.DataAccess.Entities
{
    public class Room : EntityBase
    {
        private readonly List<Service> _servicesList = new();
        
        public string RoomName { get; }
        
        public Dormitory Dormitory { get; }

        public ICollection<Service> Services => _servicesList.AsReadOnly();
    }
}