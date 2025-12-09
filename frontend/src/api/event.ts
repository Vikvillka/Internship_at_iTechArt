import { Event } from '../models/Event';
import { api } from './api';

export const eventsApi = {
  getEvents: async (): Promise<Event[]> => {
    const response = await api.get<Event[]>('/event/getAll');
    return response.data;
  },
  getEventById: async (eventId: string): Promise<Event> => {
    const response = await api.get<Event>(`/event/get/${eventId}`);
    return response.data;
  },
};
