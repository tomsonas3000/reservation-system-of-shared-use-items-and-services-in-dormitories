import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { TableType, HeaderType } from './models/BaseTableModel';

const BaseTable = (props: { tableData: TableType }): JSX.Element => {
  {
    return (
      <TableContainer
        sx={{ justifyContent: 'center', display: 'flex', minWidth: 800 }}>
        <Table component={Paper} sx={{ margin: '4rem' }}>
          <TableHead>
            <TableRow>
              {props.tableData.headers.map(
                (header: HeaderType, index: number) => {
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
