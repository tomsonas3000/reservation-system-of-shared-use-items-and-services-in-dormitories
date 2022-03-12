import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { DormitoriesService } from '../../services/dormitoriesService';
import DormitoriesTable from './DormitoriesTable';
import { DormitoryType } from './types/DormitoryType';

const Dormitories = () => {
  const [dormitories, setDormitories] = useState<DormitoryType[]>([]);

  useEffect(() => {
    DormitoriesService.getDormitories().then((res) => {
      setDormitories(res.data);
    });
  }, []);

  return (
    <Grid container m={4}>
      <Grid item>
        <Button size="large" variant="contained">
          Add new dormitory
        </Button>
        <DormitoriesTable data={dormitories} />
      </Grid>
    </Grid>
  );
};

export default Dormitories;
