import { PagedResponse } from '../models/Common';
import { Community, CommunitySearchRequest } from '../models/Community';
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
  getCommunitiesBySearch: async (
    searchParams: CommunitySearchRequest,
  ): Promise<PagedResponse<Community>> => {
    const response = await api.post('/community/search', searchParams);
    return response.data;
  },
  getUserCommunities: async (userId: string): Promise<Community[]> => {
    const response = await api.get<Community[]>(`/community/getByUser/${userId}`);
    return response.data;
  },
};
