import { CreateReservation } from '../components/reservations/types/CreateReservation';
import { DOTNET_API } from './api';

export class ReservationsService {
  static async getReservations() {
    return await DOTNET_API().get('/reservations');
  }

  static async getReservationsCalendar() {
    return await DOTNET_API().get('reservations/calendar');
  }

  static async createReservation(request: CreateReservation) {
    return await DOTNET_API().post('reservations', request);
  }
}
