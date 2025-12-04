import { TagApiModel } from "./Tag";

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
  tags: TagApiModel[];
}
