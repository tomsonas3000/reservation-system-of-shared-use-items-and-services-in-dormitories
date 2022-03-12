import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { ServiceType } from './types/ServiceType';

const ServicesTable = (props: { data: ServiceType[] }): JSX.Element => {
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 800 }}>
        <Table sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
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
                  <TableRow key={index}>
                    <TableCell>{dormitory.id}</TableCell>
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
