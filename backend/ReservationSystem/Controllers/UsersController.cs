using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetUsers()
        {
            return usersService.GetUsers();
        }

        [HttpGet]
        [Route("/managers-lookup")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetManagersLookup()
        {
            return usersService.GetManagersLookupList();
        }

        [HttpGet]
        [Route("/roles-lookup")]
        [Authorize(Roles = "Admin")]
        public ObjectResult GetRolesLookup()
        {
            return usersService.GetRolesLookupList();
        }

        [HttpPost]
        [Route("{userId}/ban")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> BanUserFromReserving([FromRoute] Guid userId)
        {
            return usersService.BanFromReserving(userId);
        }

        [HttpPost]
        [Route("{userId}/remove-ban")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> RemoveReservationBan([FromRoute] Guid userId)
        {
            return usersService.RemoveReservationBan(userId);
        }

        [HttpPost]
        [Route("send-email")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> SendEmail([FromBody] EmailDto request)
        {
            return usersService.SendEmail(request);
        }
    }
}