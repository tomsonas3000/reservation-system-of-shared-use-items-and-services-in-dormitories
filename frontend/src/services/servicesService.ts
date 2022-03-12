import { DOTNET_API } from './api';

export class ServicesService {
  static async getServices() {
    return await DOTNET_API().get('/services');
  }
}
