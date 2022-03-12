using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.Services.Helpers;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class ServicesService
    {
        private readonly ReservationDbContext reservationDbContext;

        public ServicesService(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public async Task<ObjectResult> GetServices()
        {
            var services = await reservationDbContext.Services
                .Select(x => new ServiceDto
                {
                    Id = x.Id,
                    MaxAmountOfUsers = x.MaxAmountUsers,
                    MaxTimeOfUse = x.MaxTimeOfUse.TotalMinutes,
                    Dormitory = x.Dormitory.Name,
                    Room = x.Room.RoomName,
                    Type = x.Type.GetUserFriendlyServiceType(),
                })
                .ToListAsync();
            
            return new ObjectResult(services)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
    }
}