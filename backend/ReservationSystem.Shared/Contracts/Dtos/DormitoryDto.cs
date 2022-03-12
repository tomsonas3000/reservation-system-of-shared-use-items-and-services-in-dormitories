using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class DormitoryDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string ManagerEmail { get; set; }
        
        public string ManagerPhoneNumber { get; set; }
    }
}