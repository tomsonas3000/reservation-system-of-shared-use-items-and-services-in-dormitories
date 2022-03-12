import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { ReservationType } from './types/ReservationType';

const ReservationsTable = (props: { data: ReservationType[] }): JSX.Element => {
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 800 }}>
        <Table sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              <TableCell>Reservation start</TableCell>
              <TableCell>Reservation end</TableCell>
              <TableCell>Service type</TableCell>
              <TableCell>Is the reservation finished</TableCell>
              <TableCell>{`User's name`}</TableCell>
              <TableCell>Dormitory name</TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((reservation: ReservationType, index: number) => {
                return (
                  <TableRow key={index}>
                    {console.log(reservation.isFinished)}
                    <TableCell>{reservation.beginTime}</TableCell>
                    <TableCell>{reservation.endTime}</TableCell>
                    <TableCell>{reservation.serviceType}</TableCell>
                    <TableCell>{String(reservation.isFinished)}</TableCell>
                    <TableCell>{reservation.userName}</TableCell>
                    <TableCell>{reservation.dormitory}</TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          }
        </Table>
      </TableContainer>
    );
  }
};

export default ReservationsTable;
