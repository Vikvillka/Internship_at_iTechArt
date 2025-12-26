import axios from 'axios';
import { AuthTokens } from '../models/Auth';
import { logout, setTokens } from '../store/features/auth/authSlice';
import { store } from '../store/index';
import { authApi } from './auth';

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL ?? 'https://localhost:5000',
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('authToken');
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      const refreshToken = store.getState().auth.refreshToken;
      if (refreshToken) {
        try {
          const tokens = await authApi.refresh(refreshToken);
          store.dispatch(
            setTokens({
              accessToken: tokens.accessToken,
              refreshToken: tokens.refreshToken,
            } as AuthTokens),
          );
          error.config.headers['Authorization'] = `Bearer ${tokens.accessToken}`;
          return api.request(error.config);
        } catch {
          store.dispatch(logout());
        }
      } else {
        store.dispatch(logout());
      }
    }
    const customError = {
      message: error.response?.data.detail || error.message || 'An unknown error occurred',
      status: error.response?.status || 500,
    };
    return Promise.reject(customError);
  },
);

export { api };
