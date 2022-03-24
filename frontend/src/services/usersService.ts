import { DOTNET_API } from './api';

export class UsersService {
  static async getUsers() {
    return await DOTNET_API().get('/users');
  }

  static async getManagersLookupList() {
    return await DOTNET_API().get('/managers-lookup');
  }

  static async getStudents(dormitoryId: string | undefined) {
    return await DOTNET_API().get(`/dormitories/${dormitoryId}/students`);
  }
}
