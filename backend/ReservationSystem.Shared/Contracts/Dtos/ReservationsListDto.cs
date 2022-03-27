using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ReservationsListDto
    {
        public Guid Event_id { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Title { get; set; }

        public bool IsBooked { get; set; }
    }
}