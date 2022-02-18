import { ThemeProvider } from '@emotion/react';
import { Container } from '@mui/material';
import { createTheme } from '@mui/material/styles';
import SignIn from './components/auth/signIn/SignIn';

const theme = createTheme();

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <Container
        maxWidth="xs"
        sx={{
          display: 'flex',
        }}>
        <SignIn />
      </Container>
    </ThemeProvider>
  );
};

export default App;
