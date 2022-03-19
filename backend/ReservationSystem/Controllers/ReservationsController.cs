﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;

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
    }
}