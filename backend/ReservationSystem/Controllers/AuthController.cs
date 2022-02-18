using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Services;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task Login([FromBody] LoginDto loginDto)
        {
            
        }
        
        [HttpPost]
        [Route("create")]
        public Task<ObjectResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            return authService.CreateUser(createUserDto);
        }
    }
}