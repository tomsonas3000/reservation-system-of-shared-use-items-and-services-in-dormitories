using System;
using System.Collections.Generic;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class DormitoryDetailsDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get;set; }
        
        public string City { get; set; }
        
        public Guid ManagerId { get; set; }
        
        public List<string> Rooms { get; set; }
    }
}