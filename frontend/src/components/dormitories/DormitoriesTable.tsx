import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { DormitoryType } from './types/DormitoryType';

const DormitoriesTable = (props: { data: DormitoryType[] }): JSX.Element => {
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 800 }}>
        <Table sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
              <TableCell>Address</TableCell>
              <TableCell>City</TableCell>
              <TableCell>Manager email</TableCell>
              <TableCell>Manager phone number</TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((row: DormitoryType, index: number) => {
                return (
                  <TableRow key={index}>
                    <TableCell>{row.id}</TableCell>
                    <TableCell>{row.address}</TableCell>
                    <TableCell>{row.city}</TableCell>
                    <TableCell>{row.managerEmail}</TableCell>
                    <TableCell>{row.managerPhoneNumber}</TableCell>
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

export default DormitoriesTable;
