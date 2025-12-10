import { Community } from '../models/Community';
import { api } from './api';

export const communityApi = {
  getCommunities: async (): Promise<Community[]> => {
    const response = await api.get<Community[]>('/community/getAll');
    return response.data;
  },
  getCommunityById: async (communityId: string): Promise<Community> => {
    const response = await api.get<Community>(`/community/get/${communityId}`);
    return response.data;
  },
};
