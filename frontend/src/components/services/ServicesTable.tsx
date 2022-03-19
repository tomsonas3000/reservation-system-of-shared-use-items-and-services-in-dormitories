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
              <TableCell>Type</TableCell>
              <TableCell>Maximum time of use</TableCell>
              <TableCell>Maximum amount of users</TableCell>
              <TableCell>Dormitory</TableCell>
              <TableCell>Room</TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((dormitory: ServiceType, index: number) => {
                return (
                  <TableRow
                    key={index}
                    onClick={() => navigate(`/services/${dormitory.id}`)}
                    hover>
                    <TableCell>{dormitory.type}</TableCell>
                    <TableCell>{dormitory.maxTimeOfUse}</TableCell>
                    <TableCell>{dormitory.maxAmountOfUsers}</TableCell>
                    <TableCell>{dormitory.dormitory}</TableCell>
                    <TableCell>{dormitory.room}</TableCell>
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
