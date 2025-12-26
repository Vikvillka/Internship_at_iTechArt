import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthCreds, AuthTokens } from '../../../models/Auth';
import { User } from '../../../models/User';

export interface AuthState {
  accessToken: string | null;
  refreshToken: string | null;
  user: User | null;
  error: string | null;
}

const initialState: AuthState = {
  accessToken: localStorage.getItem('accessToken'),
  refreshToken: localStorage.getItem('refreshToken'),
  user: null,
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
    setUser(state, action: PayloadAction<User>) {
      state.user = action.payload;
    },
    clearUser(state) {
      state.user = null;
    },
  },
});

export const { login, setTokens, logout, setAuthError, setUser, clearUser } = authSlice.actions;

export default authSlice.reducer;
