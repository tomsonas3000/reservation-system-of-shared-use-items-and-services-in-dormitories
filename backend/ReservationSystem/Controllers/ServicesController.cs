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
        [Route("/service-types")]
        [Authorize(Roles = "Admin")]
        public ObjectResult GetServiceTypes()
        {
            return servicesService.GetServiceTypes();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> CreateService([FromBody] CreateServiceDto request)
        {
            return servicesService.CreateService(request);
        }
    }
}