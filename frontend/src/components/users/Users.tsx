import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { UserType } from './types/UserType';
import { UsersService } from '../../services/usersService';
import UsersTable from './UsersTable';

const Users = () => {
  const [users, setUsers] = useState<UserType[]>([]);

  useEffect(() => {
    UsersService.getUsers().then((res) => {
      setUsers(res.data);
    });
  }, []);

  return (
    <Grid container m={4}>
      <Grid item>
        <Button size="large" variant="contained">
          Add new user
        </Button>
        <UsersTable data={users} />
      </Grid>
    </Grid>
  );
};

export default Users;
