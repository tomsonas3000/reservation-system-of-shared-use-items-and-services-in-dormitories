using System.Collections.Generic;

namespace ReservationSystem.DataAccess.Entities
{
    public class Dormitory : EntityBase
    {
        private readonly List<User> _usersList = new();
        private readonly List<Service> _servicesList = new();
        private readonly List<Room> _roomsList = new();

        private Dormitory(string address, string city)
        {
            Address = address;
            City = city;
        }

        public string Address { get; }
        
        public string City { get; }

        public ICollection<User> Users => _usersList.AsReadOnly();
        
        public ICollection<Service> Services => _servicesList.AsReadOnly();

        public ICollection<Room> Rooms => _roomsList.AsReadOnly();
    }
}