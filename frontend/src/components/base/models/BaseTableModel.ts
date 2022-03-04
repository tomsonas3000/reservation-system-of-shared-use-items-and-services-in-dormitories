export interface TableModel {
  headers: HeaderModel[];
  rows: unknown[];
}

export interface HeaderModel {
  columnName: string;
  friendlyName: string;
}
