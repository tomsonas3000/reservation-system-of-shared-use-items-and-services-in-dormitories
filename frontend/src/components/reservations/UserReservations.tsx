import { Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ReservationsService } from '../../services/reservationsService';
import ReservationsTable from './ReservationsTable';
import { ReservationType } from './types/ReservationType';

const UserReservations = () => {
  const [reservations, setReservations] = useState<ReservationType[]>([]);
  const navigate = useNavigate();
  const { userId } = useParams();

  useEffect(() => {
    if (!userId) {
      navigate('/users');
    }
    ReservationsService.getUserReservations(userId as string).then((res) => {
      setReservations(res.data);
    });
  }, []);

  const handleReservationDelete = (reservationId: string) => {
    ReservationsService.deleteReservation(reservationId).then(() => {
      ReservationsService.getUserReservations(userId as string).then((res) => {
        setReservations(res.data);
      });
    });
  };

  return (
    <Grid container m={4} sx={{ display: 'flex', justifyContent: 'center' }}>
      <Grid item>
        <ReservationsTable
          data={reservations}
          onReservationDelete={handleReservationDelete}
        />
      </Grid>
    </Grid>
  );
};

export default UserReservations;
