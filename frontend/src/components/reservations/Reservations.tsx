import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { ReservationsService } from '../../services/reservationsService';
import ReservationsTable from './ReservationsTable';
import { ReservationType } from './types/ReservationType';

const Reservations = () => {
  const [reservations, setReservations] = useState<ReservationType[]>([]);

  useEffect(() => {
    ReservationsService.getReservations().then((res) => {
      setReservations(res.data);
    });
  }, []);

  const handleReservationDelete = (reservationId: string) => {
    ReservationsService.deleteReservation(reservationId).then(() => {
      ReservationsService.getReservations().then((res) => {
        setReservations(res.data);
      });
    });
  };

  return (
    <Grid container m={4}>
      <Grid item>
        <ReservationsTable
          data={reservations}
          onReservationDelete={handleReservationDelete}
        />
      </Grid>
    </Grid>
  );
};

export default Reservations;
