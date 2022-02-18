using System;

namespace ReservationSystem.DataAccess.Entities
{
    public class Reservation : EntityBase
    {
        private Reservation()
        {
        }
        
        public DateTime BeginTime { get; }
        
        public DateTime EndTime { get; }

        public Service Service { get; }
        
        public bool IsFinished { get; }

        public User User { get; }
    }
}