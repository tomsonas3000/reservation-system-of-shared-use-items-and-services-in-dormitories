import {
  Button,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { DormitoriesService } from '../../services/dormitoriesService';
import { UsersService } from '../../services/usersService';
import { UserType } from '../users/types/UserType';

const ManageStudents = () => {
  const [students, setStudents] = useState<UserType[]>([]);
  const [selectedStudents, setSelectedStudents] = useState<string[]>([]);

  const { dormitoryId } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (!dormitoryId) {
      navigate('/dormitories');
    }
    UsersService.getStudents(dormitoryId).then((res) => {
      setStudents(res.data);
      setSelectedStudents(
        res.data
          .filter((x: UserType) => x.dormitoryId)
          .map((x: UserType) => x.id)
      );
    });
  }, []);

  const addRemoveStudent = (id: string) => {
    if (!selectedStudents.includes(id)) {
      setSelectedStudents([...selectedStudents, id]);
    } else {
      setSelectedStudents(selectedStudents.filter((x) => x !== id));
    }
  };

  const updateDormitoryStudents = () => {
    DormitoriesService.updateDormitoryStudents(
      selectedStudents,
      dormitoryId
    ).then(() => {
      navigate('/dormitories');
    });
  };

  return (
    <Grid container m={4} justifyContent="center">
      <Grid item xs={8}>
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
                <TableCell></TableCell>
              </TableRow>
            </TableHead>
            {
              <TableBody>
                {students.map((student: UserType, index: number) => {
                  return (
                    <TableRow key={index}>
                      <TableCell>{student.id}</TableCell>
                      <TableCell>{student.name}</TableCell>
                      <TableCell>{student.surname}</TableCell>
                      <TableCell>{student.telephoneNumber}</TableCell>
                      <TableCell>{student.email}</TableCell>
                      <TableCell>
                        <Button
                          size="large"
                          variant="contained"
                          color={
                            selectedStudents.includes(student.id as string)
                              ? 'error'
                              : 'success'
                          }
                          onClick={() =>
                            addRemoveStudent(student.id as string)
                          }>
                          {selectedStudents.includes(student.id as string)
                            ? '-'
                            : '+'}
                        </Button>
                      </TableCell>
                    </TableRow>
                  );
                })}
              </TableBody>
            }
          </Table>
        </TableContainer>
      </Grid>
      <Grid item xs={8} justifyContent="center" display="flex">
        <Button
          variant="contained"
          color="success"
          onClick={() => updateDormitoryStudents()}>
          Update dormitory students
        </Button>
      </Grid>
    </Grid>
  );
};

export default ManageStudents;
