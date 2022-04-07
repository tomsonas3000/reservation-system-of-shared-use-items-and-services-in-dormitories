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
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Services.Helpers;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class ReservationsService
    {
        private readonly ReservationDbContext reservationDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly ServicesRepository servicesRepository;
        private readonly UsersService usersService;
        private readonly ReservationsRepository reservationsRepository;

        public ReservationsService(ReservationDbContext reservationDbContext, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, 
        ServicesRepository servicesRepository, UsersService usersService, UsersRepository usersRepository, ReservationsRepository reservationsRepository)
        {
            this.reservationDbContext = reservationDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.servicesRepository = servicesRepository;
            this.usersService = usersService;
            this.reservationsRepository = reservationsRepository;
        }

        public async Task<ObjectResult> GetReservations()
        {
            var reservations = await reservationDbContext.ReservationDates
                .OrderBy(x => x.BeginTime)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
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
                    Type = new LookupDto
                    {
                        Name = x.Key.GetUserFriendlyServiceType(),
                        Value = x.Key.ToString(),
                    },
                    ServiceList = x.Select(service => new ServiceListDto
                    {
                        Id = service.Id,
                        MaximumTimeOfUse = service.MaxTimeOfUse.Minutes,
                        Room = service.Room.RoomName,
                        Name = service.Name,
                        ReservationsList = service.ReservationsList.Select(reservation => new ReservationsListDto
                        {
                            Event_id = reservation.Id,
                            StartDate = reservation.BeginTime,
                            EndDate = reservation.EndTime,
                            IsBooked = reservation.IsFinished,
                            Title = service.Room.RoomName,
                        }).ToList()
                    }).ToList()
                }).ToList();

            return new ObjectResult(reservationsData)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> CreateReservation(CreateReservationDto request)
        {
            var service = await reservationDbContext.Services.Include(x => x.ReservationsList).FirstOrDefaultAsync(x => x.Id == request.ServiceId);
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
            var userEntity = await usersService.GetUserById(user.Id);
            

            if (service is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.NotFound,
                };
            }

            var reservationCreateResult = Reservation.Create(request.ServiceId, user, request.StartDate, request.EndDate);
            
            var addToServiceResult = service.AddReservation(reservationCreateResult.Value);

            if (!addToServiceResult.IsSuccess)
            {
                return new ObjectResult(addToServiceResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            userEntity.AddReservation(reservationCreateResult.Value);
            
            await servicesRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> DeleteReservation(Guid reservationId)
        {
            var reservation = await reservationDbContext.ReservationDates.FindAsync(reservationId);

            if (reservation is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.NotFound,
                };
            }
            
            reservationsRepository.DeleteReservation(reservation);
            await reservationsRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
    }
}