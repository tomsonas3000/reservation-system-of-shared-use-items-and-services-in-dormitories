import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { UserType } from './types/UserType';

const UsersTable = (props: { data: UserType[] }): JSX.Element => {
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 800 }}>
        <Table sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Surname</TableCell>
              <TableCell>Telephone number</TableCell>
              <TableCell>Email address</TableCell>
              <TableCell>Role</TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data.map((user: UserType, index: number) => {
                return (
                  <TableRow key={index}>
                    <TableCell>{user.id}</TableCell>
                    <TableCell>{user.name}</TableCell>
                    <TableCell>{user.surname}</TableCell>
                    <TableCell>{user.telephoneNumber}</TableCell>
                    <TableCell>{user.emailAddress}</TableCell>
                    <TableCell>{user.role}</TableCell>
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

export default UsersTable;
