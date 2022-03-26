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
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 800 }}>
        <Table sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Type</TableCell>
              <TableCell>Maximum time of use</TableCell>
              <TableCell>Maximum amount of users</TableCell>
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
                    hover>
                    <TableCell>{service.name}</TableCell>
                    <TableCell>{service.type}</TableCell>
                    <TableCell>{service.maxTimeOfUse}</TableCell>
                    <TableCell>{service.maxAmountOfUsers}</TableCell>
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
