import { Button, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { DormitoriesService } from '../../services/dormitoriesService';
import BaseTable from '../base/BaseTable';
import { TableType } from '../base/models/BaseTableModel';

const Dormitories = () => {
  const x: TableType = {
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
  const [dormitories, setDormitories] = useState<TableType>(x);

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
        <BaseTable tableData={dormitories} />
      </Grid>
    </Grid>
  );
};

export default Dormitories;
