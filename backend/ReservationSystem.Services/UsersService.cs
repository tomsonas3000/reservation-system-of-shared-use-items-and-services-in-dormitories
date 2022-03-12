using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
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
                    Role = role.Name,
                    Dormitory = user.Dormitory.Name ?? "-",
                }).ToListAsync();
            
            return new ObjectResult(users)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }
    }
}