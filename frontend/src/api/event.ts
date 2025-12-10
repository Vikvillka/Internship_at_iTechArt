import { PagedResponse } from '../models/Common';
import { Event, EventSearchRequest } from '../models/Event';
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
  searchEvents: async (searchParams: EventSearchRequest): Promise<PagedResponse<Event>> => {
    const response = await api.post('/event/search', searchParams);
    return response.data;
  },
};
