import axios from 'axios';
import Cookies from 'universal-cookie';

export const DOTNET_API = () => {
  const cookies = new Cookies();

  const token = cookies.get('jwt');

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
          window.location.replace('http://localhost:3000/sign-in');
        }
      }
      return Promise.reject(err);
    }
  );

  return httpClient;
};
