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
    public class ReservationsService
    {
        private readonly ReservationDbContext reservationDbContext;

        public ReservationsService(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public async Task<ObjectResult> GetReservations()
        {
            var reservations = await reservationDbContext.ReservationDates
                .Select(x => new ReservationDto
                {
                    BeginTime = x.BeginTime.GetUserFriendlyDateTime(),
                    EndTime = x.EndTime.GetUserFriendlyDateTime(),
                    IsFinished = x.IsFinished,
                    ServiceType = x.Service.Type.GetUserFriendlyServiceType(),
                    UserName = $"{x.User.Name} {x.User.Surname}",
                    Dormitory = x.Service.Dormitory.Name,
                })
                .ToListAsync();

            return new ObjectResult(reservations)
            {
                StatusCode = (int?) HttpStatusCode.OK,
            };
        }
    }
}