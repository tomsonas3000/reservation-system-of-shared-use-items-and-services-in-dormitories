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
import { DormitoryType } from './types/DormitoryType';

const DormitoriesTable = (props: { data: DormitoryType[] }): JSX.Element => {
  const navigate = useNavigate();
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 1600 }}>
        <Table sx={{ my: 4 }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#81d4fa' }}>
              <TableCell>Name</TableCell>
              <TableCell>Address</TableCell>
              <TableCell>City</TableCell>
              <TableCell>Manager email</TableCell>
              <TableCell>Manager phone number</TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.data?.map((dormitory: DormitoryType, index: number) => {
                return (
                  <TableRow
                    key={index}
                    hover
                    sx={{ backgroundColor: '#e1f5fe' }}>
                    <TableCell
                      onClick={() => navigate(`/dormitories/${dormitory.id}`)}>
                      {dormitory.name}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/dormitories/${dormitory.id}`)}>
                      {dormitory.address}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/dormitories/${dormitory.id}`)}>
                      {dormitory.city}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/dormitories/${dormitory.id}`)}>
                      {dormitory.managerEmail}
                    </TableCell>
                    <TableCell
                      onClick={() => navigate(`/dormitories/${dormitory.id}`)}>
                      {dormitory.managerPhoneNumber}
                    </TableCell>
                    <TableCell>
                      <Box
                        sx={{
                          display: 'flex',
                          justifyContent: 'flex-end',
                        }}>
                        <Button
                          variant="contained"
                          onClick={() =>
                            navigate(
                              `/dormitories/${dormitory.id}/manage-students`
                            )
                          }>
                          Manage students
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

export default DormitoriesTable;
