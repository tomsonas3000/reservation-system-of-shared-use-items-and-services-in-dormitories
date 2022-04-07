import {
  Button,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Paper,
} from '@mui/material';
import { Box } from '@mui/system';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
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

const ReservationsCalendar = () => {
  enum View {
    serviceTypes = 'serviceTypes',
    services = 'services',
    reservations = 'reservations',
  }

  const [data, setData] = useState<ServiceType[]>();
  const [serviceTypes, setServiceTypes] = useState<LookupType[]>([]);
  const [services, setServices] = useState<ServiceList[]>([]);
  const [activeService, setActiveService] = useState<ServiceList>();
  const [reservations, setReservations] = useState<ReservationsList[]>([]);
  const [activeView, setActiveView] = useState<View>(View.serviceTypes);
  const [isEditingAvailable, setIsEditingAvailable] = useState<boolean>(false);
  const [addedAppointment, setAddedAppointment] = useState({});

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
    ({ added }) => {
      if (added) {
        const request: CreateReservation = {
          serviceId: activeService?.id,
          startDate: added?.startDate,
          endDate: added?.endDate,
        };

        ReservationsService.createReservation(request)
          .then(() => {
            ReservationsService.getReservationsCalendar().then((res) => {
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
            alert(err.response.data['reservationsList']);
          });
      }
    },
    [setData, reservations]
  );

  const onAddedAppointmentChange = useCallback((appointment) => {
    setAddedAppointment(appointment);
  }, []);

  const CommandLayout = useCallback(({ ...restProps }) => {
    return (
      <AppointmentForm.CommandLayout hideDeleteButton={true} {...restProps} />
    );
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

  return (
    <Box margin={4}>
      {activeView === View.serviceTypes && (
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
      )}
      {activeView === View.services && (
        <>
          <IconButton onClick={onBackButtonClick}>
            <ArrowBackIcon />
          </IconButton>
          <List>
            {services?.map((service, idx) => {
              return (
                <ListItem key={idx}>
                  <ListItemButton onClick={() => onServiceClick(service)}>
                    <ListItemText primary={service.name}></ListItemText>
                  </ListItemButton>
                </ListItem>
              );
            })}
          </List>
        </>
      )}
      {activeView === View.reservations && (
        <>
          <IconButton onClick={onBackButtonClick}>
            <ArrowBackIcon />
          </IconButton>
          <Paper>
            <Scheduler data={reservations}>
              <ViewState />
              <EditingState
                onCommitChanges={onCommitChanges}
                addedAppointment={addedAppointment}
                onAddedAppointmentChange={onAddedAppointmentChange}
                onEditingAppointmentChange={(reservation) => {
                  if (reservation) {
                    setIsEditingAvailable(true);
                  } else {
                    setIsEditingAvailable(false);
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
                readOnly={isEditingAvailable}
                commandLayoutComponent={CommandLayout}
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
