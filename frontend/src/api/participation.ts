import { api } from './api';
import { ParticipationCount } from '../models/Participation';

export const participationApi = {
  getParticipationCounts: async (eventId: string): Promise<ParticipationCount[]> => {
    const response = await api.get<ParticipationCount[]>(`/participation/event/${eventId}/count`);
    return response.data;
  },
};