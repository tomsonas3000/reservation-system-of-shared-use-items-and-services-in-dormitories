using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Services.Helpers;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class ReservationsService
    {
        private readonly ReservationDbContext reservationDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public ReservationsService(ReservationDbContext reservationDbContext, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.reservationDbContext = reservationDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
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

        public async Task<ObjectResult> GetReservationsDataForCalendar()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
            var dormitoryId = user.DormitoryId;
            var services = await reservationDbContext.Services
                .Include(x => x.ReservationsList)
                .Include(x => x.Room)
                .Where(x => x.DormitoryId == dormitoryId)
                .ToListAsync();

            var reservationsData = services.GroupBy(x => x.Type)
                .Select(x => new ServiceTypeDto
                {
                    Type = x.Key.ToString(),
                    ServiceList = x.Select(service => new ServiceListDto
                    {
                        MaximumTimeOfUse = service.MaxTimeOfUse.Minutes,
                        Room = service.Room.RoomName,
                        ReservationsList = service.ReservationsList.Select(reservation => new ReservationsListDto
                        {
                            Event_id = reservation.Id,
                            Start = reservation.BeginTime,
                            End = reservation.EndTime,
                            IsBooked = reservation.IsFinished,
                        }).ToList()
                    }).ToList()
                }).ToList();

            return new ObjectResult(reservationsData)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
    }
}