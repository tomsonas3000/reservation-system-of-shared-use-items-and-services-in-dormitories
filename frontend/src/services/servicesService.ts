import { ServiceType } from '../components/services/types/ServiceType';
import { DOTNET_API } from './api';

export class ServicesService {
  static async getServices() {
    return await DOTNET_API().get('/services');
  }

  static async getServiceTypes() {
    return await DOTNET_API().get('/service-types');
  }

  static async createService(request: ServiceType) {
    return await DOTNET_API().post('/services', request);
  }
}
