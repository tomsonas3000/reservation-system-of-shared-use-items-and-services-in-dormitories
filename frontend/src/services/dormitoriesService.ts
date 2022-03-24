import { CreateDormitoryType } from '../components/dormitories/types/CreateDormitoryType';
import { DOTNET_API } from './api';

export class DormitoriesService {
  static async getDormitories() {
    return await DOTNET_API().get('/dormitories');
  }

  static async getDormitory(dormitoryId: string) {
    return await DOTNET_API().get(`/dormitories/${dormitoryId}`);
  }

  static async getDormitoriesLookupList() {
    return await DOTNET_API().get('/dormitories-lookup');
  }

  static async createDormitory(request: CreateDormitoryType) {
    return await DOTNET_API().post('/dormitories', request);
  }

  static async updateDormitory(
    dormitoryId: string | undefined,
    request: CreateDormitoryType
  ) {
    return await DOTNET_API().put(`/dormitories/${dormitoryId}`, request);
  }

  static async updateDormitoryStudents(
    students: string[],
    dormitoryId: string | undefined
  ) {
    return await DOTNET_API().post(
      `/dormitories/${dormitoryId}/update-students`,
      students
    );
  }
}
