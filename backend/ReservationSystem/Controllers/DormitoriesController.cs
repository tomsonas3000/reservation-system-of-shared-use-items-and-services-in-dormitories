﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DormitoriesController : ControllerBase
    {
        private readonly DormitoriesService dormitoriesService;

        public DormitoriesController(DormitoriesService dormitoriesService)
        {
            this.dormitoriesService = dormitoriesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetDormitories()
        {
            return dormitoriesService.GetDormitories();
        }
    }
}