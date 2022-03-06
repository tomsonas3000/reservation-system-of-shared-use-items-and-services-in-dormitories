import { ThemeProvider } from '@emotion/react';
import { createTheme } from '@mui/material/styles';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignIn from './components/auth/signIn/SignIn';
import Header from './components/base/Header';
import BaseTable from './components/base/BaseTable';
import { TableModel } from './components/base/models/BaseTableModel';
import ProtectedRoute from './routes/ProtectedRoute';
import role from './utils/enums/role';

const theme = createTheme();

const App = () => {
  const x: TableModel = {
    headers: [
      { columnName: 'column1', friendlyName: 'name 1' },
      { columnName: 'column2', friendlyName: 'name 2' },
      { columnName: 'column3', friendlyName: 'name 3' },
    ],
    rows: [
      {
        column1: 'test1',
        column2: 'test1',
        column3: 'test1',
      },
      {
        column1: 'test2',
        column2: 'test2',
        column3: 'test2',
      },

      {
        column1: 'test3',
        column2: 'test3',
        column3: 'test3',
      },
    ],
  };

  return (
    <ThemeProvider theme={theme}>
      <Router>
        <Header />
        <Routes>
          <Route path="sign-in" element={<SignIn />} />
          <Route
            path="dormitories"
            element={
              <ProtectedRoute
                outlet={<BaseTable tableData={x} />}
                role={role.Admin}
              />
            }
          />
        </Routes>
      </Router>
    </ThemeProvider>
  );
};

export default App;
