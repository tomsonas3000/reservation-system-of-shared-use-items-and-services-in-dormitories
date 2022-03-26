using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Type { get; set; }
        
        public double MaxTimeOfUse { get; set; }
        
        public int MaxAmountOfUsers { get; set; }
        
        public string Room { get; set; }
        
        public string Dormitory { get; set; }
    }
}