using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.Services
{
    public class UsersService
    {
        private readonly ReservationDbContext reservationDbContext;
        private readonly UserManager<User> userManager;
        private readonly UsersRepository usersRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersService(ReservationDbContext reservationDbContext, UserManager<User> userManager, UsersRepository usersRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.reservationDbContext = reservationDbContext;
            this.userManager = userManager;
            this.usersRepository = usersRepository;
            this.httpContextAccessor = httpContextAccessor;
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
                    TelephoneNumber = user.PhoneNumber,
                    EmailAddress = user.Email,
                    Role = role.Name,
                    HasMoreThanTenReservations = user.Reservations.Count(x => x.EndTime > DateTime.Now) > 5,
                    IsBannedFromReserving = user.IsBannedFromReserving,
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
            
            return new ObjectResult(users)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await reservationDbContext.Users.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ObjectResult> GetManagersLookupList()
        {
            var managersLookupList = await (from user in reservationDbContext.Users
                join userRole in reservationDbContext.UserRoles on user.Id equals userRole.UserId
                join role in reservationDbContext.Roles on userRole.RoleId equals role.Id
                where role.Name == UserRole.Manager.ToString()
                select new LookupDto
                {
                    Name = user.Email,
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

        public async Task<ObjectResult> BanFromReserving(Guid userId)
        {
            var user =  await reservationDbContext.Users.FindAsync(userId);
            
            if (user is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.NotFound,
                };
            }

            var isStudent = await userManager.IsInRoleAsync(user, UserRole.Student.ToString());

            if (!isStudent)
            {
                return new ObjectResult(new Dictionary<string, string> {{"Role", "Only students can be banned from reserving services."}})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            
            user.SetIsBannedFromReserving(true);

            await usersRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> RemoveReservationBan(Guid userId)
        {
            var user =  await reservationDbContext.Users.FindAsync(userId);
            
            if (user is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int) HttpStatusCode.NotFound,
                };
            }

            var isStudent = await userManager.IsInRoleAsync(user, UserRole.Student.ToString());

            if (!isStudent)
            {
                return new ObjectResult(new Dictionary<string, string> {{"Role", "Only students can be banned from reserving services."}})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            
            user.SetIsBannedFromReserving(false);

            await usersRepository.SaveChanges();

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }

        public async Task<ObjectResult> SendEmail(EmailDto request)
        {
            var recipient = await userManager.FindByEmailAsync(request.Recipient);

            if (recipient is null)
            {
                return new ObjectResult(new Dictionary<string, string> {{"recipient", "Recipient is invalid."}})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }

            var validationResult = new Result();

            var subjectResult = RequiredString.Create(validationResult, request.Subject, "subject", 200);
            var bodyResult = RequiredString.Create(validationResult, request.Body, "content", 2000);

            if (!validationResult.IsSuccess)
            {
                return new ObjectResult(validationResult.Errors)
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                };
            }
            
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);

            using var smtp = new SmtpClient
            {
                Host = "localhost",
                Port = 2525,
            };
            
            await smtp.SendMailAsync(
                user.Email, 
                recipient.Email, 
                subjectResult!.Value.Value,
                bodyResult!.Value.Value);

            return new ObjectResult(null)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
    }
}