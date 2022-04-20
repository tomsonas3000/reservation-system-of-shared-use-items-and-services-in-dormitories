import {
  Box,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { ServiceType } from './types/ServiceType';

const ServicesTable = (props: { data: ServiceType[] }): JSX.Element => {
  const navigate = useNavigate();
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 1300 }}>
        <Table sx={{ my: 4 }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#81d4fa' }}>
              <TableCell>Name</TableCell>
              <TableCell>Type</TableCell>
              <TableCell>Maximum time of use</TableCell>
              <TableCell>Dormitory</TableCell>
              <TableCell>Room</TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((service: ServiceType, index: number) => {
                return (
                  <TableRow
                    key={index}
                    hover
                    sx={{ backgroundColor: '#e1f5fe' }}>
                    <TableCell
                      onClick={() => navigate(`/services/${service.id}`)}>
                      {service.name}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/services/${service.id}`)}>
                      {service.type}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/services/${service.id}`)}>
                      {service.maxTimeOfUse}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/services/${service.id}`)}>
                      {service.dormitory}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/services/${service.id}`)}>
                      {service.room}
                    </TableCell>
                    <TableCell
                      sx={{
                        maxWidth: 150,
                      }}>
                      <Box
                        sx={{
                          display: 'flex',
                          justifyContent: 'flex-end',
                        }}>
                        <Button
                          variant="contained"
                          onClick={() => {
                            navigate(`/service-reservations/${service.id}`);
                          }}>
                          Reservations
                        </Button>
                      </Box>
                    </TableCell>
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

export default ServicesTable;
