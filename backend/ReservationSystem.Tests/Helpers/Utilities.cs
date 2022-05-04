using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Tests.Constants;

namespace ReservationSystem.Tests.Helpers
{
    public class Utilities
    {
        public static void InitializeDbForTests(ReservationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            var dormitory = Dormitory.Create("Dorm", "City", "Address", Guid.NewGuid()).Value;
            dormitory.Id = AuthConstants.DefaultDormitoryId;
            dbContext.Dormitories.Add(dormitory);

            var admin = User.Create("admin", "admin", AuthConstants.DefaultAdminUserEmail, "+37061111111").Value;
            admin.ConcurrencyStamp = DateTime.Now.Ticks.ToString();
            admin.Email = AuthConstants.DefaultAdminUserEmail;
            admin.EmailConfirmed = true;
            admin.Id = AuthConstants.DefaultAdminUserId;

            admin.NormalizedEmail = admin.Email.ToUpper();
            admin.NormalizedUserName = admin.Email.ToUpper();
            admin.PasswordHash = Guid.NewGuid().ToString();
            admin.UserName = admin.Email;

            var adminUserRole = new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("8D411ABA-7E5E-4FF9-9AFF-430B89BEB461"),
                UserId = admin.Id
            };

            var manager = User.Create("manager", "manager", AuthConstants.DefaultManagerUserEmail, "+37061111111").Value;
            manager.ConcurrencyStamp = DateTime.Now.Ticks.ToString();
            manager.Email = AuthConstants.DefaultManagerUserEmail;
            manager.EmailConfirmed = true;
            manager.Id = AuthConstants.DefaultManagerUserId;

            manager.NormalizedEmail = manager.Email.ToUpper();
            manager.NormalizedUserName = manager.Email.ToUpper();
            manager.PasswordHash = Guid.NewGuid().ToString();
            manager.UserName = manager.Email;

            var managerUserRole = new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("F46A30FC-9425-4275-A245-D389387D84A8"),
                UserId = manager.Id
            };

            var student = User.Create("student", "student", AuthConstants.DefaultStudentUserEmail, "+37061111111").Value;
            student.ConcurrencyStamp = DateTime.Now.Ticks.ToString();
            student.Email = AuthConstants.DefaultStudentUserEmail;
            student.EmailConfirmed = true;
            student.Id = AuthConstants.DefaultStudentUserId;

            student.NormalizedEmail = student.Email.ToUpper();
            student.NormalizedUserName = student.Email.ToUpper();
            student.PasswordHash = Guid.NewGuid().ToString();
            student.UserName = student.Email;

            var studentUserRole = new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("89494DE6-9587-4EE5-B3F1-AC171C52911B"),
                UserId = student.Id
            };

            dbContext.Roles.AddRange(new List<IdentityRole<Guid>>
            {
                new()
                {
                    Id = Guid.Parse("8D411ABA-7E5E-4FF9-9AFF-430B89BEB461"),
                    Name = UserRole.Admin.ToString(),
                    ConcurrencyStamp = "1",
                    NormalizedName = UserRole.Admin.ToString().ToUpper()
                },
                new()
                {
                    Id = Guid.Parse("F46A30FC-9425-4275-A245-D389387D84A8"),
                    Name = UserRole.Manager.ToString(),
                    ConcurrencyStamp = "3",
                    NormalizedName = UserRole.Manager.ToString().ToUpper()
                },
                new()
                {
                    Id = Guid.Parse("89494DE6-9587-4EE5-B3F1-AC171C52911B"),
                    Name = UserRole.Student.ToString(),
                    ConcurrencyStamp = "4",
                    NormalizedName = UserRole.Student.ToString().ToUpper()
                }
            });
            dbContext.Users.Add(admin);
            dbContext.Users.Add(manager);
            dbContext.Users.Add(student);
            dbContext.UserRoles.Add(adminUserRole);
            dbContext.UserRoles.Add(managerUserRole);
            dbContext.UserRoles.Add(studentUserRole);

            var service = Service.Create("service", ServiceType.Basketball.ToString(), 15, Guid.NewGuid(), AuthConstants.DefaultDormitoryId).Value;
            service.Id = AuthConstants.DefaultServiceId;
            var reservation = Reservation.Create(service, student, DateTime.Now.AddMinutes(5).ToString(), DateTime.Now.AddMinutes(15).ToString()).Value;
            reservation.Id = AuthConstants.DefaultReservationId;
            dbContext.SaveChanges();
            dormitory.AddRooms(new List<string> { AuthConstants.DefaultRoomName });
            dormitory.Rooms.First().Id = AuthConstants.DefaultRoomId;
            dbContext.Services.Add(service);
            dbContext.ReservationDates.Add(reservation);
            dbContext.SaveChanges();
        }
    }
}