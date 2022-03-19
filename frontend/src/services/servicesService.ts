import { ServiceType } from '../components/services/types/ServiceType';
import { DOTNET_API } from './api';

export class ServicesService {
  static async getServices() {
    return await DOTNET_API().get('/services');
  }

  static async getService(serviceId: string) {
    return await DOTNET_API().get(`/services/${serviceId}`);
  }

  static async getServiceTypes() {
    return await DOTNET_API().get('/service-types');
  }

  static async createService(request: ServiceType) {
    return await DOTNET_API().post('/services', request);
  }

  static async updateService(
    serviceId: string | undefined,
    request: ServiceType
  ) {
    return await DOTNET_API().put(`/services/${serviceId}`, request);
  }
}
