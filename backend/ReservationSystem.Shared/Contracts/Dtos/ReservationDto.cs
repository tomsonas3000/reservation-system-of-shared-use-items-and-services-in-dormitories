using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        
        public string BeginTime { get; set; }
        
        public string EndTime { get; set; }
        
        public string ServiceType { get; set; }
        
        public bool IsFinished { get; set; }
        
        public string UserName { get; set; }
        
        public string Dormitory { get; set; }
    }
}