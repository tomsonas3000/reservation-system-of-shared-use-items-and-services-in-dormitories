using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ServiceDetailsDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Type { get; set; }
        
        public double MaxTimeOfUse { get; set; }
        
        public int MaxAmountOfUsers { get; set; }
        
        public Guid RoomId { get; set; }
        
        public Guid DormitoryId { get; set; }
    }
}