import { TagApiModel } from './Tag';

export interface Event {
  id: string;
  title: string;
  description: string;
  eventDate: string;
  maxParticipants: number;
  venue: string;
  address: string;
  status: string;
  duration: number;
  imagePath: string;
  latitude: number | null;
  longitude: number | null;
  communityName: string;
  communityId: string;
  tags: TagApiModel[];
}

export interface EventSearchRequest {
  keywords?: string;
  location?: string;
  category?: string;
  dateFrom?: string;
  dateTo?: string;
  page: number;
  pageSize: number;
}
