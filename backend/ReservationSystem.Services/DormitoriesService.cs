using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.DataAccess;
using ReservationSystem.Shared.Contracts.Dtos;

namespace ReservationSystem.Services
{
    public class DormitoriesService
    {
        private readonly ReservationDbContext reservationDbContext;

        public DormitoriesService(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public async Task<ObjectResult> GetDormitories()
        {
            var dormitories = await reservationDbContext.Dormitories
                .Include(x => x.Manager)
                .ToListAsync();

            if (dormitories.Count == 0)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int?) HttpStatusCode.NotFound,
                };
            }

            var dormitoriesDto = dormitories.Select(x => new DormitoryDto
            {
                Id = x.Id,
                Address = x.Address,
                City = x.City,
                ManagerEmail = x.Manager.Email,
                ManagerPhoneNumber = x.Manager.PhoneNumber,
            }).ToList();

            return new ObjectResult(dormitoriesDto)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }
    }
}