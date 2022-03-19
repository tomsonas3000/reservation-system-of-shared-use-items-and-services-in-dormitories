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
    public class ServicesController
    {
        private readonly ServicesService servicesService;

        public ServicesController(ServicesService servicesService)
        {
            this.servicesService = servicesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ObjectResult> GetServices()
        {
            return await servicesService.GetServices();
        }

        [HttpGet]
        [Route("{serviceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ObjectResult> GetService([FromRoute] Guid serviceId)
        {
            return await servicesService.GetService(serviceId);
        }

        [HttpGet]
        [Route("/service-types")]
        [Authorize(Roles = "Admin")]
        public ObjectResult GetServiceTypes()
        {
            return servicesService.GetServiceTypes();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> CreateService([FromBody] CreateUpdateServiceDto request)
        {
            return servicesService.CreateService(request);
        }

        [HttpPut]
        [Route("{serviceId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> UpdateService([FromBody] CreateUpdateServiceDto request, [FromRoute] Guid serviceId)
        {
            return servicesService.UpdateService(serviceId, request);
        }
    }
}