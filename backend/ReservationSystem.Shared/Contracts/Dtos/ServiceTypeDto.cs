using System.Collections.Generic;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ServiceTypeDto
    {
        public LookupDto Type { get; set; }
        
        public List<ServiceListDto> ServiceList { get; set; }
    }
}