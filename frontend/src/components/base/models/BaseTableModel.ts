export interface TableType {
  headers: HeaderType[];
  rows: unknown[];
}

export interface HeaderType {
  columnName: string;
  friendlyName: string;
}
