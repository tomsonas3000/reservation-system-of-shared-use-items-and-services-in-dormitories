import { CreateDormitoryType } from '../components/dormitories/types/CreateDormitoryType';
import { DOTNET_API } from './api';

export class DormitoriesService {
  static async getDormitories() {
    return await DOTNET_API().get('/dormitories');
  }

  static async getDormitoriesLookupList() {
    return await DOTNET_API().get('/dormitories-lookup');
  }

  static async createDormitory(request: CreateDormitoryType) {
    return await DOTNET_API().post('/dormitories', request);
  }
}
