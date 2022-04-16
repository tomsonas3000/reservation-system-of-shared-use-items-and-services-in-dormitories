using System;

namespace ReservationSystem.EmailsWorker
{
    public class ReservationEmailData
    {
        public Guid ReservationId { get; set; }
        
        public string RecipientEmail { get; set; }
        
        public string ServiceName { get; set; }
        
        public string RoomName { get; set; }
    }
}