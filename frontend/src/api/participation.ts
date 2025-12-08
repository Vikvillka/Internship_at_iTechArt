import { api } from './api';
import { ParticipationCount } from '../models/Participation';

export const participationApi = {
  getParticipationCounts: async (eventIds: string[]): Promise<ParticipationCount[]> => {
    const response = await api.post<ParticipationCount[]>(`/participation/event/counts`, eventIds);
    return response.data;
  },
};
