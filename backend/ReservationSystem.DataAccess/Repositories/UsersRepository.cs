using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.Repositories
{
    public class UsersRepository
    {
        private readonly UserManager<User> userManager;
        private readonly ReservationDbContext reservationDbContext;

        public UsersRepository(UserManager<User> userManager, ReservationDbContext reservationDbContext)
        {
            this.userManager = userManager;
            this.reservationDbContext = reservationDbContext;
        }

        public async Task<ObjectResult> Add(User user, string? password, string? role)
        {
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new ObjectResult(new Dictionary<string, string>
                    { { "password", "The provided password is invalid" } })
                {
                    StatusCode = (int?)HttpStatusCode.BadRequest
                };
            }

            await userManager.AddToRoleAsync(user, role);
            
            return new ObjectResult(user.Id)
            {
                StatusCode = (int?)HttpStatusCode.OK
            };
        }
        
        public async Task SaveChanges()
        {
            await reservationDbContext.SaveChangesAsync();
        }
    }
}