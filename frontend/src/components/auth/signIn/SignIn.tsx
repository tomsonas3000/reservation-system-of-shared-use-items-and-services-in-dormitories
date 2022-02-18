import { Avatar, Box, Button, TextField, Typography } from '@mui/material';
import { LockOutlined } from '@mui/icons-material';
import * as yup from 'yup';
import { useFormik } from 'formik';

const validationSchema = yup.object().shape({
  email: yup
    .string()
    .email('Enter a valid email address.')
    .required('Email address is required.'),
  password: yup
    .string()
    .required('Passowrd is required')
    .matches(
      /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/,
      'Password must be at least 8 characters long, have at least one letter, and one number.'
    ),
});

const SignIn = () => {
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      alert(JSON.stringify(values, null, 2));
    },
  });

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
      <Box component="form" onSubmit={formik.handleSubmit} sx={{ m: 1 }}>
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
