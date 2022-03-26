using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ReservationsListDto
    {
        public Guid Event_id { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }

        public bool IsBooked { get; set; }
    }
}