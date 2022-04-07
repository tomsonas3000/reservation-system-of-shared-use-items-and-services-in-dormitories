import {
  AppBar,
  Button,
  MenuItem,
  Toolbar,
  Drawer,
  IconButton,
  Typography,
  Grid,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { useEffect, useState } from 'react';
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
  //const [mobileView, setMobileView] = useState(false);

  const authState = useSelector((state: RootState) => state.auth);
  const dispath = useDispatch();
  const navigate = useNavigate();

  const handleLogoutClick = () => {
    dispath(logout());
    navigate('/sign-in');
  };

  // useEffect(() => {
  //   const handleResize = () => {
  //     window.innerWidth < 600 ? setMobileView(true) : setMobileView(false);
  //   };

  //   handleResize();

  //   window.addEventListener('resize', handleResize);
  // }, []);

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
      }
      if (authState.role === Role.Student) {
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

// const MobileHeader = (): JSX.Element => {
//   const [drawerOpen, setDrawerOpen] = useState(false);

//   return (
//     <Toolbar sx={{ justifyContent: 'right' }} color="inherit">
//       <IconButton
//         color="inherit"
//         aria-label="menu"
//         aria-haspopup="true"
//         onClick={() => setDrawerOpen(!drawerOpen)}>
//         <MenuIcon />
//       </IconButton>
//       <Drawer
//         sx={{ padding: '2rem' }}
//         anchor="right"
//         open={drawerOpen}
//         onClose={() => setDrawerOpen(!drawerOpen)}>
//         <DrawerButtons />
//       </Drawer>
//     </Toolbar>
//   );
// };

// const DrawerButtons = (): JSX.Element => {
//   return (
//     <>
//       {adminMenuItems.map(({ label, href }) => {
//         return (
//           <Link
//             to={href}
//             color="black"
//             key={label}
//             style={{ textDecoration: 'none' }}>
//             <MenuItem>
//               <Typography color="black">{label}</Typography>
//             </MenuItem>
//           </Link>
//         );
//       })}
//     </>
//   );
// };

export default Header;
