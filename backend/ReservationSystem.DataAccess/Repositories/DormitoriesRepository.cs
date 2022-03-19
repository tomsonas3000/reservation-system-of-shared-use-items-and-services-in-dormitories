using System.Threading.Tasks;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.Repositories
{
    public class DormitoriesRepository
    {
        private readonly ReservationDbContext reservationDbContext;

        public DormitoriesRepository(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }

        public void AddDormitory(Dormitory dormitory)
        {
            reservationDbContext.Dormitories.Add(dormitory);
        }

        public void UpdateDormitory(Dormitory dormitory)
        {
            reservationDbContext.Dormitories.Update(dormitory);
        }

        public async Task SaveChanges()
        {
            await reservationDbContext.SaveChangesAsync();
        }
    }
}