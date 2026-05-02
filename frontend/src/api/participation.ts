import { Participation, ParticipationCount, ParticipationRequest } from '../models/Participation';
import { api } from './api';

export const participationApi = {
  getParticipationCounts: async (eventIds: string[]): Promise<ParticipationCount[]> => {
    const response = await api.post<ParticipationCount[]>(`/participation/event/counts`, eventIds);
    return response.data;
  },
  getUserParticipations: async (userId: string): Promise<Participation[]> => {
    const response = await api.get<Participation[]>(`/participation/user/${userId}`);
    return response.data;
  },
  participate: async (request: ParticipationRequest): Promise<Participation> => {
    const response = await api.post<Participation>('/participation/participate', request);
    return response.data;
  },
  cancelParticipation: async (request: ParticipationRequest): Promise<void> => {
    await api.post('/participation/cancel', request);
  },
};
