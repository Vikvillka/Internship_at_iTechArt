import { AuthCreds, AuthTokens } from '../models/Auth';
import { api } from './api';

export const authApi = {
  getTokens: async (creds: AuthCreds): Promise<AuthTokens> => {
    const response = await api.post<AuthTokens>('/auth/getTokens', creds);
    return response.data;
  },

  refresh: async (refreshToken: string): Promise<AuthTokens> => {
    const response = await api.post<AuthTokens>('/auth/refresh', { refreshToken });
    return response.data;
  },
};
