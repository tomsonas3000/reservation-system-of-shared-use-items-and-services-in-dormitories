using System;
using System.Collections.Generic;

namespace ReservationSystem.DataAccess.Entities
{
    public class Dormitory : EntityBase
    {
        private readonly List<User> residentsList = new();
        private readonly List<Service> servicesList = new();
        private readonly List<Room> roomsList = new();

        private Dormitory()
        {
            
        }

        public string Address { get; }
        
        public string City { get; }
        
        public User Manager { get; set; }
        
        public Guid ManagerId { get; set; }

        public ICollection<User> Residents => residentsList.AsReadOnly();
        
        public ICollection<Service> Services => servicesList.AsReadOnly();

        public ICollection<Room> Rooms => roomsList.AsReadOnly();
    }
}