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
        
        public static Result<Reservation> Create(Service service, User user, string beginTime, string endTime)
        {
            var result = new Result<Reservation>();
            var beginTimeResult = RequiredDate.Create(result, beginTime);
            var endTimeResult = RequiredDate.Create(result, endTime);

            if (beginTimeResult!.IsSuccess && endTimeResult!.IsSuccess)
            {
                if (endTimeResult.Value.Value.Subtract(beginTimeResult.Value.Value) > service.MaxTimeOfUse)
                {
                    result.AddError("MaxTimeOfUse", "The selected time span is too big for this service.");
                }

                if (endTimeResult.Value.Value < DateTime.Now)
                {
                    result.AddError(nameof(EndTime), "Reservation can not be made for a past time.");
                }
            }

            if (user.IsBannedFromReserving)
            {
                result.AddError(nameof(user.IsBannedFromReserving), "Administrator has disabled ability to add new reservations.");
            }

            if (!result.IsSuccess)
            {
                return result;
            }
            
            return new Result<Reservation>(new Reservation
            {
                Service = service,
                BeginTime = beginTimeResult.Value.Value,
                EndTime = endTimeResult!.Value.Value,
                UserId = user.Id,
            });
        }
        
        public Result<Reservation> Update(string? beginTime, string? endTime)
        {
            var result = new Result<Reservation>();
            var beginTimeResult = RequiredDate.Create(result, beginTime ?? BeginTime.ToString());
            var endTimeResult = RequiredDate.Create(result, endTime ?? EndTime.ToString());

            if (beginTimeResult!.IsSuccess && endTimeResult!.IsSuccess)
            {
                if (endTimeResult.Value.Value.Subtract(beginTimeResult.Value.Value) > Service.MaxTimeOfUse)
                {
                    result.AddError("MaxTimeOfUse", "The selected time span is too big for this service.");
                }

                if (endTimeResult.Value.Value < DateTime.Now)
                {
                    result.AddError(nameof(EndTime), "Reservation can not be made for a past time.");
                }

                if (endTimeResult.Value.Value < beginTimeResult.Value.Value)
                {
                    result.AddError(nameof(BeginTime), "Reservation start time can not be later that end time.");
                }
            }

            if (User.IsBannedFromReserving)
            {
                result.AddError(nameof(User.IsBannedFromReserving), "Administrator has disabled ability to update reservations.");
            }

            if (!result.IsSuccess)
            {
                return result;
            }
            
            BeginTime = beginTimeResult.Value.Value;
            EndTime = endTimeResult!.Value.Value;

            return new Result<Reservation>(this);
        }
        
        public DateTime BeginTime { get; protected set; }
        
        public DateTime EndTime { get; protected set; }

        public Guid ServiceId { get; protected set; }
        
        public Service Service { get; protected set; }
        
        public bool IsEmailSentOn { get; }
        
        public Guid UserId { get; protected set; }

        public User User { get; protected set; }
    }
}