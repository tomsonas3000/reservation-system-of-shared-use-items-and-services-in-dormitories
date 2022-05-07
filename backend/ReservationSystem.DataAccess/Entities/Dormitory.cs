using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.DataAccess.Entities
{
    public class Dormitory : EntityBase
    {
        private List<User?> residentsList = new();
        private readonly List<Room> roomsList = new();

        public static Result<Dormitory> Create(string name, string city, string address, Guid managerId)
        {
            var result = new Result<Dormitory>();
            var nameResult = RequiredString.Create(result, name, nameof(Name), 100);
            var cityResult = RequiredString.Create(result, city, nameof(City), 100);
            var addressResult = RequiredString.Create(result, address, nameof(Address), 100);

            if (!result.IsSuccess)
            {
                return result;
            }

            return new Result<Dormitory>(new Dormitory
            {
                Name = nameResult!.Value.Value,
                Address = addressResult!.Value.Value,
                City = cityResult!.Value.Value,
                ManagerId = managerId,
            });
        }
        
        public Result<Dormitory> Update(string name, string city, string address, Guid managerId)
        {
            var result = new Result<Dormitory>();
            var nameResult = RequiredString.Create(result, name, nameof(Name), 100);
            var cityResult = RequiredString.Create(result, city, nameof(City), 100);
            var addressResult = RequiredString.Create(result, address, nameof(Address), 100);

            if (!result.IsSuccess)
            {
                return result;
            }

            Name = nameResult!.Value.Value;
            Address = addressResult!.Value.Value;
            City = cityResult!.Value.Value;
            ManagerId = managerId;

            return result;
        }

        public Result<Dormitory> AddRooms(List<string> rooms)
        {
            var result = new Result<Dormitory>();
            foreach (var room in rooms)
            {
                var createRoomResult = Room.Create(result, room);
                if (createRoomResult is null || !createRoomResult.IsSuccess)
                {
                    return result;
                }

                if (roomsList.Exists(x => x.RoomName == createRoomResult!.Value.RoomName))
                {
                    result.AddError("rooms", "Rooms must be unique.");
                    return result;
                }

                roomsList.Add(createRoomResult!.Value);
            }

            return result;
        }

        public Result<Dormitory> UpdateRooms(List<string> rooms)
        {
            var result = new Result<Dormitory>();

            if (!roomsList.All(existingRoom => rooms.Contains(existingRoom.RoomName)))
            {
                result.AddError("rooms", "Rooms can't be deleted");
                return result;
            }

            foreach (var room in rooms)
            {
                if (!roomsList.Select(x => x.RoomName).Contains(room))
                {
                    var createRoomResult = Room.Create(result, room);
                    if (createRoomResult is null || !createRoomResult.IsSuccess)
                    {
                        return result;
                    }

                    roomsList.Add(createRoomResult!.Value);
                }
            }

            return result;
        }

        public Result<Dormitory> UpdateResidents(List<User?> students)
        {
            var result = new Result<Dormitory>();

            residentsList = students;

            return result;
        }

        public string Name { get; protected set; }

        public string Address { get; protected set; }

        public string City { get; protected set; }

        public User Manager { get; protected set; }

        public Guid ManagerId { get; protected set; }

        public ICollection<User?> Residents => residentsList;
        
        public ICollection<Room> Rooms => roomsList;
    }
}