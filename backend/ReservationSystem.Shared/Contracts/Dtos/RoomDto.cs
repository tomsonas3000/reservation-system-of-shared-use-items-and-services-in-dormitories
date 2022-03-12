using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string DormitoryId { get; set; }
    }
}