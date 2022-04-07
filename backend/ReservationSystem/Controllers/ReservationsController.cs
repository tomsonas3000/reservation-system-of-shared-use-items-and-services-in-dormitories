using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController
    {
        private readonly ReservationsService reservationsService;

        public ReservationsController(ReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetReservations()
        {
            return reservationsService.GetReservations();
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        [Route("calendar")]
        public Task<ObjectResult> GetReservationsForCalendar()
        {
            return reservationsService.GetReservationsDataForCalendar();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public Task<ObjectResult> CreateReservation([FromBody] CreateReservationDto request)
        {
            return reservationsService.CreateReservation(request);
        }
    }
}