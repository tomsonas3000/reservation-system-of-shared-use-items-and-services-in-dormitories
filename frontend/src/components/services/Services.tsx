import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ServicesService } from '../../services/servicesService';
import ServicesTable from './ServicesTable';
import { ServiceType } from './types/ServiceType';

const Services = () => {
  const navigate = useNavigate();

  const [services, setServices] = useState<ServiceType[]>([]);

  useEffect(() => {
    ServicesService.getServices().then((res) => {
      setServices(res.data);
    });
  }, []);

  return (
    <Grid container m={4} sx={{ display: 'flex', justifyContent: 'center' }}>
      <Grid item>
        <Button
          size="large"
          variant="contained"
          onClick={() => navigate('/create-service')}>
          Add new service
        </Button>
        <ServicesTable data={services} />
      </Grid>
    </Grid>
  );
};

export default Services;
