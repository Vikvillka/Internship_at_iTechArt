export interface ParticipationCount {
  eventId: string;
  count: number;
}

export interface ParticipationRequest {
  userId: string;
  eventId: string;
}

export interface Participation {
  id: string;
  userId: string;
  eventId: string;
  eventName: string;
  isConfirmed: boolean;
}
