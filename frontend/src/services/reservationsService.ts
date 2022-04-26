import { CreateReservation } from '../components/reservations/types/CreateReservation';
import { UpdateReservation } from '../components/reservations/types/UpdateReservation';
import { DOTNET_API } from './api';

export class ReservationsService {
  static async getReservations() {
    return await DOTNET_API().get('/reservations');
  }
  static async getDormitoryReservations(dormitoryId: string) {
    return await DOTNET_API().get(`/dormitory-reservations/${dormitoryId}`);
  }
  static async getUserReservations(userId: string) {
    return await DOTNET_API().get(`/user-reservations/${userId}`);
  }
  static async getServiceReservations(serviceId: string) {
    return await DOTNET_API().get(`/service-reservations/${serviceId}`);
  }

  static async getReservationsCalendar() {
    return await DOTNET_API().get('reservations/calendar');
  }

  static async createReservation(request: CreateReservation) {
    return await DOTNET_API().post('reservations', request);
  }

  static async updateReservation(
    request: UpdateReservation,
    reservationId: string
  ) {
    return await DOTNET_API().put(`/reservations/${reservationId}`, request);
  }

  static async deleteReservation(reservationId: string) {
    return await DOTNET_API().delete(`reservations/${reservationId}`);
  }
}
