import { AppBar, Button, Toolbar, Grid } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../redux/store';
import { logout } from '../../redux/slices/authSlice';
import Role from '../../utils/enums/role';

const adminMenuItems = [
  {
    label: 'Dormitories',
    href: '/dormitories',
  },
  {
    label: 'Users',
    href: '/users',
  },
  {
    label: 'Services',
    href: '/services',
  },
  {
    label: 'Reservations',
    href: '/reservations-admin',
  },
];

const studentMenuItems = [
  {
    label: 'Reservations',
    href: '/reservations-calendar',
  },
];

const Header = () => {
  const authState = useSelector((state: RootState) => state.auth);
  const dispath = useDispatch();
  const navigate = useNavigate();

  const handleLogoutClick = () => {
    dispath(logout());
    navigate('/sign-in');
  };

  const HeaderButtons = (): JSX.Element => {
    const ButtonsByRole = (): JSX.Element => {
      if (authState.role === Role.Admin) {
        return (
          <>
            {adminMenuItems.map(({ label, href }) => {
              return (
                <Button
                  key={label}
                  to={href}
                  color="inherit"
                  component={Link}
                  sx={{ marginX: '2rem' }}>
                  {label}
                </Button>
              );
            })}
          </>
        );
      } else {
        return (
          <>
            {studentMenuItems.map(({ label, href }) => {
              return (
                <Button
                  key={label}
                  to={href}
                  color="inherit"
                  component={Link}
                  sx={{ marginX: '2rem' }}>
                  {label}
                </Button>
              );
            })}
          </>
        );
      }

      return <></>;
    };

    return (
      <Grid container display="flex" justifyContent="center">
        <ButtonsByRole />
        <Button onClick={handleLogoutClick}>Logout</Button>
      </Grid>
    );
  };

  const DesktopHeader = (): JSX.Element => {
    return (
      <Toolbar
        color="inherit"
        sx={{ display: 'flex', justifyContent: 'center' }}>
        <HeaderButtons />
      </Toolbar>
    );
  };

  return authState.isLoggedIn ? (
    <header>
      <AppBar color="inherit" position="sticky">
        <DesktopHeader />
      </AppBar>
    </header>
  ) : null;
};

export default Header;
