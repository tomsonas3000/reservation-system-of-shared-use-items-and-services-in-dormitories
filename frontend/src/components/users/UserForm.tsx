import { useEffect, useState } from 'react';
import { LookupType } from '../base/types/LookupType';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { Box, Button, MenuItem, TextField, Typography } from '@mui/material';
import { UsersService } from '../../services/usersService';
import { useNavigate } from 'react-router-dom';
import { handleErrors } from '../../utils/functions';

const UserForm = () => {
  const [roles, setRoles] = useState<LookupType[]>([]);
  const navigate = useNavigate();

  const validationSchema = yup.object().shape({
    name: yup.string().required('Name is required.').max(200),
    surname: yup.string().required('Surname is required.').max(200),
    emailAddress: yup.string().required('Email address is required.').max(200),
    telephoneNumber: yup
      .string()
      .required('Telephone number is required.')
      .max(200),
    password: yup.string().required('Password is required.').min(8).max(200),
    role: yup.string().required('Role is required.'),
  });

  const formik = useFormik({
    initialValues: {
      name: '',
      surname: '',
      emailAddress: '',
      telephoneNumber: '',
      password: '',
      role: '',
    },
    validationSchema: validationSchema,
    onSubmit: () => {
      UsersService.createUser({
        name: formik.values.name,
        surname: formik.values.surname,
        emailAddress: formik.values.emailAddress,
        telephoneNumber: formik.values.telephoneNumber,
        password: formik.values.password,
        role: formik.values.role,
      })
        .then(() => navigate('/users'))
        .catch((err) => {
          handleErrors(formik, err);
        });
    },
  });

  useEffect(() => {
    UsersService.getRolesLookupList().then((res) => {
      setRoles(res.data);
    });
  }, []);

  return (
    <Box
      sx={{
        marginTop: 8,
        flexDirection: 'column',
        alignItems: 'center',
        display: 'flex',
      }}>
      <Typography component="h1" variant="h5">
        Create user
      </Typography>
      <Box
        component="form"
        onSubmit={formik.handleSubmit}
        sx={{
          m: 4,
          flexDirection: 'column',
          alignItems: 'center',
          display: 'flex',
          minWidth: 600,
        }}>
        <TextField
          id="name"
          label="User name"
          value={formik.values.name}
          onChange={formik.handleChange}
          error={formik.touched.name && !!formik.errors.name}
          helperText={formik.touched.name && formik.errors.name}
          margin="normal"
          fullWidth
        />
        <TextField
          id="surname"
          label="User surname"
          value={formik.values.surname}
          onChange={formik.handleChange}
          error={formik.touched.surname && !!formik.errors.surname}
          helperText={formik.touched.surname && formik.errors.surname}
          margin="normal"
          fullWidth
        />
        <TextField
          id="emailAddress"
          label="User email address"
          value={formik.values.emailAddress}
          onChange={formik.handleChange}
          error={formik.touched.emailAddress && !!formik.errors.emailAddress}
          helperText={formik.touched.emailAddress && formik.errors.emailAddress}
          margin="normal"
          fullWidth
        />
        <TextField
          id="telephoneNumber"
          label="User telephone number"
          fullWidth
          margin="normal"
          value={formik.values.telephoneNumber}
          onChange={formik.handleChange}
          error={
            formik.touched.telephoneNumber && !!formik.errors.telephoneNumber
          }
          helperText={
            formik.touched.telephoneNumber && formik.errors.telephoneNumber
          }
        />
        <TextField
          id="password"
          label="User password"
          fullWidth
          margin="normal"
          value={formik.values.password}
          onChange={formik.handleChange}
          error={formik.touched.password && !!formik.errors.password}
          helperText={formik.touched.password && formik.errors.password}
          type="password"
        />
        <TextField
          id="role"
          label="User role"
          fullWidth
          margin="normal"
          value={formik.values.role}
          onChange={(e) => {
            formik.setFieldValue('role', e.target.value);
          }}
          error={formik.touched.role && !!formik.errors.role}
          helperText={formik.touched.role && formik.errors.role}
          select>
          {roles.map((role: LookupType) => {
            return (
              <MenuItem key={role.value} value={role.value}>
                {role.name}
              </MenuItem>
            );
          })}
        </TextField>
        <Button
          type="submit"
          fullWidth
          variant="contained"
          sx={{ margin: '2rem' }}>
          Submit
        </Button>
      </Box>
    </Box>
  );
};

export default UserForm;
