import { DOTNET_API } from './api';

export class UsersService {
  static async getUsers() {
    return await DOTNET_API().get('/users');
  }
}
