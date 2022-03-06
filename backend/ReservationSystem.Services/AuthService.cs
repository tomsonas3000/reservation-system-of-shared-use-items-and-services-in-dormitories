using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Shared.Contracts.Dtos;
using ReservationSystem.Shared.ValueObjects;

namespace ReservationSystem.Services
{
    public class AuthService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UsersRepository usersRepository;
        private readonly JwtService jwtService;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            UsersRepository usersRepository,
            JwtService jwtService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.usersRepository = usersRepository;
            this.jwtService = jwtService;
        }

        public async Task<ObjectResult> CreateUser(CreateUserDto createUserDto)
        {
            var user = await userManager.FindByEmailAsync(createUserDto.Email);

            if (user is not null)
            {
                return new ObjectResult(new Dictionary<string, string>
                    {{"Email", "The user with this email already exists."}})
                {
                    StatusCode = (int?) HttpStatusCode.BadRequest
                };
            }

            var createResult = User.Create(createUserDto.Name, createUserDto.Surname, createUserDto.Email,
                createUserDto.PhoneNumber);

            if (!createResult.IsSuccess)
            {
                return new ObjectResult(createResult.Errors)
                {
                    StatusCode = (int?) HttpStatusCode.BadRequest
                };
            }

            var roleResult = RequiredEnum<UserRole>.Create(createUserDto.Role, "Role");

            if (!roleResult.IsSuccess)
            {
                return new ObjectResult(roleResult.Errors)
                {
                    StatusCode = (int?) HttpStatusCode.BadRequest
                };
            }

            var roleExists = await roleManager.RoleExistsAsync(roleResult.Value?.Value.ToString());

            if (!roleExists)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int?) HttpStatusCode.NotFound
                };
            }

            return await usersRepository.Add(createResult.Value, createUserDto.Password,
                roleResult.Value?.Value.ToString());
        }

        public async Task<ObjectResult> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int?) HttpStatusCode.Unauthorized
                };
            }

            if (await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email, user.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role, userRoles.FirstOrDefault() ?? string.Empty)
                };

                var token = jwtService.GetToken(authClaims);

                return new ObjectResult(new LoginResponseDto
                    {Token = new JwtSecurityTokenHandler().WriteToken(token), Role = userRoles.First()})
                {
                    StatusCode = (int?) HttpStatusCode.OK
                };
            }

            return new ObjectResult(null)
            {
                StatusCode = (int?) HttpStatusCode.Unauthorized
            };
        }
    }
}