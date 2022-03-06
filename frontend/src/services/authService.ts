import Cookies from 'universal-cookie';
import { LoginType } from '../components/auth/types/LoginType';
import { DOTNET_API } from './api';

export class AuthService {
  static async login(request: LoginType) {
    const cookies = new Cookies();

    const res = await DOTNET_API().post('/auth/login', request);
    cookies.set('jwt', res?.data?.token, {
      maxAge: 3600,
    });
    cookies.set('role', res?.data?.role, {
      maxAge: 3600,
    });
    return res;
  }
}
