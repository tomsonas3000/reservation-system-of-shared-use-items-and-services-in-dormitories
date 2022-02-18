using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.Repositories
{
    public class UsersRepository
    {
        private readonly UserManager<User> userManager;

        public UsersRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ObjectResult> Add(User user, string? password, string? role)
        {
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new ObjectResult(new Dictionary<string, string>
                    { { "password", "The provided password is invalid" } })
                {
                    StatusCode = 400
                };
            }

            await userManager.AddToRoleAsync(user, role);
            
            return new ObjectResult(user.Id)
            {
                StatusCode = 200
            };
        }
    }
}