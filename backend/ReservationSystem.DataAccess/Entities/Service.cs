using System;
using System.Collections.Generic;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public class Service : EntityBase
    {
        private readonly List<Reservation> reservationsList = new();
        
        public ServiceType Type { get; protected set; }

        public TimeSpan MaxTimeOfUse { get; protected set; }

        public int MaxAmountUsers { get; protected set; }
        
        public Guid RoomId { get; protected set; }

        public Room Room { get; protected set; }
        
        public Guid DormitoryId { get; protected set; }
        
        public Dormitory Dormitory { get; protected set; }

        public ICollection<Reservation> ReservationsList => reservationsList;
        
        

        public static Result<Service> Create(string? type, int maxTimeOfUse, int maxAmountOfUsers, Guid roomId, Guid dormitoryId)
        {
            var result = new Result<Service>();
            var typeResult = RequiredEnum<ServiceType>.Create(result, type, "type");
            var maxTimeOfUseResult = FixedNumber.Create(result, maxTimeOfUse, "maxTimeOfUse", 10, 400);
            var maxAmountOfUsersResult = FixedNumber.Create(result, maxAmountOfUsers, "maxAmountOfUsers", 1, 20);

            if (!result.IsSuccess)
            {
                return result;
            }

            return new Result<Service>(new Service
            {
                Type = typeResult.Value.Value,
                MaxAmountUsers = maxAmountOfUsersResult.Value.Value,
                MaxTimeOfUse = TimeSpan.FromMinutes(maxTimeOfUseResult.Value.Value),
                DormitoryId = dormitoryId,
                RoomId = roomId,
            });
        }

        public Result<Service> Update(string? type, int maxTimeOfUse, int maxAmountOfUsers, Guid roomId,
            Guid dormitoryId)
        {
            var result = new Result<Service>();
            var typeResult = RequiredEnum<ServiceType>.Create(result, type, "type");
            var maxTimeOfUseResult = FixedNumber.Create(result, maxTimeOfUse, "maxTimeOfUse", 10, 400);
            var maxAmountOfUsersResult = FixedNumber.Create(result, maxAmountOfUsers, "maxAmountOfUsers", 1, 20);

            if (!result.IsSuccess)
            {
                return result;
            }

            Type = typeResult.Value.Value;
            MaxAmountUsers = maxAmountOfUsersResult.Value.Value;
            MaxTimeOfUse = TimeSpan.FromMinutes(maxTimeOfUseResult.Value.Value);
            DormitoryId = dormitoryId;
            RoomId = roomId;

            return result;
        }
    }
}