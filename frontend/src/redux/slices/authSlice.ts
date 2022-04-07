import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import Cookies from 'universal-cookie';
import { LoginResponseType } from '../../components/auth/types/LoginResponseType';
import Role from '../../utils/enums/role';

const cookies = new Cookies();

export const authSlice = createSlice({
  name: 'auth',
  initialState: {
    isLoggedIn: cookies.get('jwt') ? true : false,
    role: cookies.get('role') ? cookies.get('role') : '',
    dormitoryId: cookies.get('dormitoryId') ? cookies.get('dormitoryId') : '',
  },
  reducers: {
    logIn: (state, action: PayloadAction<LoginResponseType>) => {
      state.role = action.payload.role;
      state.isLoggedIn = true;
      state.dormitoryId = action.payload?.dormitoryId;
      if (state.role === Role.Admin) window.location.replace('/dormitories');
      if (state.role === Role.Student)
        window.location.replace('/reservations-calendar');
    },
    logout: (state) => {
      state.role = '';
      state.isLoggedIn = false;
      state.dormitoryId = '';
      cookies.remove('jwt');
      cookies.remove('role');
      cookies.remove('dormitoryId');
    },
  },
});

export const { logIn, logout } = authSlice.actions;

export default authSlice.reducer;
