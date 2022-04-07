import { useEffect, useState } from 'react';
import { LookupType } from '../base/types/LookupType';
import * as yup from 'yup';
import { FieldArray, useFormik } from 'formik';
import { Box, Button, MenuItem, TextField, Typography } from '@mui/material';
import { UsersService } from '../../services/usersService';
import { DormitoriesService } from '../../services/dormitoriesService';
import { useNavigate, useParams } from 'react-router-dom';
import { DormitoryDetailsType } from './types/DormitoryDetailsType';
import { handleErrors } from '../../utils/functions';

const DormitoryForm = () => {
  const [managers, setManagers] = useState<LookupType[]>([]);
  const [dormitory, setDormitory] = useState<DormitoryDetailsType>({
    id: '',
    name: '',
    address: '',
    city: '',
    managerId: '',
    rooms: [],
  });

  const { dormitoryId } = useParams();
  const navigate = useNavigate();

  const validationSchema = yup.object().shape({
    name: yup.string().required('Name is required.').max(100),
    address: yup.string().required('Address is required.').max(100),
    city: yup.string().required('City is required.').max(100),
    manager: yup.string().required('Manager is required'),
  });

  const formik = useFormik({
    initialValues: {
      name: '',
      address: '',
      city: '',
      manager: '',
      rooms: [''],
    },
    validationSchema: validationSchema,
    onSubmit: () => {
      if (!dormitoryId) {
        DormitoriesService.createDormitory({
          name: formik.values.name,
          address: formik.values.address,
          city: formik.values.city,
          manager: formik.values.manager,
          rooms: formik.values.rooms,
        })
          .then(() => navigate('/dormitories'))
          .catch((err) => {
            handleErrors(formik, err);
          });
      } else {
        DormitoriesService.updateDormitory(dormitoryId, {
          name: formik.values.name,
          address: formik.values.address,
          city: formik.values.city,
          manager: formik.values.manager,
          rooms: formik.values.rooms,
        })
          .then(() => navigate('/dormitories'))
          .catch((err) => {
            handleErrors(formik, err);
          });
      }
    },
  });

  useEffect(() => {
    UsersService.getManagersLookupList().then((res) => {
      setManagers(res.data);
    });

    if (dormitoryId) {
      DormitoriesService.getDormitory(dormitoryId as string)
        .then((res) => {
          setDormitory(res.data);
        })
        .catch(() => {
          navigate('/dormitories');
        });
    }
  }, []);

  useEffect(() => {
    formik.setValues({
      name: dormitory.name,
      address: dormitory.address,
      city: dormitory.city,
      manager: dormitory.managerId.toUpperCase(),
      rooms: dormitory.rooms,
    });
  }, [dormitory]);

  return (
    <Box
      sx={{
        marginTop: 8,
        flexDirection: 'column',
        alignItems: 'center',
        display: 'flex',
      }}>
      <Typography component="h1" variant="h5">
        {dormitoryId ? 'Update dormitory' : 'Create dormitory'}
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
          label="Dormitory name"
          value={formik.values.name}
          onChange={formik.handleChange}
          error={formik.touched.name && !!formik.errors.name}
          helperText={formik.touched.name && formik.errors.name}
          margin="normal"
          fullWidth
        />
        <TextField
          id="city"
          label="Dormitory city"
          value={formik.values.city}
          onChange={formik.handleChange}
          error={formik.touched.city && !!formik.errors.city}
          helperText={formik.touched.city && formik.errors.city}
          margin="normal"
          fullWidth
        />
        <TextField
          id="address"
          label="Dormitory address"
          value={formik.values.address}
          onChange={formik.handleChange}
          error={formik.touched.address && !!formik.errors.address}
          helperText={formik.touched.address && formik.errors.address}
          margin="normal"
          fullWidth
        />
        <TextField
          id="manager"
          label="Dormitory's manager"
          fullWidth
          margin="normal"
          value={formik.values.manager}
          onChange={(e) => {
            formik.setFieldValue('manager', e.target.value);
          }}
          error={formik.touched.manager && !!formik.errors.manager}
          helperText={formik.touched.manager && formik.errors.manager}
          select>
          {managers.map((manager: LookupType) => {
            return (
              <MenuItem key={manager.value} value={manager.value}>
                {manager.name}
              </MenuItem>
            );
          })}
        </TextField>
        <FieldArray name="rooms" validateOnChange={false}>
          {() =>
            formik.values.rooms?.map((room, index) => {
              return (
                <Box key={index} sx={{ width: '100%', display: 'flex' }}>
                  <TextField
                    name={`rooms.${index}`}
                    key={index}
                    onChange={formik.handleChange}
                    label="Room name"
                    value={room}
                    margin="normal"
                    fullWidth
                    error={
                      index === formik.values.rooms.length - 1
                        ? formik.touched.rooms && !!formik.errors.rooms
                        : false
                    }
                    helperText={
                      index === formik.values.rooms.length - 1
                        ? formik.touched.rooms && formik.errors.rooms
                        : false
                    }
                  />
                  <Button
                    variant="contained"
                    color="error"
                    size="small"
                    disabled={formik.values.rooms.length <= 1}
                    onClick={() =>
                      formik.setFieldValue(
                        'rooms',
                        formik.values.rooms.filter((x) => x !== room)
                      )
                    }
                    sx={{ marginY: '0.9rem', marginX: '0.5rem' }}>
                    x
                  </Button>
                  {index === formik.values.rooms.length - 1 ? (
                    <Button
                      variant="contained"
                      color="success"
                      size="small"
                      onClick={() =>
                        formik.setFieldValue('rooms', [
                          ...formik.values.rooms,
                          '',
                        ])
                      }
                      sx={{ marginY: '0.9rem', marginX: '0.5rem' }}>
                      +
                    </Button>
                  ) : null}
                </Box>
              );
            })
          }
        </FieldArray>
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

export default DormitoryForm;
