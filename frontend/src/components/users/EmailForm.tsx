import { Button, TextField, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useFormik } from 'formik';
import { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import * as yup from 'yup';
import { UsersService } from '../../services/usersService';
import { handleErrors } from '../../utils/functions';

const EmailForm = () => {
  const { recipient } = useParams();
  const navigate = useNavigate();

  const validationSchema = yup.object().shape({
    recipient: yup.string().required('Recipient email is required').max(200),
    subject: yup.string().required('Subject is required').max(500),
    body: yup.string().required('Email body is required').max(2000),
  });

  const formik = useFormik({
    initialValues: {
      recipient: '',
      subject: '',
      body: '',
    },
    validationSchema: validationSchema,
    onSubmit: () => {
      UsersService.sendEmailToUser({
        recipient: formik.values.recipient,
        subject: formik.values.subject,
        body: formik.values.body,
      })
        .then(() => {
          navigate('/users');
        })
        .catch((err) => {
          handleErrors(formik, err);
        });
    },
  });

  useEffect(() => {
    if (recipient === '') navigate('/users');

    formik.setValues({ ...formik.values, recipient: recipient as string });
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
        Send email to user
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
          id="recipient"
          label="Recipient email"
          value={formik.values.recipient}
          onChange={formik.handleChange}
          error={formik.touched.recipient && !!formik.errors.recipient}
          helperText={formik.touched.recipient && formik.errors.recipient}
          margin="normal"
          fullWidth
          disabled
        />
        <TextField
          id="subject"
          label="Subject"
          value={formik.values.subject}
          onChange={formik.handleChange}
          error={formik.touched.subject && !!formik.errors.subject}
          helperText={formik.touched.subject && formik.errors.subject}
          margin="normal"
          fullWidth
        />
        <TextField
          id="body"
          label="Body"
          value={formik.values.body}
          onChange={formik.handleChange}
          error={formik.touched.body && !!formik.errors.body}
          helperText={formik.touched.body && formik.errors.body}
          margin="normal"
          multiline
          minRows={7}
          fullWidth
        />
        <Button
          type="submit"
          fullWidth
          variant="contained"
          sx={{ margin: '2rem' }}>
          Send email
        </Button>
      </Box>
    </Box>
  );
};

export default EmailForm;
