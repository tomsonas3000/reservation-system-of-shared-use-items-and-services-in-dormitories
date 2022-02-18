using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task Login([FromBody] LoginDto loginDto)
        {
            
        }
        
        [HttpPost]
        [Route("create")]
        public async Task CreateUser([FromBody] CreateUserDto createUserDto)
        {
            User user = new User
            {
                Email = createUserDto.Email,
                PhoneNumber = createUserDto.PhoneNumber,
                Name = createUserDto.Name,
                Surname = createUserDto.Surname,
            };

            await userManager.CreateAsync(user, createUserDto.Password);

            await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
        }
    }
}