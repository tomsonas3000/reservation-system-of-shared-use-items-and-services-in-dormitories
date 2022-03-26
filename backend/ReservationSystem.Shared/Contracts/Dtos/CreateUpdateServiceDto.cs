using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class CreateUpdateServiceDto
    {
        public string Name { get; set; }
        
        public string? Type { get; set; }
        
        public int MaxTimeOfUse { get; set; }

        public Guid Room { get; set; }
        
        public Guid Dormitory { get; set; }
    }
}