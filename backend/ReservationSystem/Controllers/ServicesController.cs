using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;

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
    }
}