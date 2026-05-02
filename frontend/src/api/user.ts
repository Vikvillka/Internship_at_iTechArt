import { User, UserRegistration } from '../models/User';
import { api } from './api';

export const userApi = {
  getUserById: async (userId: string): Promise<User> => {
    const response = await api.get<User>(`/user/get/${userId}`);
    return response.data;
  },
  registerUser: async (userRegistration: UserRegistration): Promise<User> => {
    const response = await api.post<User>('/user/register', userRegistration);
    return response.data;
  },
};
