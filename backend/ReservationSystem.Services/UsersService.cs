using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class UsersService
    {
        private readonly ReservationDbContext reservationDbContext;

        public UsersService(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public async Task<ObjectResult> GetUsers()
        {
            var users = await 
                (from user in reservationDbContext.Users
                join userRole in reservationDbContext.UserRoles on user.Id equals userRole.UserId
                join role in reservationDbContext.Roles on userRole.RoleId equals role.Id
                select new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    TelehponeNumber = user.PhoneNumber,
                    EmailAddress = user.Email,
                    Role = role.Name
                }).ToListAsync();
            
            return new ObjectResult(users)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await reservationDbContext.Users.Include(x => x.Reservations).FirstOrDefaultAsync();
        }

        public async Task<ObjectResult> GetManagersLookupList()
        {
            var managersLookupList = await (from user in reservationDbContext.Users
                join userRole in reservationDbContext.UserRoles on user.Id equals userRole.UserId
                join role in reservationDbContext.Roles on userRole.RoleId equals role.Id
                where role.Name == UserRole.Manager.ToString()
                select new LookupDto
                {
                    Name = user.Name,
                    Value = user.Id.ToString(),
                }).ToListAsync();

            return new ObjectResult(managersLookupList)
            {
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public ObjectResult GetRolesLookupList()
        {
            var rolesLookupList = Enum.GetValues<UserRole>().Where(x => x != UserRole.Admin).Select(x => new LookupDto
            {
                Name = x.ToString(),
                Value = x.ToString(),
            }).ToList();

            return new ObjectResult(rolesLookupList)
            {
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}