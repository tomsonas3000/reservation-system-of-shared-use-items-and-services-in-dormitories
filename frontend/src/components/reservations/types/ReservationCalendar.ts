import { LookupType } from '../../base/types/LookupType';

export interface ServiceType {
  type: LookupType;
  serviceList: ServiceList[];
}

export interface ServiceList {
  id: string;
  maximumTimeOfUse: number;
  room: string;
  reservationsList: ReservationsList[];
  name: string;
}

export interface ReservationsList {
  id: string;
  startDate: Date;
  endDate: Date;
  isBookedByUser: boolean;
  title: string;
}
