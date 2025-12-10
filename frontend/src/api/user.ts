import { User } from '../models/User';
import { api } from './api';

export const userApi = {
  getUserById: async (userId: string): Promise<User> => {
    const response = await api.get<User>(`/user/get/${userId}`);
    return response.data;
  },
};
