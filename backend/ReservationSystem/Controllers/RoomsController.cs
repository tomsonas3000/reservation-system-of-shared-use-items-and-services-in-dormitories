using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController
    {
        private readonly RoomsService roomsService;

        public RoomsController(RoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetRooms()
        {
            return roomsService.GetRooms();
        }
    }
}