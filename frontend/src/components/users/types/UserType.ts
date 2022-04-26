export interface UserType {
  id?: string;
  name: string;
  surname: string;
  telephoneNumber: string;
  emailAddress: string;
  role: string;
  dormitoryId?: string;
  password?: string;
  isBannedFromReserving?: boolean;
  hasMoreThanTenReservations?: boolean;
}
