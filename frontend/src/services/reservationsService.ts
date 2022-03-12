import { DOTNET_API } from './api';

export class ReservationsService {
  static async getReservations() {
    return await DOTNET_API().get('/reservations');
  }
}
