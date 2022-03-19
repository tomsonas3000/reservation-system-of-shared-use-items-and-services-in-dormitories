import { LookupType } from '../../base/types/LookupType';

export interface ServiceDetailsType {
  id: string;
  type: string;
  maxTimeOfUse: number;
  maxAmountOfUsers: number;
  roomId: string;
  dormitoryId: string;
}
