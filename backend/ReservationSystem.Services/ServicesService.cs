using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Services.Helpers;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Services
{
    public class ServicesService
    {
        private readonly ReservationDbContext reservationDbContext;
        private readonly ServicesRepository servicesRepository;

        public ServicesService(ReservationDbContext reservationDbContext, ServicesRepository servicesRepository)
        {
            this.reservationDbContext = reservationDbContext;
            this.servicesRepository = servicesRepository;
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

        public async Task<ObjectResult> GetService(Guid serviceId)
        {
            var service = await reservationDbContext.Services.Where(x => x.Id == serviceId)
                .Select(x => new ServiceDetailsDto
                {
                    Id = x.Id,
                    MaxAmountOfUsers = x.MaxAmountUsers,
                    MaxTimeOfUse = x.MaxTimeOfUse.TotalMinutes,
                    DormitoryId = x.DormitoryId,
                    RoomId = x.RoomId,
                    Type = x.Type.ToString(),
                })
                .FirstOrDefaultAsync();

            if (service is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            return new ObjectResult(service)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public ObjectResult GetServiceTypes()
        {
            var serviceTypes = EnumValues.GetValues<ServiceType>().Select(x => new LookupDto
            {
                Name = x.GetUserFriendlyServiceType(),
                Value = x.ToString(),
            }).ToList();

            return new ObjectResult(serviceTypes)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> CreateService(CreateUpdateServiceDto request)
        {
            var basicValidation = await ValidateService(request);

            if (basicValidation is not null)
            {
                return basicValidation;
            }

            var createResult = Service.Create(request.Type, request.MaxTimeOfUse, request.MaxAmountOfUsers, request.Room, request.Dormitory);

            if (!createResult.IsSuccess)
            {
                return new ObjectResult(createResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
            }

            servicesRepository.AddService(createResult.Value);
            await servicesRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }

        public async Task<ObjectResult> UpdateService(Guid serviceId, CreateUpdateServiceDto request)
        {
            var basicValidation = await ValidateService(request);

            if (basicValidation is not null)
            {
                return basicValidation;
            }

            var service = await reservationDbContext.Services.FirstOrDefaultAsync(x => x.Id == serviceId);

            if (service is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }

            var updateResult = service.Update(request.Type, request.MaxTimeOfUse, request.MaxAmountOfUsers, request.Room, request.Dormitory);

            if (!updateResult.IsSuccess)
            {
                return new ObjectResult(updateResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
            }
            
            await servicesRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }

        private async Task<ObjectResult?> ValidateService(CreateUpdateServiceDto request)
        {
            var dormitory = await reservationDbContext.Dormitories.FirstOrDefaultAsync(x => x.Id == request.Dormitory);

            if (dormitory is null)
            {
                return new ObjectResult(new Dictionary<string, string>
                    {{"Dormitory", "The provided dormitory does not exist."}})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
            }

            var room = await reservationDbContext.Rooms.FirstOrDefaultAsync(x =>
                x.Id == request.Room && x.Dormitory.Id == dormitory.Id);

            if (room is null)
            {
                return new ObjectResult(new Dictionary<string, string>
                    {{"Room", "The provided room does not exist or belongs to a different dormitory."}})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
            }

            return null;
        }
    }
}