import {
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
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 1600 }}>
        <Table sx={{ my: 4 }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#81d4fa' }}>
              <TableCell>Name</TableCell>
              <TableCell>Type</TableCell>
              <TableCell>Maximum time of use</TableCell>
              <TableCell>Dormitory</TableCell>
              <TableCell>Room</TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((service: ServiceType, index: number) => {
                return (
                  <TableRow
                    key={index}
                    onClick={() => navigate(`/services/${service.id}`)}
                    hover
                    sx={{ backgroundColor: '#e1f5fe' }}>
                    <TableCell>{service.name}</TableCell>
                    <TableCell>{service.type}</TableCell>
                    <TableCell>{service.maxTimeOfUse}</TableCell>
                    <TableCell>{service.dormitory}</TableCell>
                    <TableCell>{service.room}</TableCell>
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
