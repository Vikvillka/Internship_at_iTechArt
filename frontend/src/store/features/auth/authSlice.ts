import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthCreds, AuthTokens } from '../../../models/Auth';

export interface AuthState {
  accessToken: string | null;
  refreshToken: string | null;
  error: string | null;
}

const initialState: AuthState = {
  accessToken: localStorage.getItem('accessToken'),
  refreshToken: localStorage.getItem('refreshToken'),
  error: null,
};

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    login(state, action: PayloadAction<AuthCreds>) {
      state.error = null;
    },
    setTokens(state, action: PayloadAction<AuthTokens>) {
      state.accessToken = action.payload.accessToken;
      state.refreshToken = action.payload.refreshToken;
      state.error = null;
    },
    logout(state) {
      state.accessToken = null;
      state.refreshToken = null;
      state.error = null;
    },
    setAuthError(state, action: PayloadAction<string>) {
      state.error = action.payload;
    },
  },
});

export const { login, setTokens, logout, setAuthError } = authSlice.actions;

export default authSlice.reducer;
