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

  return (
    <Grid container m={4}>
      <Grid item>
        <Button size="large" variant="contained">
          Add new reservation
        </Button>
        <ReservationsTable data={reservations} />
      </Grid>
    </Grid>
  );
};

export default Reservations;
