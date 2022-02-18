using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.Services
{
    public class AuthService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UsersRepository usersRepository;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, UsersRepository usersRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.usersRepository = usersRepository;
        }

        public async Task<ObjectResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = await userManager.FindByEmailAsync(createUserDto.Email);

            if (user is not null)
            {
                return new ObjectResult(new Dictionary<string, string>{{"Email", "The user with this email already exists."}})
                {
                    StatusCode = 400
                };
            }

            var createResult = User.Create(createUserDto.Name, createUserDto.Surname, createUserDto.Email,
                createUserDto.PhoneNumber);

            if (!createResult.IsSuccess)
            {
                return new ObjectResult(createResult.Errors)
                {
                    StatusCode = 400
                };
            }

            var roleResult = RequiredEnum<UserRole>.Create(createUserDto.Role, "Role");

            if (!roleResult.IsSuccess)
            {
                return new ObjectResult(roleResult.Errors)
                {
                    StatusCode = 400
                };
            }

            var roleExists = await roleManager.RoleExistsAsync(roleResult.Value?.Value.ToString());

            if (!roleExists)
            {
                return new ObjectResult(null)
                {
                    StatusCode = 404
                };
            }
            
            return await usersRepository.Add(createResult.Value, createUserDto.Password, roleResult.Value?.Value.ToString());
        }
    }
}