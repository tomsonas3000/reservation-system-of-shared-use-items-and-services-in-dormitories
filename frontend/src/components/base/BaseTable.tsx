import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { useSelector } from 'react-redux';
import { TableModel, HeaderModel } from './models/BaseTableModel';

const BaseTable = (props: { tableData: TableModel }): JSX.Element => {
  const selector = useSelector((state) => state);

  {
    return (
      <TableContainer sx={{ justifyContent: 'center', display: 'flex' }}>
        {console.log(selector)}
        <Table component={Paper} sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              {props.tableData.headers.map(
                (header: HeaderModel, index: number) => {
                  return (
                    <TableCell key={index}>{header.friendlyName}</TableCell>
                  );
                }
              )}
            </TableRow>
          </TableHead>
          {
            <TableBody>
              {props.tableData.rows.map((row: any, index: number) => {
                return (
                  <TableRow key={index}>
                    {props.tableData.headers.map((header, index) => {
                      return (
                        <TableCell key={index}>
                          {row[header.columnName]}
                        </TableCell>
                      );
                    })}
                  </TableRow>
                );
              })}
            </TableBody>
          }
        </Table>
      </TableContainer>
    );
  }
};

export default BaseTable;
