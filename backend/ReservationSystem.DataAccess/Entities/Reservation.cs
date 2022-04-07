using System;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public class Reservation : EntityBase
    {
        private Reservation()
        {
        }
        
        public static Result<Reservation> Create(Guid serviceId, User user, string beginTime, string endTime)
        {
            var result = new Result<Reservation>();
            var beginTimeResult = RequiredDate.Create(result, beginTime);
            var endTimeResult = RequiredDate.Create(result, endTime);

            if (!result.IsSuccess)
            {
                return result;
            }
            
            return new Result<Reservation>(new Reservation
            {
                ServiceId = serviceId,
                BeginTime = beginTimeResult.Value.Value,
                EndTime = endTimeResult.Value.Value,
                User = user,
            });
        }
        
        public DateTime BeginTime { get; protected set; }
        
        public DateTime EndTime { get; protected set; }

        public Guid ServiceId { get; protected set; }
        
        public Service Service { get; protected set; }
        
        public bool IsFinished { get; }

        public User User { get; protected set; }
    }
}