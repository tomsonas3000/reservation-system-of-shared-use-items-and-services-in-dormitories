import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import Cookies from 'universal-cookie';
import { LoginResponseType } from '../../components/auth/types/LoginResponseType';

const cookies = new Cookies();

export const authSlice = createSlice({
  name: 'auth',
  initialState: {
    isLoggedIn: cookies.get('jwt') ? true : false,
    role: cookies.get('role') ? cookies.get('role') : '',
  },
  reducers: {
    logIn: (state, action: PayloadAction<LoginResponseType>) => {
      state.role = action.payload.role;
      state.isLoggedIn = true;
    },
  },
});

export const { logIn } = authSlice.actions;

export default authSlice.reducer;
