import axios from 'axios';
import { useDispatch, useSelector } from 'react-redux';
import Cookies from 'universal-cookie';
import { RootState } from '../redux/store';
import { logout } from '../redux/slices/authSlice';

export const DOTNET_API = () => {
  const cookies = new Cookies();

  const token = cookies.get('jwt');

  //const dispath = useDispatch();

  const httpClient = axios.create({
    baseURL: 'http://localhost:5000/',
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  httpClient.interceptors.response.use(
    (res) => {
      return res;
    },
    (err) => {
      if (err.response.status === 401) {
        if (window.location.href !== 'http://localhost:3000/sign-in') {
          //dispath(logout());
          window.location.replace('http://localhost:3000/sign-in');
        }
      }
      return err;
    }
  );

  return httpClient;
};
