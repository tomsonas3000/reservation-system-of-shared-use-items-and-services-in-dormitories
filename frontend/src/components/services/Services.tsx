import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { ServicesService } from '../../services/servicesService';
import ServicesTable from './ServicesTable';
import { ServiceType } from './types/ServiceType';

const Services = () => {
  const [services, setServices] = useState<ServiceType[]>([]);

  useEffect(() => {
    ServicesService.getServices().then((res) => {
      setServices(res.data);
    });
  }, []);

  return (
    <Grid container m={4}>
      <Grid item>
        <Button size="large" variant="contained">
          Add new service
        </Button>
        <ServicesTable data={services} />
      </Grid>
    </Grid>
  );
};

export default Services;
