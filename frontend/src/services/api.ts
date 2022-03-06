import axios from 'axios';
import Cookies from 'universal-cookie';

export const DOTNET_API = () => {
  const cookies = new Cookies();

  return axios.create({
    baseURL: 'http://localhost:5000/',
    headers: {
      Authorization: `Bearer ${cookies.get('jwt')}`,
    },
  });
};
