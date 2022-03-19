using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;
using ReservationSystem.Shared.Contracts.Dtos;

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

        [HttpGet]
        [Route("/dormitories-lookup")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetDormitoriesLookupList()
        {
            return dormitoriesService.GetDormitoriesLookupList();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> CreateDormitory([FromBody] CreateDormitoryDto request)
        {
            return dormitoriesService.CreateDormitory(request);
        }
    }
}