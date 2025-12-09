import { ParticipationCount } from '../models/Participation';
import { api } from './api';

export const participationApi = {
  getParticipationCounts: async (eventIds: string[]): Promise<ParticipationCount[]> => {
    const response = await api.post<ParticipationCount[]>(`/participation/event/counts`, eventIds);
    return response.data;
  },
};
