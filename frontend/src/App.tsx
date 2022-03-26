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

const theme = createTheme();

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <Router>
        <Header />
        <Routes>
          <Route path="" element={<SignIn />} />
          <Route path="sign-in" element={<SignIn />} />
          <Route
            path="dormitories"
            element={
              <ProtectedRoute outlet={<Dormitories />} role={role.Admin} />
            }
          />
          <Route
            path="dormitories/:dormitoryId"
            element={
              <ProtectedRoute outlet={<DormitoryForm />} role={role.Admin} />
            }
          />
          <Route
            path="create-dormitory"
            element={
              <ProtectedRoute outlet={<DormitoryForm />} role={role.Admin} />
            }
          />
          <Route
            path="dormitories/:dormitoryId/manage-students"
            element={
              <ProtectedRoute outlet={<ManageStudents />} role={role.Admin} />
            }
          />
          <Route
            path="users"
            element={<ProtectedRoute outlet={<Users />} role={role.Admin} />}
          />
          <Route
            path="services"
            element={<ProtectedRoute outlet={<Services />} role={role.Admin} />}
          />
          <Route
            path="create-service"
            element={
              <ProtectedRoute outlet={<ServiceForm />} role={role.Admin} />
            }
          />
          <Route
            path="services/:serviceId"
            element={
              <ProtectedRoute outlet={<ServiceForm />} role={role.Admin} />
            }
          />
          <Route
            path="reservations-admin"
            element={
              <ProtectedRoute outlet={<Reservations />} role={role.Admin} />
            }
          />
          <Route
            path="reservations"
            element={
              <ProtectedRoute outlet={<h1>Test</h1>} role={role.Student} />
            }
          />
          <Route element={<h1 style={{ marginTop: '5rem' }}>Not Found</h1>} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
};

export default App;
