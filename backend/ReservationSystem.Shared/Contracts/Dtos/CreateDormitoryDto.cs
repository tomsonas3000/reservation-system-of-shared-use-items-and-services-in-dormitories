using System.Collections.Generic;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class CreateDormitoryDto
    {
        public string Name { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }
        
        public string Manager { get; set; }
        
        public List<string> Rooms { get; set; }
    }
}