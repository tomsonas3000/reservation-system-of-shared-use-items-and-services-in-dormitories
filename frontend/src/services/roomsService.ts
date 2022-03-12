import { DOTNET_API } from './api';

export class RoomsService {
  static async getRooms() {
    return await DOTNET_API().get('/rooms');
  }
}
