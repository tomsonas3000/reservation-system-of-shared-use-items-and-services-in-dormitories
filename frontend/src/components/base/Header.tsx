import {
  AppBar,
  Button,
  MenuItem,
  Toolbar,
  Drawer,
  IconButton,
  Typography,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

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
    href: '/reservations',
  },
];

const Header = () => {
  const [mobileView, setMobileView] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      window.innerWidth < 600 ? setMobileView(true) : setMobileView(false);
    };

    handleResize();

    window.addEventListener('resize', handleResize);
  }, []);

  return (
    <header>
      <AppBar color="inherit" position="sticky">
        {mobileView ? <MobileHeader /> : <DesktopHeader />}
      </AppBar>
    </header>
  );
};

const DesktopHeader = (): JSX.Element => {
  return (
    <Toolbar color="inherit" sx={{ display: 'flex', justifyContent: 'center' }}>
      <HeaderButtons />
    </Toolbar>
  );
};

const MobileHeader = (): JSX.Element => {
  const [drawerOpen, setDrawerOpen] = useState(false);

  return (
    <Toolbar sx={{ justifyContent: 'right' }} color="inherit">
      <IconButton
        color="inherit"
        aria-label="menu"
        aria-haspopup="true"
        onClick={() => setDrawerOpen(!drawerOpen)}>
        <MenuIcon />
      </IconButton>
      <Drawer
        sx={{ padding: '2rem' }}
        anchor="right"
        open={drawerOpen}
        onClose={() => setDrawerOpen(!drawerOpen)}>
        <DrawerButtons />
      </Drawer>
    </Toolbar>
  );
};

const HeaderButtons = (): JSX.Element => {
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
};

const DrawerButtons = (): JSX.Element => {
  return (
    <>
      {adminMenuItems.map(({ label, href }) => {
        return (
          <Link
            to={href}
            color="black"
            key={label}
            style={{ textDecoration: 'none' }}>
            <MenuItem>
              <Typography color="black">{label}</Typography>
            </MenuItem>
          </Link>
        );
      })}
    </>
  );
};

export default Header;
