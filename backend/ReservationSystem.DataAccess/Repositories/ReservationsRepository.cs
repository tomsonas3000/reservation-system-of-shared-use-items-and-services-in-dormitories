using System.Threading.Tasks;
using ReservationSystem.DataAccess.Entities;

namespace ReservationSystem.DataAccess.Repositories
{
    public class ReservationsRepository
    {
        private readonly ReservationDbContext reservationDbContext;

        public ReservationsRepository(ReservationDbContext reservationDbContext)
        {
            this.reservationDbContext = reservationDbContext;
        }
        
        public void DeleteReservation(Reservation reservation)
        {
            reservationDbContext.ReservationDates.Remove(reservation);
        }

        public async Task SaveChanges()
        {
            await reservationDbContext.SaveChangesAsync();
        }
    }
}