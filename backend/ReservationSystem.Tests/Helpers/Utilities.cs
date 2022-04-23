using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Tests.Constants;

namespace ReservationSystem.Tests.Helpers
{
    public class Utilities
    {
        public static void InitializeDbForTests(ReservationDbContext dbContext)
        {
            dbContext.Dormitories.AddRange(GetDormitories());
            var user = User.Create("name", "surname", "test@email.com", "+37061111111").Value;
            user.ConcurrencyStamp = DateTime.Now.Ticks.ToString();
            user.Email = AuthConstants.defaultUserEmail;
            user.EmailConfirmed = true;
            user.Id = AuthConstants.defaultUserId;
        
            user.NormalizedEmail = user.Email;
            user.NormalizedUserName = user.Email;
            user.PasswordHash = Guid.NewGuid().ToString();
            user.UserName = user.Email;
            
            var userRole = new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("8D411ABA-7E5E-4FF9-9AFF-430B89BEB461"),
                UserId = user.Id
            };
            
            dbContext.Users.Add(user);
            dbContext.UserRoles.Add(userRole);
            dbContext.SaveChanges();
        }

        public static void ReinitializeDbForTests(ReservationDbContext db)
        {
            db.Dormitories.RemoveRange(db.Dormitories);
            InitializeDbForTests(db);
        }

        public static List<Dormitory> GetDormitories()
        {
            return new List<Dormitory> { Dormitory.Create("Dorm", "City", "Address", Guid.NewGuid()).Value };
        }
    }
}