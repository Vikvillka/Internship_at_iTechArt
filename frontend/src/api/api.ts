import axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { AuthTokens } from '../models/Auth';
import { logout, setTokens } from '../store/features/auth/authSlice';
import { store } from '../store/index';
import { authApi } from './auth';

interface RetryableRequest extends AxiosRequestConfig {
  _retry?: boolean;
}

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL ?? 'https://localhost:5000',
});

api.interceptors.request.use((config) => {
  const token = store.getState().auth.accessToken;
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  async (error: AxiosError<any>) => {
    const originalRequest = error.config as RetryableRequest;
    const status = error.response?.status;
    const errorInstance = error.response?.data.instance;

    const customError = {
      message: error.response?.data.detail || error.message || 'An unknown error occurred',
      status: error.response?.status || 500,
    };

    if (
      status === 401 &&
      !originalRequest._retry &&
      errorInstance !== '/AuthService/RefreshToken'
    ) {
      originalRequest._retry = true;

      const refreshToken = store.getState().auth.refreshToken;

      if (!refreshToken) {
        store.dispatch(logout());
        return Promise.reject(customError);
      }
      try {
        const tokens = await authApi.refresh(refreshToken);
        store.dispatch(
          setTokens({
            accessToken: tokens.accessToken,
            refreshToken: tokens.refreshToken,
          } as AuthTokens),
        );

        originalRequest.headers = {
          ...originalRequest.headers,
          Authorization: `Bearer ${tokens.accessToken}`,
        };

        return api.request(originalRequest);
      } catch {
        store.dispatch(logout());
        return Promise.reject(customError);
      }
    }
    return Promise.reject(customError);
  },
);

export { api };
