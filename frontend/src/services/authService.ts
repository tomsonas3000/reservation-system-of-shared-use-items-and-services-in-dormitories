import Cookies from 'universal-cookie';
import { LoginType } from '../components/auth/types/LoginType';
import { DOTNET_API } from './api';

export class AuthService {
  static async login(request: LoginType) {
    const cookies = new Cookies();

    const res = await DOTNET_API().post('/auth/login', request);
    cookies.set('jwt', res?.data?.token, {
      maxAge: 360000000,
    });
    cookies.set('role', res?.data?.role, {
      maxAge: 360000000,
    });
    cookies.set('dormitoryId', res?.data?.dormitoryId, {
      maxAge: 360000000,
    });
    return res;
  }
}
