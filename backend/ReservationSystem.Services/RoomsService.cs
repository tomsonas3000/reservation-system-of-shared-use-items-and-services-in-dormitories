using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class RoomsService
    {
        private readonly ReservationDbContext reservationDbContext;

        public RoomsService(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public async Task<ObjectResult> GetRooms()
        {
            var rooms = await reservationDbContext.Rooms.Select(x => new RoomDto
            {
                Id = x.Id,
                DormitoryId = x.Dormitory.Id.ToString(),
                Name = x.RoomName,
            }).OrderBy(x => x.Name).ToListAsync();

            return new ObjectResult(rooms)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
    }
}