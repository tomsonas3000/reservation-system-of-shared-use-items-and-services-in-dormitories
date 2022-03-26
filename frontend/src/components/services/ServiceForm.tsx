import { Box, Button, MenuItem, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import * as yup from 'yup';
import { DormitoriesService } from '../../services/dormitoriesService';
import { RoomsService } from '../../services/roomsService';
import { ServicesService } from '../../services/servicesService';
import { LookupType } from '../base/types/LookupType';
import { RoomType } from '../rooms/types/RoomType';
import { ServiceDetailsType } from './types/ServiceDetailsType';
import { handleErrors } from '../../utils/functions';

const ServiceForm = () => {
  const [serviceTypes, setServiceTypes] = useState<LookupType[]>([]);
  const [dormitories, setDormitories] = useState<LookupType[]>([]);
  const [rooms, setRooms] = useState<RoomType[]>([]);
  const [availableRooms, setAvailableRooms] = useState<RoomType[]>([]);
  const [service, setService] = useState<ServiceDetailsType>({
    id: '',
    name: '',
    type: '',
    maxTimeOfUse: 0,
    maxAmountOfUsers: 0,
    dormitoryId: '',
    roomId: '',
  });

  const { serviceId } = useParams();
  const navigate = useNavigate();

  const validationSchema = yup.object().shape({
    name: yup.string().required('Service name is required.').max(200),
    type: yup.string().required('Service type is required.'),
    maxTimeOfUse: yup
      .number()
      .required('Maximum time of use is required.')
      .min(10)
      .max(400),
    maxAmountOfUsers: yup
      .number()
      .required('Maximum amount of users is required.')
      .min(1)
      .max(10),
    dormitory: yup.string().required('Dormitory is required.'),
    room: yup.string().required('Room is required.'),
  });

  const formik = useFormik({
    initialValues: {
      name: '',
      type: '',
      maxTimeOfUse: 0,
      maxAmountOfUsers: 0,
      dormitory: '',
      room: '',
    },
    validationSchema: validationSchema,
    onSubmit: () => {
      if (!serviceId) {
        ServicesService.createService({
          name: formik.values.name,
          type: formik.values.type,
          maxTimeOfUse: formik.values.maxTimeOfUse,
          maxAmountOfUsers: formik.values.maxAmountOfUsers,
          room: formik.values.room,
          dormitory: formik.values.dormitory,
        })
          .then(() => {
            navigate('/services');
          })
          .catch((err) => {
            handleErrors(formik, err);
          });
      } else {
        ServicesService.updateService(serviceId, {
          name: formik.values.name,
          type: formik.values.type,
          maxTimeOfUse: formik.values.maxTimeOfUse,
          maxAmountOfUsers: formik.values.maxAmountOfUsers,
          room: formik.values.room,
          dormitory: formik.values.dormitory,
        })
          .then(() => {
            navigate('/services');
          })
          .catch((err) => {
            handleErrors(formik, err);
          });
      }
    },
  });

  useEffect(() => {
    ServicesService.getServiceTypes().then((res) => {
      setServiceTypes(res.data);
    });

    DormitoriesService.getDormitoriesLookupList().then((res) => {
      setDormitories(res.data);
    });

    RoomsService.getRooms().then((res) => {
      setRooms(res.data);
    });

    if (serviceId) {
      ServicesService.getService(serviceId as string)
        .then((res) => {
          setService(res.data);
        })
        .catch(() => {
          navigate('/services');
        });
    }
  }, []);

  useEffect(() => {
    setAvailableRooms(
      rooms.filter(
        (x) => x.dormitoryId.toUpperCase() === service.dormitoryId.toUpperCase()
      )
    );
    formik.setValues({
      name: service.name,
      type: service.type,
      maxAmountOfUsers: service.maxAmountOfUsers,
      maxTimeOfUse: service.maxTimeOfUse,
      dormitory: service.dormitoryId.toUpperCase(),
      room: service.roomId,
    });
  }, [service]);

  return (
    <Box
      sx={{
        marginTop: 8,
        flexDirection: 'column',
        alignItems: 'center',
        display: 'flex',
      }}>
      <Typography component="h1" variant="h5">
        {serviceId ? 'Update service' : 'Create service'}
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
          label="Name of the service"
          fullWidth
          margin="normal"
          type="string"
          value={formik.values.name}
          onChange={formik.handleChange}
          error={formik.touched.name && !!formik.errors.name}
          helperText={formik.touched.name && formik.errors.name}
        />
        <TextField
          id="type"
          label="Service type"
          select
          value={formik.values.type}
          onChange={(e) => formik.setFieldValue('type', e.target.value)}
          error={formik.touched.type && !!formik.errors.type}
          helperText={formik.touched.type && formik.errors.type}
          margin="normal"
          fullWidth>
          {serviceTypes.map((type: LookupType) => {
            return (
              <MenuItem key={type.value} value={type.value}>
                {type.name}
              </MenuItem>
            );
          })}
        </TextField>
        <TextField
          id="maxTimeOfUse"
          label="Maximum time (in minutes) to use the service"
          fullWidth
          margin="normal"
          type="number"
          value={formik.values.maxTimeOfUse}
          onChange={formik.handleChange}
          error={formik.touched.maxTimeOfUse && !!formik.errors.maxTimeOfUse}
          helperText={formik.touched.maxTimeOfUse && formik.errors.maxTimeOfUse}
        />
        <TextField
          id="maxAmountOfUsers"
          label="Maximum amount of users that can use the service at one moment"
          fullWidth
          margin="normal"
          type="number"
          value={formik.values.maxAmountOfUsers}
          onChange={formik.handleChange}
          error={
            formik.touched.maxAmountOfUsers && !!formik.errors.maxAmountOfUsers
          }
          helperText={
            formik.touched.maxAmountOfUsers && formik.errors.maxAmountOfUsers
          }
        />
        <TextField
          id="dormitory"
          label="Dormitory in which the service is"
          fullWidth
          margin="normal"
          value={formik.values.dormitory}
          onChange={(e) => {
            formik.setFieldValue('dormitory', e.target.value);
            setAvailableRooms(
              rooms.filter((x) => x.dormitoryId == e.target.value)
            );
            formik.setFieldValue('room', '');
          }}
          error={formik.touched.dormitory && !!formik.errors.dormitory}
          helperText={formik.touched.dormitory && formik.errors.dormitory}
          select>
          {dormitories.map((dormitory: LookupType) => {
            return (
              <MenuItem key={dormitory.value} value={dormitory.value}>
                {dormitory.name}
              </MenuItem>
            );
          })}
        </TextField>
        <TextField
          id="room"
          label="Dormitory's room in which the service is"
          fullWidth
          margin="normal"
          value={formik.values.room}
          onChange={(e) => formik.setFieldValue('room', e.target.value)}
          error={formik.touched.room && !!formik.errors.room}
          helperText={formik.touched.room && formik.errors.room}
          select>
          {availableRooms.map((room: RoomType) => {
            return (
              <MenuItem key={room.id} value={room.id}>
                {room.name}
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

export default ServiceForm;
