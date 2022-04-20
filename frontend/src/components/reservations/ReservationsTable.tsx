import {
  Box,
  Button,
  Modal,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from '@mui/material';
import { ReservationType } from './types/ReservationType';
import DeleteIcon from '@mui/icons-material/Delete';
import { useState } from 'react';

const ReservationsTable = (props: {
  data: ReservationType[];
  onReservationDelete: (reservationId: string) => void;
}): JSX.Element => {
  const [confirmationModalOpen, setConfirmationModalOpen] = useState(false);
  const [selectedReservationId, setSelectedReservationId] = useState('');

  const handleDeleteButtonClick = (reservationId: string) => {
    setSelectedReservationId(reservationId);
    setConfirmationModalOpen(true);
  };

  const handleDeleteConfirmationClick = () => {
    props.onReservationDelete(selectedReservationId);
    setConfirmationModalOpen(false);
  };

  const modalContentStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    p: 2,
  };
  {
    return (
      <>
        <Modal
          open={confirmationModalOpen}
          onClose={() => setConfirmationModalOpen(false)}>
          <Box sx={modalContentStyle}>
            <Typography variant="h6">
              Are you sure you want to delete this reservation?
            </Typography>
            <Box sx={{ justifyContent: 'flex-end', display: 'flex', m: 2 }}>
              <Button
                sx={{ mx: 1 }}
                variant="contained"
                color="error"
                onClick={handleDeleteConfirmationClick}>
                Yes
              </Button>
              <Button
                sx={{ mx: 1 }}
                variant="contained"
                color="success"
                onClick={() => setConfirmationModalOpen(false)}>
                No
              </Button>
            </Box>
          </Box>
        </Modal>
        {props.data.length > 0 ? (
          <TableContainer
            sx={{ justifyContent: 'center', display: 'flex', minWidth: 1300 }}>
            <Table sx={{ my: 4 }}>
              <TableHead>
                <TableRow sx={{ backgroundColor: '#81d4fa' }}>
                  <TableCell>Reservation start</TableCell>
                  <TableCell>Reservation end</TableCell>
                  <TableCell>Service type</TableCell>
                  <TableCell>Service name</TableCell>
                  <TableCell>{`Student's name`}</TableCell>
                  <TableCell>Dormitory name</TableCell>
                  <TableCell />
                </TableRow>
              </TableHead>
              <TableBody>
                {props.data.map(
                  (reservation: ReservationType, index: number) => {
                    return (
                      <TableRow key={index} sx={{ backgroundColor: '#e1f5fe' }}>
                        <TableCell>{reservation.beginTime}</TableCell>
                        <TableCell>{reservation.endTime}</TableCell>
                        <TableCell>{reservation.serviceType}</TableCell>
                        <TableCell>{reservation.serviceName}</TableCell>
                        <TableCell>{reservation.userName}</TableCell>
                        <TableCell>{reservation.dormitory}</TableCell>
                        <TableCell>
                          <Box
                            sx={{
                              display: 'flex',
                              justifyContent: 'flex-end',
                            }}>
                            <Button
                              size="large"
                              variant="contained"
                              color="error"
                              onClick={() =>
                                handleDeleteButtonClick(reservation.id)
                              }>
                              <DeleteIcon />
                            </Button>
                          </Box>
                        </TableCell>
                      </TableRow>
                    );
                  }
                )}
              </TableBody>
            </Table>
          </TableContainer>
        ) : (
          <Box sx={{ display: 'flex', justifyContent: 'center' }}>
            <Typography variant="h4">No data available to display</Typography>
          </Box>
        )}
      </>
    );
  }
};

export default ReservationsTable;
