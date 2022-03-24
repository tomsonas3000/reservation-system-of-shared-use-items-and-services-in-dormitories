using System;
using System.Collections.Generic;
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
        [Route("{dormitoryId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetDormitory([FromRoute] Guid dormitoryId)
        {
            return dormitoriesService.GetDormitory(dormitoryId);
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
        public Task<ObjectResult> CreateDormitory([FromBody] CreateUpdateDormitoryDto request)
        {
            return dormitoriesService.CreateDormitory(request);
        }

        [HttpPut]
        [Route("{dormitoryId}")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> UpdateDormitory([FromRoute] Guid dormitoryId, [FromBody] CreateUpdateDormitoryDto request)
        {
            return dormitoriesService.UpdateDormitory(dormitoryId, request);
        }

        [HttpPost]
        [Route("{dormitoryId}/update-students")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> AddStudentsToDormitory([FromRoute] Guid dormitoryId, [FromBody] List<Guid> studentsIds)
        {
            return dormitoriesService.UpdateDormitoryStudents(dormitoryId, studentsIds);
        }

        [HttpGet]
        [Route("{dormitoryId}/students")]
        [Authorize(Roles = "Admin")]
        public Task<ObjectResult> GetStudents([FromRoute] Guid dormitoryId)
        {
            return dormitoriesService.GetDormitoryStudents(dormitoryId);
        }
    }
}