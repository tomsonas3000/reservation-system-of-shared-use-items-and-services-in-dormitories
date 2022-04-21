import { Avatar, Box, Button, TextField, Typography } from '@mui/material';
import { LockOutlined } from '@mui/icons-material';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { AuthService } from '../../../services/authService';
import { useDispatch, useSelector } from 'react-redux';
import { logIn } from '../../../redux/slices/authSlice';
import { useNavigate } from 'react-router-dom';
import { RootState } from '../../../redux/store';
import { useEffect } from 'react';
import Role from '../../../utils/enums/role';

const validationSchema = yup.object().shape({
  email: yup
    .string()
    .email('Enter a valid email address.')
    .required('Email address is required.'),
  password: yup.string().required('Passowrd is required'),
});

const SignIn = () => {
  const authState = useSelector((state: RootState) => state.auth);

  const disptach = useDispatch();
  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: validationSchema,
    onSubmit: () => {
      handleLogin();
    },
  });

  const handleLogin = () => {
    AuthService.login({
      email: formik.values.email,
      password: formik.values.password,
    })
      .then((res) => {
        disptach(
          logIn({
            role: res.data.role,
            token: res.data.token,
            dormitoryId: res.data?.dormitoryId,
          })
        );
      })
      .catch(() => {
        formik.setErrors({
          password: 'The username or password was invalid.',
        });
      });
  };

  useEffect(() => {
    if (authState.isLoggedIn) {
      if (authState.role === Role.Admin) navigate('/dormitories');
      else navigate('/reservations-calendar');
    }
  }, []);

  return (
    <Box
      sx={{
        marginTop: 8,
        flexDirection: 'column',
        alignItems: 'center',
        display: 'flex',
      }}>
      <Avatar sx={{ m: 1, width: 64, height: 64 }}>
        <LockOutlined />
      </Avatar>
      <Typography variant="h5">Dormitories Reservation System</Typography>
      <Box
        component="form"
        onSubmit={formik.handleSubmit}
        sx={{ m: 1, maxWidth: '30rem' }}>
        <TextField
          id="email"
          name="email"
          label="Email address"
          value={formik.values.email}
          onChange={formik.handleChange}
          error={formik.touched.email && !!formik.errors.email}
          helperText={formik.touched.email && formik.errors.email}
          margin="normal"
          fullWidth
          required
        />
        <TextField
          id="password"
          name="password"
          label="Password"
          value={formik.values.password}
          onChange={formik.handleChange}
          error={formik.touched.password && !!formik.errors.password}
          helperText={formik.touched.password && formik.errors.password}
          margin="normal"
          fullWidth
          required
          type="password"
        />
        <Button type="submit" fullWidth variant="contained">
          Sign In
        </Button>
      </Box>
    </Box>
  );
};

export default SignIn;
