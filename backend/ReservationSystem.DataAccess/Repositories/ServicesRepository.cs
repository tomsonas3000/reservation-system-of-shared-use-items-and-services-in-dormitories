using System.Threading.Tasks;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.Repositories
{
    public class ServicesRepository
    {
        private readonly ReservationDbContext reservationDbContext;

        public ServicesRepository(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public void AddService(Service service)
        {
            reservationDbContext.Services.Add(service);
        }

        public async Task SaveChanges()
        {
            await reservationDbContext.SaveChangesAsync();
        }
    }
}