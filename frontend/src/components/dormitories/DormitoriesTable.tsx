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
              <TableCell>Name</TableCell>
              <TableCell>Address</TableCell>
              <TableCell>City</TableCell>
              <TableCell>Manager email</TableCell>
              <TableCell>Manager phone number</TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((dormitory: DormitoryType, index: number) => {
                return (
                  <TableRow key={index}>
                    <TableCell>{dormitory.id}</TableCell>
                    <TableCell>{dormitory.name}</TableCell>
                    <TableCell>{dormitory.address}</TableCell>
                    <TableCell>{dormitory.city}</TableCell>
                    <TableCell>{dormitory.managerEmail}</TableCell>
                    <TableCell>{dormitory.managerPhoneNumber}</TableCell>
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
