import { Event } from './Event';

export interface Community {
  id: string;
  name: string;
  description: string;
  category: string;
  city: string;
  country: string;
  ownerId: string;
  events: Event[];
}
