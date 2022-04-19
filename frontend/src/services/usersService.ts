import { UserType } from '../components/users/types/UserType';
import { DOTNET_API } from './api';

export class UsersService {
  static async getUsers() {
    return await DOTNET_API().get('/users');
  }

  static async getManagersLookupList() {
    return await DOTNET_API().get('/managers-lookup');
  }

  static async getRolesLookupList() {
    return await DOTNET_API().get('/roles-lookup');
  }

  static async getStudents(dormitoryId: string | undefined) {
    return await DOTNET_API().get(`/dormitories/${dormitoryId}/students`);
  }

  static async getUser(userId: string | undefined) {
    return await DOTNET_API().get(`users/${userId}`);
  }

  static async createUser(request: UserType) {
    return await DOTNET_API().post('/auth/create', request);
  }

  static async updateUser(userId: string | undefined, request: UserType) {
    return await DOTNET_API().put(`/users/${userId}`, request);
  }

  static async banUserFromReserving(userId: string | undefined) {
    return await DOTNET_API().post(`/users/${userId}/ban`);
  }

  static async removeReservationBan(userId: string | undefined) {
    return await DOTNET_API().post(`/users/${userId}/remove-ban`);
  }
}
