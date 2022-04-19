import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { DormitoriesService } from '../../services/dormitoriesService';
import DormitoriesTable from './DormitoriesTable';
import { DormitoryType } from './types/DormitoryType';

const Dormitories = () => {
  const [dormitories, setDormitories] = useState<DormitoryType[]>([]);

  const navigate = useNavigate();

  useEffect(() => {
    DormitoriesService.getDormitories().then((res) => {
      setDormitories(res.data);
    });
  }, []);

  return (
    <Grid container m={4} sx={{ display: 'flex', justifyContent: 'center' }}>
      <Grid item>
        <Button
          size="large"
          variant="contained"
          onClick={() => navigate('/create-dormitory')}>
          Add new dormitory
        </Button>
        <DormitoriesTable data={dormitories} />
      </Grid>
    </Grid>
  );
};

export default Dormitories;
