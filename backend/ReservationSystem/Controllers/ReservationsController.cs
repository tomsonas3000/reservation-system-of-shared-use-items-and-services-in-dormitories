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
        [Authorize(Roles = "Student, Manager, Guard")]
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

        [HttpDelete]
        [Route("{reservationId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> DeleteReservation([FromRoute] Guid reservationId)
        {
            return reservationsService.DeleteReservation(reservationId);
        }

        [HttpGet]
        [Route("/dormitory-reservations/{dormitoryId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetDormitoryReservations([FromRoute] Guid dormitoryId)
        {
            return reservationsService.GetDormitoryReservations(dormitoryId);
        }

        [HttpGet]
        [Route("/user-reservations/{userId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetUserReservations([FromRoute] Guid userId)
        {
            return reservationsService.GetUserReservations(userId);
        }

        [HttpGet]
        [Route("/service-reservations/{serviceId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetServiceReservations([FromRoute] Guid serviceId)
        {
            return reservationsService.GetServiceReservations(serviceId);
        }
    }
}