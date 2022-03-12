using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class CreateServiceDto
    {
        public string? Type { get; set; }
        
        public int MaxTimeOfUse { get; set; }
        
        public int MaxAmountOfUsers { get; set; }
        
        public Guid Room { get; set; }
        
        public Guid Dormitory { get; set; }
    }
}