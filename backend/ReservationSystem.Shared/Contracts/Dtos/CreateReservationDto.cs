using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class CreateReservationDto
    {
        public Guid ServiceId { get; set; }
        
        public string StartDate { get; set; }
        
        public string EndDate { get; set; }
    }
}