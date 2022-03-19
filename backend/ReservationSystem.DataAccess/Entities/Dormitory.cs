using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public class Dormitory : EntityBase
    {
        private readonly List<User> residentsList = new();
        private readonly List<Service> servicesList = new();
        private readonly List<Room> roomsList = new();

        public static Result<Dormitory> Create(string name, string city, string address, User manager)
        {
            var result = new Result<Dormitory>();
            var nameResult = RequiredString.Create(result, name, nameof(Name), 100);
            var cityResult = RequiredString.Create(result, name, nameof(Name), 100);
            var addressResult = RequiredString.Create(result, name, nameof(Name), 100);

            if (!result.IsSuccess)
            {
                return result;
            }

            return new Result<Dormitory>(new Dormitory
            {
                Name = nameResult.Value.Value,
                Address = addressResult.Value.Value,
                City = cityResult.Value.Value,
                ManagerId = manager.Id,
            });
        }

        public Result<Dormitory> AddRooms(List<string> rooms)
        {
            var result = new Result<Dormitory>();
            foreach(var room in rooms)
            {
                var createRoomResult = Room.Create(result, room);
                if (createRoomResult is not null && !createRoomResult.IsSuccess)
                {
                    return result;
                }

                if (roomsList.Exists(x => x.RoomName == createRoomResult.Value.RoomName))
                {
                    result.AddError("rooms", "Rooms must be unique.");
                    return result;
                }
                roomsList.Add(createRoomResult.Value);
            }

            return result;
        }
        
        public string Name { get; protected set; }

        public string Address { get; protected set; }
        
        public string City { get; protected set; }
        
        public User Manager { get; protected set; }
        
        public Guid ManagerId { get; protected set; }

        public ICollection<User> Residents => residentsList;
        
        public ICollection<Service> Services => servicesList;

        public ICollection<Room> Rooms => roomsList;
    }
}