import { DOTNET_API } from './api';

export class DormitoriesService {
  static async getDormitories() {
    return await DOTNET_API().get('/dormitories');
  }

  static async getDormitoriesLookupList() {
    return await DOTNET_API().get('/dormitories-lookup');
  }
}
