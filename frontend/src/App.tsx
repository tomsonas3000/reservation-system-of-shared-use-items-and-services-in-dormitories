import { ThemeProvider } from '@emotion/react';
import { createTheme } from '@mui/material/styles';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignIn from './components/auth/signIn/SignIn';
import Header from './components/base/Header';

const theme = createTheme();

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <Router>
        <Header />
        <Routes>
          <Route path="/sign-in" element={<SignIn />} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
};

export default App;
