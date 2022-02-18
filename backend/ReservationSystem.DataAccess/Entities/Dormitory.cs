using System.Collections.Generic;

namespace ReservationSystem.DataAccess.Entities
{
    public class Dormitory : EntityBase
    {
        private readonly List<User> usersList = new();
        private readonly List<Service> servicesList = new();
        private readonly List<Room> roomsList = new();

        private Dormitory()
        {
            
        }

        public string Address { get; }
        
        public string City { get; }

        public ICollection<User> Users => usersList.AsReadOnly();
        
        public ICollection<Service> Services => servicesList.AsReadOnly();

        public ICollection<Room> Rooms => roomsList.AsReadOnly();
    }
}