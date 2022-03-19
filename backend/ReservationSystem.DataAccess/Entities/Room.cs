using System;
using System.Collections.Generic;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public class Room : EntityBase
    {
        private readonly List<Service> servicesList = new();

        public static Result<Room>? Create(Result result, string name)
        {
            var nameResult = RequiredString.Create(result, name, "Rooms", 100);

            if (!nameResult.IsSuccess)
            {
                return null;
            }
            
            return new Result<Room>(new Room
            {
                RoomName = nameResult.Value.Value,
            });
        }

        public string RoomName { get; protected set; }

        public Dormitory Dormitory { get; protected set; }

        public ICollection<Service> Services => servicesList;
    }
}