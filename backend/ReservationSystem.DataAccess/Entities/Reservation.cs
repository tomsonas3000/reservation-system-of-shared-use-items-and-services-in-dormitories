using System;

namespace ReservationSystem.DataAccess.Entities
{
    public class Reservation : EntityBase
    {
        private Reservation()
        {
        }
        
        public DateTime BeginTime { get; protected set; }
        
        public DateTime EndTime { get; protected set; }

        public Guid ServiceId { get; protected set; }
        
        public Service Service { get; protected set; }
        
        public bool IsFinished { get; }

        public User User { get; }
    }
}