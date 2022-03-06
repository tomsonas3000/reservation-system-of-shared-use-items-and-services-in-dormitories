import { Avatar, Box, Button, TextField, Typography } from '@mui/material';
import { LockOutlined } from '@mui/icons-material';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { AuthService } from '../../../services/authService';
import { useDispatch } from 'react-redux';
import { logIn } from '../../../redux/slices/authSlice';
import { useNavigate } from 'react-router-dom';

const validationSchema = yup.object().shape({
  email: yup
    .string()
    .email('Enter a valid email address.')
    .required('Email address is required.'),
  password: yup.string().required('Passowrd is required'),
});

const SignIn = () => {
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
        disptach(logIn({ role: res.data.role, token: res.data.token }));
        navigate('/dormitories');
      })
      .catch(() => {
        formik.setErrors({
          password: 'The username or password was invalid.',
        });
      });
  };

  return (
    <Box
      sx={{
        marginTop: 8,
        flexDirection: 'column',
        alignItems: 'center',
        display: 'flex',
      }}>
      <Avatar sx={{ m: 1 }}>
        <LockOutlined />
      </Avatar>
      <Typography component="h1" variant="h5">
        Sign In
      </Typography>
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
          Submit
        </Button>
      </Box>
      <pre>{JSON.stringify(formik.values, null, 2)}</pre>
    </Box>
  );
};

export default SignIn;
