import {
  Alert,
  Button,
  Fab,
  Grid,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Modal,
  Paper,
  Snackbar,
  Typography,
} from '@mui/material';
import { Box } from '@mui/system';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import QuestionMarkIcon from '@mui/icons-material/QuestionMark';
import { useCallback, useEffect, useState } from 'react';
import { ReservationsService } from '../../services/reservationsService';
import { LookupType } from '../base/types/LookupType';
import {
  ReservationsList,
  ServiceList,
  ServiceType,
} from './types/ReservationCalendar';
import {
  ViewState,
  IntegratedEditing,
  EditingState,
} from '@devexpress/dx-react-scheduler';
import {
  Scheduler,
  DayView,
  Appointments,
  WeekView,
  Toolbar,
  ViewSwitcher,
  DateNavigator,
  TodayButton,
  AppointmentForm,
} from '@devexpress/dx-react-scheduler-material-ui';
import { CreateReservation } from './types/CreateReservation';
import { RootState } from '../../redux/store';
import { useSelector } from 'react-redux';
import Role from '../../utils/enums/role';
import { UpdateReservation } from './types/UpdateReservation';

const ReservationsCalendar = () => {
  enum View {
    serviceTypes = 'serviceTypes',
    services = 'services',
    reservations = 'reservations',
  }

  const authState = useSelector((state: RootState) => state.auth);

  const [data, setData] = useState<ServiceType[]>();
  const [serviceTypes, setServiceTypes] = useState<LookupType[]>([]);
  const [services, setServices] = useState<ServiceList[]>([]);
  const [activeService, setActiveService] = useState<ServiceList>();
  const [reservations, setReservations] = useState<ReservationsList[]>([]);
  const [activeView, setActiveView] = useState<View>(View.serviceTypes);
  const [isEditingDisabled, setIsEditingDisabled] = useState<boolean>(false);
  const [addedAppointment, setAddedAppointment] = useState({});
  const [openSuccessSnackbar, setOpenSuccessSnackbar] = useState(false);
  const [openDeleteSuccessSnackbar, setOpenDeleteSuccessSnackbar] =
    useState(false);
  const [openErrorSnackbar, setOpenErrorSnackbar] = useState(false);
  const [errorSnackbarMessage, setErrorSnackbarMessage] = useState('');
  const [helpModalOpen, setHelpModalOpen] = useState(false);

  useEffect(() => {
    ReservationsService.getReservationsCalendar().then((res) => {
      setData(res.data);
      setServiceTypes(res.data.map((x: ServiceType) => x.type));
    });
  }, []);

  const onServiceTypeClick = (type: LookupType) => {
    setActiveView(View.services);
    setServices(
      data?.find((x) => x.type.value === type.value)
        ?.serviceList as ServiceList[]
    );
  };

  const onServiceClick = (service: ServiceList) => {
    setActiveView(View.reservations);
    setActiveService(service);
    setReservations(service.reservationsList);
  };

  const onBackButtonClick = () => {
    if (activeView === View.services) {
      setActiveView(View.serviceTypes);
      setActiveService(undefined);
    } else if (activeView === View.reservations) {
      setActiveView(View.services);
      setReservations([]);
    }
  };

  const onCommitChanges = useCallback(
    ({ added, changed, deleted }) => {
      if (added) {
        const request: CreateReservation = {
          serviceId: activeService?.id,
          startDate: added?.startDate,
          endDate: added?.endDate,
        };

        ReservationsService.createReservation(request)
          .then(() => {
            ReservationsService.getReservationsCalendar().then((res) => {
              setOpenSuccessSnackbar(true);
              res.data.forEach((type: ServiceType) => {
                type.serviceList.forEach((service) => {
                  if (service.id === activeService?.id) {
                    setReservations(service.reservationsList);
                  }
                });
              });
            });
          })
          .catch((err) => {
            setErrorSnackbarMessage(
              err.response.data[Object.keys(err.response.data)[0]]
            );
            setOpenErrorSnackbar(true);
          });
      } else if (changed) {
        const request: UpdateReservation = {
          startDate: changed[Object.keys(changed)[0]]?.startDate,
          endDate: changed[Object.keys(changed)[0]]?.endDate,
        };

        ReservationsService.updateReservation(request, Object.keys(changed)[0])
          .then(() => {
            ReservationsService.getReservationsCalendar().then((res) => {
              setOpenSuccessSnackbar(true);
              res.data.forEach((type: ServiceType) => {
                type.serviceList.forEach((service) => {
                  if (service.id === activeService?.id) {
                    setReservations(service.reservationsList);
                  }
                });
              });
            });
          })
          .catch((err) => {
            setErrorSnackbarMessage(
              err.response.data[Object.keys(err.response.data)[0]]
            );
            setOpenErrorSnackbar(true);
          });
      } else if (deleted) {
        ReservationsService.deleteReservation(deleted)
          .then(() => {
            ReservationsService.getReservationsCalendar().then((res) => {
              setOpenDeleteSuccessSnackbar(true);
              res.data.forEach((type: ServiceType) => {
                type.serviceList.forEach((service) => {
                  if (service.id === activeService?.id) {
                    setReservations(service.reservationsList);
                  }
                });
              });
            });
          })
          .catch((err) => {
            setErrorSnackbarMessage(
              err.response.data[Object.keys(err.response.data)[0]]
            );
            setOpenErrorSnackbar(true);
          });
      }
    },
    [setData, reservations]
  );

  const onAddedAppointmentChange = useCallback((appointment) => {
    setAddedAppointment(appointment);
  }, []);

  const TextEditor = useCallback(({ type, ...props }) => {
    // eslint-disable-next-line react/destructuring-assignment
    if (type === 'titleTextEditor' || type === 'multilineTextEditor') {
      return null;
    }
    return <AppointmentForm.TextEditor type {...props} />;
  }, []);

  const BooleanEditor = useCallback(() => {
    return null;
  }, []);

  const Label = useCallback(({ ...props }) => {
    return <AppointmentForm.Label {...props} text="" />;
  }, []);

  const modalContentStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    backgroundColor: 'white',
    p: 3,
  };

  return (
    <Box margin={4}>
      <Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        open={openSuccessSnackbar}
        onClose={() => setOpenSuccessSnackbar(false)}
        autoHideDuration={6000}>
        <Alert severity="success">Reservation was added successfully!</Alert>
      </Snackbar>
      <Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        open={openDeleteSuccessSnackbar}
        onClose={() => setOpenDeleteSuccessSnackbar(false)}
        autoHideDuration={6000}>
        <Alert severity="success">Reservation was deleted successfully!</Alert>
      </Snackbar>
      <Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        open={openErrorSnackbar}
        onClose={() => setOpenErrorSnackbar(false)}
        autoHideDuration={6000}>
        <Alert severity="error">{errorSnackbarMessage}</Alert>
      </Snackbar>
      <Modal open={helpModalOpen} onClose={() => setHelpModalOpen(false)}>
        <Box sx={modalContentStyle}>
          <Typography variant="h6" sx={{ mb: 2 }}>
            Clicking available cell will open a view where reservation start and
            end dates can be selected.
          </Typography>
          <Typography variant="body2">
            At most, 3 active or upcoming reservations for a service can be
            selected.
          </Typography>
          <Typography variant="body2">
            At most, 5 active or upcoming reservations can be selected in total
          </Typography>
          <Typography variant="body2">
            Maximum reservation time for {activeService?.name} {` is `}
            {activeService?.maximumTimeOfUse} minutes.
          </Typography>
          <Box sx={{ justifyContent: 'flex-end', display: 'flex' }}>
            <Button
              sx={{ mx: 1, mt: 2, mb: 1 }}
              variant="contained"
              onClick={() => setHelpModalOpen(false)}>
              Close
            </Button>
          </Box>
        </Box>
      </Modal>
      {activeView === View.serviceTypes && (
        <>
          <Typography sx={{ my: 1, fontSize: 24 }} variant="h6">
            Service types
          </Typography>
          <List>
            {serviceTypes?.map((type, idx) => {
              return (
                <ListItem key={idx}>
                  <ListItemButton onClick={() => onServiceTypeClick(type)}>
                    <ListItemText primary={type.name}></ListItemText>
                  </ListItemButton>
                </ListItem>
              );
            })}
          </List>
        </>
      )}
      {activeView === View.services && (
        <>
          <Box sx={{ display: 'flex' }}>
            <IconButton sx={{ mx: 1 }} onClick={onBackButtonClick}>
              <ArrowBackIcon />
            </IconButton>
            <Typography sx={{ my: 1, fontSize: 24 }} variant="h6">
              Services
            </Typography>
          </Box>
          <List>
            {services?.map((service, idx) => {
              return (
                <ListItem key={idx}>
                  <ListItemButton onClick={() => onServiceClick(service)}>
                    <ListItemText
                      primary={service.name}
                      secondary={service.room}></ListItemText>
                  </ListItemButton>
                </ListItem>
              );
            })}
          </List>
        </>
      )}
      {activeView === View.reservations && (
        <>
          <Grid container sx={{ display: 'flex', pb: 2 }}>
            <Grid item xs={10} sx={{ display: 'flex' }}>
              <IconButton sx={{ mx: 1 }} onClick={onBackButtonClick}>
                <ArrowBackIcon />
              </IconButton>
              <Typography sx={{ my: 1, fontSize: 24 }} variant="h6">
                {activeService?.name} reservations calendar
              </Typography>
            </Grid>
            <Grid
              item
              xs={2}
              sx={{ display: 'flex', justifyContent: 'flex-end' }}>
              <Fab
                color="primary"
                aria-label="add"
                onClick={() => setHelpModalOpen(true)}>
                <QuestionMarkIcon />
              </Fab>
            </Grid>
          </Grid>
          <Paper>
            <Scheduler data={reservations}>
              <ViewState />
              <EditingState
                onCommitChanges={onCommitChanges}
                addedAppointment={addedAppointment}
                onAddedAppointmentChange={onAddedAppointmentChange}
                onEditingAppointmentChange={(reservation) => {
                  if (
                    reservation &&
                    !reservation?.isBookedByUser &&
                    authState.role !== Role.Manager
                  ) {
                    setIsEditingDisabled(true);
                  } else {
                    setIsEditingDisabled(false);
                  }
                }}
              />
              <IntegratedEditing />
              <DayView startDayHour={8} endDayHour={23} />
              <WeekView />
              <Toolbar />
              <DateNavigator />
              <TodayButton />
              <ViewSwitcher />
              <Appointments />

              <AppointmentForm
                readOnly={isEditingDisabled}
                textEditorComponent={TextEditor}
                booleanEditorComponent={BooleanEditor}
                labelComponent={Label}
              />
            </Scheduler>
          </Paper>
        </>
      )}
    </Box>
  );
};

export default ReservationsCalendar;
