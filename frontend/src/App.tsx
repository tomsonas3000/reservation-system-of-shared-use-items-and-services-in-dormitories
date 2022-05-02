import { ThemeProvider } from '@emotion/react';
import { createTheme } from '@mui/material/styles';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignIn from './components/auth/signIn/SignIn';
import Header from './components/base/Header';
import ProtectedRoute from './routes/ProtectedRoute';
import role from './utils/enums/role';
import Dormitories from './components/dormitories/Dormitories';
import DormitoryForm from './components/dormitories/DormitoryForm';
import Users from './components/users/Users';
import Services from './components/services/Services';
import Reservations from './components/reservations/Reservations';
import ServiceForm from './components/services/ServiceForm';
import ManageStudents from './components/dormitories/ManageStudents';
import ReservationsCalendar from './components/reservations/ReservationsCalendar';
import UserForm from './components/users/UserForm';
import DormitoryReservations from './components/reservations/DormitoryReservations';
import ServiceReservations from './components/reservations/ServiceReservations';
import UserReservations from './components/reservations/UserReservations';
import EmailForm from './components/users/EmailForm';

const theme = createTheme();

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <Router basename="/">
        <Header />
        <Routes>
          <Route path="" element={<SignIn />} />
          <Route path="sign-in" element={<SignIn />} />
          <Route
            path="dormitories"
            element={
              <ProtectedRoute outlet={<Dormitories />} roles={[role.Admin]} />
            }
          />
          <Route
            path="dormitories/:dormitoryId"
            element={
              <ProtectedRoute outlet={<DormitoryForm />} roles={[role.Admin]} />
            }
          />
          <Route
            path="create-dormitory"
            element={
              <ProtectedRoute outlet={<DormitoryForm />} roles={[role.Admin]} />
            }
          />
          <Route
            path="dormitories/:dormitoryId/manage-students"
            element={
              <ProtectedRoute
                outlet={<ManageStudents />}
                roles={[role.Admin]}
              />
            }
          />
          <Route
            path="users"
            element={<ProtectedRoute outlet={<Users />} roles={[role.Admin]} />}
          />
          <Route
            path="create-user"
            element={
              <ProtectedRoute outlet={<UserForm />} roles={[role.Admin]} />
            }
          />
          <Route
            path="services"
            element={
              <ProtectedRoute outlet={<Services />} roles={[role.Admin]} />
            }
          />
          <Route
            path="create-service"
            element={
              <ProtectedRoute outlet={<ServiceForm />} roles={[role.Admin]} />
            }
          />
          <Route
            path="services/:serviceId"
            element={
              <ProtectedRoute outlet={<ServiceForm />} roles={[role.Admin]} />
            }
          />
          <Route
            path="reservations-admin"
            element={
              <ProtectedRoute outlet={<Reservations />} roles={[role.Admin]} />
            }
          />
          <Route
            path="dormitory-reservations/:dormitoryId"
            element={
              <ProtectedRoute
                outlet={<DormitoryReservations />}
                roles={[role.Admin]}
              />
            }
          />
          <Route
            path="service-reservations/:serviceId"
            element={
              <ProtectedRoute
                outlet={<ServiceReservations />}
                roles={[role.Admin]}
              />
            }
          />
          <Route
            path="user-reservations/:userId"
            element={
              <ProtectedRoute
                outlet={<UserReservations />}
                roles={[role.Admin]}
              />
            }
          />
          <Route
            path="reservations-calendar"
            element={
              <ProtectedRoute
                outlet={<ReservationsCalendar />}
                roles={[role.Student, role.Manager]}
              />
            }
          />
          <Route
            path="users/send-email/:recipient"
            element={
              <ProtectedRoute outlet={<EmailForm />} roles={[role.Admin]} />
            }
          />
          <Route element={<h1 style={{ marginTop: '5rem' }}>Not Found</h1>} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
};

export default App;
