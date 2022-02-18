using System;

namespace ReservationSystem.DataAccess.Entities
{
    public class Reservation : EntityBase
    {
        private Reservation(DateTime beginTime, DateTime endTime, Service service, bool isFinished, User user)
        {
            BeginTime = beginTime;
            EndTime = endTime;
            Service = service;
            IsFinished = isFinished;
            User = user;
        }
        public DateTime BeginTime { get; }
        
        public DateTime EndTime { get; }

        public Service Service { get; }
        
        public bool IsFinished { get; }
        
        public User User { get; }
    }
}