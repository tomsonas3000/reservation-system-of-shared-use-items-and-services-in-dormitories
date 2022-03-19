using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class DormitoriesService
    {
        private readonly ReservationDbContext reservationDbContext;
        private readonly UserManager<User> userManager;
        private readonly DormitoriesRepository dormitoriesRepository;

        public DormitoriesService(ReservationDbContext reservationDbContext, UserManager<User> userManager, DormitoriesRepository dormitoriesRepository)
        {
            this.reservationDbContext = reservationDbContext;
            this.userManager = userManager;
            this.dormitoriesRepository = dormitoriesRepository;
        }

        public async Task<ObjectResult> GetDormitories()
        {
            var dormitories = await reservationDbContext.Dormitories
                .Select(x => new DormitoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    ManagerEmail = x.Manager.Email,
                    ManagerPhoneNumber = x.Manager.PhoneNumber,
                })
                .ToListAsync();

            return new ObjectResult(dormitories)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }

        public async Task<ObjectResult> GetDormitoriesLookupList()
        {
            var dormitoriesLookupList = await reservationDbContext.Dormitories
                .Select(x => new LookupDto
                {
                    Name = x.Name,
                    Value = x.Id.ToString(),
                }).ToListAsync();

            return new ObjectResult(dormitoriesLookupList)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> CreateDormitory(CreateDormitoryDto request)
        {
            var manager = await reservationDbContext.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Manager));

            if (manager is null)
            {
                return new ObjectResult(
                    new Dictionary<string, string> { { "Manager", "The provided manager does not exist." } })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            
            var isManager = await userManager.IsInRoleAsync(manager, UserRole.Manager.ToString());

            if (!isManager)
            {
                return new ObjectResult(
                    new Dictionary<string, string> { { "Manager", "The provided manager is not a manager." } })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var createResult = Dormitory.Create(request.Name, request.City, request.Address, manager);

            if (!createResult.IsSuccess)
            {
                return new ObjectResult(createResult.Errors)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var dormitory = createResult.Value;

            var roomsAddResult = dormitory.AddRooms(request.Rooms);

            if (!roomsAddResult.IsSuccess)
            {
                return new ObjectResult(roomsAddResult.Errors)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            
            dormitoriesRepository.AddDormitory(dormitory);
            await dormitoriesRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}