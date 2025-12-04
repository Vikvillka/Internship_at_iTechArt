import { api } from './api';
import { Event } from '../models/Event';

export const eventsApi = {
  getEvents: async (): Promise<Event[]> => {
    const response = await api.get<Event[]>('/event/getAll');
    return response.data;
  },
};
