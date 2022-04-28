using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
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
        ServicesRepository servicesRepository, UsersService usersService, ReservationsRepository reservationsRepository)
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
                    ServiceType = x.Service.Type.GetUserFriendlyServiceType(),
                    ServiceName = x.Service.Name,
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
            var isManager = await userManager.IsInRoleAsync(user, UserRole.Manager.ToString());
            var dormitoryId = user.DormitoryId ?? (await reservationDbContext.Dormitories.FirstOrDefaultAsync(x => x.ManagerId == user.Id))?.Id;

            if (dormitoryId is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.NotFound,
                };
            }
            
            var services = await reservationDbContext.Services
                .Include(x => x.ReservationsList)
                .ThenInclude(x => x.User)
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
                        MaximumTimeOfUse = (int)service.MaxTimeOfUse.TotalMinutes,
                        Room = service.Room.RoomName,
                        Name = service.Name,
                        ReservationsList = service.ReservationsList.Select(reservation => new ReservationsListDto
                        {
                            Id = reservation.Id,
                            StartDate = reservation.BeginTime,
                            EndDate = reservation.EndTime,
                            Title = isManager ? $"{reservation.User.Name} {reservation.User.Surname}" : service.Room.RoomName,
                            IsBookedByUser = reservation.UserId == user.Id,
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

            var reservationCreateResult = Reservation.Create(service, user, request.StartDate, request.EndDate);
            
            if (!reservationCreateResult.IsSuccess)
            {
                return new ObjectResult(reservationCreateResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            
            var addToServiceResult = service.AddReservation(reservationCreateResult.Value);

            if (!addToServiceResult.IsSuccess)
            {
                return new ObjectResult(addToServiceResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            userEntity.AddReservation(reservationCreateResult.Value);

            if (userEntity.Reservations.Count(x => x.ServiceId == service.Id && x.EndTime > DateTime.Now) >= 3)
            {
                return new ObjectResult(new Dictionary<string, string>
                    { { "Reservations", "Maximum of 3 active or incoming reservations can be created." } })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            if (userEntity.Reservations.Count(x => x.EndTime > DateTime.Now) > 5)
            {
                return new ObjectResult(new Dictionary<string, string>
                    { { "reservations", "Maximum of 5 active or upcoming reservations can be selected in total." } })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            
            await servicesRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
        
        public async Task<ObjectResult> UpdateReservation(UpdateReservationDto request, Guid reservationId)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
            var isStudent = await userManager.IsInRoleAsync(user, UserRole.Student.ToString());
            var reservation = await reservationDbContext.ReservationDates
                .Include(x => x.Service)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == reservationId);

            if (reservation is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.NotFound,
                };
            }
            
            var userEntity = await usersService.GetUserById(reservation.UserId);

            if (user.Id != userEntity.Id && isStudent)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            
            var reservationUpdateResult = reservation.Update(request.StartDate, request.EndDate);
            
            if (!reservationUpdateResult.IsSuccess)
            {
                return new ObjectResult(reservationUpdateResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            
            var addToServiceResult = reservation.Service.AddReservation(reservationUpdateResult.Value);

            if (!addToServiceResult.IsSuccess)
            {
                return new ObjectResult(addToServiceResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            userEntity.AddReservation(reservationUpdateResult.Value);

            if (userEntity.Reservations.Count(x => x.ServiceId == reservation.Service.Id && x.EndTime > DateTime.Now) >= 4)
            {
                return new ObjectResult(new Dictionary<string, string>
                    { { "Reservations", "Maximum of 3 active or incoming reservations can be created." } })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            if (userEntity.Reservations.Count(x => x.EndTime > DateTime.Now) > 5)
            {
                return new ObjectResult(new Dictionary<string, string>
                    { { "reservations", "Maximum of 5 active or upcoming reservations can be selected in total." } })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            
            await servicesRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> DeleteReservation(Guid reservationId)
        {
            var reservation = await reservationDbContext.ReservationDates.FindAsync(reservationId);
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
            var isStudent = await userManager.IsInRoleAsync(user, UserRole.Student.ToString());

            if (reservation is null || (reservation.UserId != user.Id && isStudent))
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

        public async Task<ObjectResult> GetDormitoryReservations(Guid dormitoryId)
        {
            var reservations = await reservationDbContext.ReservationDates
                .Where(x => x.Service.DormitoryId == dormitoryId)
                .OrderBy(x => x.BeginTime)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    BeginTime = x.BeginTime.GetUserFriendlyDateTime(),
                    EndTime = x.EndTime.GetUserFriendlyDateTime(),
                    ServiceType = x.Service.Type.GetUserFriendlyServiceType(),
                    ServiceName = x.Service.Name,
                    UserName = $"{x.User.Name} {x.User.Surname}",
                    Dormitory = x.Service.Dormitory.Name,
                })
                .ToListAsync();

            return new ObjectResult(reservations)
            {
                StatusCode = (int?) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> GetUserReservations(Guid userId)
        {
            var reservations = await reservationDbContext.ReservationDates
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.BeginTime)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    BeginTime = x.BeginTime.GetUserFriendlyDateTime(),
                    EndTime = x.EndTime.GetUserFriendlyDateTime(),
                    ServiceType = x.Service.Type.GetUserFriendlyServiceType(),
                    ServiceName = x.Service.Name,
                    UserName = $"{x.User.Name} {x.User.Surname}",
                    Dormitory = x.Service.Dormitory.Name,
                })
                .ToListAsync();

            return new ObjectResult(reservations)
            {
                StatusCode = (int?) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> GetServiceReservations(Guid serviceId)
        {
            var reservations = await reservationDbContext.ReservationDates
                .Where(x => x.ServiceId == serviceId)
                .OrderBy(x => x.BeginTime)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    BeginTime = x.BeginTime.GetUserFriendlyDateTime(),
                    EndTime = x.EndTime.GetUserFriendlyDateTime(),
                    ServiceType = x.Service.Type.GetUserFriendlyServiceType(),
                    ServiceName = x.Service.Name,
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