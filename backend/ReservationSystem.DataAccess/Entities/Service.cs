using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public class Service : EntityBase
    {
        private List<Reservation> reservationsList = new();
        
        public string Name { get; protected set; }
        
        public ServiceType Type { get; protected set; }

        public TimeSpan MaxTimeOfUse { get; protected set; }

        public Guid RoomId { get; protected set; }

        public Room Room { get; protected set; }
        
        public Guid DormitoryId { get; protected set; }
        
        public Dormitory Dormitory { get; protected set; }

        public ICollection<Reservation> ReservationsList => reservationsList;
        
        public static Result<Service> Create(string name, string? type, int maxTimeOfUse, Guid roomId, Guid dormitoryId)
        {
            var result = new Result<Service>();
            var nameResult = RequiredString.Create(result, name, "name", 200);
            var typeResult = RequiredEnum<ServiceType>.Create(result, type, "type");
            var maxTimeOfUseResult = FixedNumber.Create(result, maxTimeOfUse, "maxTimeOfUse", 10, 400);

            if (!result.IsSuccess)
            {
                return result;
            }

            return new Result<Service>(new Service
            {
                Name = nameResult!.Value.Value,
                Type = typeResult!.Value.Value,
                MaxTimeOfUse = TimeSpan.FromMinutes(maxTimeOfUseResult.Value.Value),
                DormitoryId = dormitoryId,
                RoomId = roomId,
            });
        }

        public Result<Service> Update(string name, string? type, int maxTimeOfUse, Guid roomId,
            Guid dormitoryId)
        {
            var result = new Result<Service>();
            var nameResult = RequiredString.Create(result, name, "name", 200);
            var typeResult = RequiredEnum<ServiceType>.Create(result, type, "type");
            var maxTimeOfUseResult = FixedNumber.Create(result, maxTimeOfUse, "maxTimeOfUse", 10, 400);
            
            if (typeResult is not null && typeResult.IsSuccess && Type != typeResult.Value.Value)
            {
                result.AddError("type", "Type can not be updated.");
            }
            
            if (!result.IsSuccess)
            {
                return result;
            }

            Name = nameResult!.Value.Value;
            Type = typeResult!.Value.Value;
            MaxTimeOfUse = TimeSpan.FromMinutes(maxTimeOfUseResult.Value.Value);
            DormitoryId = dormitoryId;
            RoomId = roomId;

            return result;
        }

        public Result<Service> AddReservation(Reservation reservation)
        {
            var result = new Result<Service>();

            if (reservationsList.OrderBy(x => x.BeginTime).Any(x => x.BeginTime < reservation.EndTime && x.EndTime > reservation.BeginTime))
            {
                result.AddError(nameof(reservationsList), "The time is already taken.");
                return result;
            }
            
            reservationsList.Add(reservation);

            return new Result<Service>(this);
        }
    }
}