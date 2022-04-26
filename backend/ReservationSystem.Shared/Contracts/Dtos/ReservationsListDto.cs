using System;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class ReservationsListDto
    {
        public Guid Id { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Title { get; set; }

        public bool IsBookedByUser { get; set; }
    }
}