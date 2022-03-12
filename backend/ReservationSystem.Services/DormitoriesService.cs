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
                .Select(x => new DormitoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    ManagerEmail = x.Manager.Email,
                    ManagerPhoneNumber = x.Manager.PhoneNumber,
                })
                .ToListAsync();

            return new ObjectResult(dormitories)
            {
                StatusCode = (int?) HttpStatusCode.OK
            };
        }

        public async Task<ObjectResult> GetDormitoriesLookupList()
        {
            var dormitoriesLookupList = await reservationDbContext.Dormitories
                .Select(x => new LookupDto
                {
                    Name = x.Name,
                    Value = x.Id.ToString(),
                }).ToListAsync();

            return new ObjectResult(dormitoriesLookupList)
            {
                StatusCode = (int) HttpStatusCode.OK,
            };
        }
    }
}