using System;
using System.Collections.Generic;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ServiceListDto
    {
        public Guid Id { get; set; }
        
        public int MaximumTimeOfUse { get; set; }
        
        public string Room { get; set; }
        
        public List<ReservationsListDto> ReservationsList { get; set; }
        
        public string Name { get; set; }
    }
}