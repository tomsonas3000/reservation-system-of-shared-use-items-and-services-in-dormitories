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

const ReservationsCalendar = () => {
  enum View {
    serviceTypes = 'serviceTypes',
    services = 'services',
    reservations = 'reservations',
  }

  const [data, setData] = useState<ServiceType[]>();
  const [serviceTypes, setServiceTypes] = useState<LookupType[]>([]);
  const [services, setServices] = useState<ServiceList[]>([]);
  const [reservations, setReservations] = useState<ReservationsList[]>([]);
  const [activeView, setActiveView] = useState<View>(View.serviceTypes);
  const [isEditingAvailable, setIsEditingAvailable] = useState<boolean>(false);
  const resources = [
    {
      fieldName: 'type',
      title: 'Type',
      instances: [
        { id: 'private', text: 'Private', color: '#EC407A' },
        { id: 'work', text: 'Work', color: '#7E57C2' },
      ],
    },
  ];

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
    setReservations(service.reservationsList);
  };

  const onBackButtonClick = () => {
    if (activeView === View.services) {
      setActiveView(View.serviceTypes);
    } else if (activeView === View.reservations) {
      setActiveView(View.services);
    }
  };

  const CommandLayout = useCallback(({ hideDeleteButton, ...restProps }) => {
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

  const BasicLayout = useCallback(({ booleanEditorComponent, ...props }) => {
    const BooleanEditor = useCallback(({ ...props }) => {
      return <AppointmentForm.BooleanEditor value="" {...props} />;
    }, []);
    return (
      <AppointmentForm.BasicLayout
        booleanEditorComponent={BooleanEditor}
        {...props}
      />
    );
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
                onCommitChanges={({ added, changed, deleted }) => {
                  console.log(added);
                  console.log(changed);
                  console.log(deleted);
                  return data;
                }}
                onEditingAppointmentChange={(reservation) => {
                  if (reservation) {
                    setIsEditingAvailable(true);
                  } else {
                    setIsEditingAvailable(false);
                  }
                }}
                addedAppointment={{}}
                onAddedAppointmentChange={() => console.log('yes')}
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
                basicLayoutComponent={BasicLayout}
              />
            </Scheduler>
          </Paper>
        </>
      )}
    </Box>
  );
};

export default ReservationsCalendar;
