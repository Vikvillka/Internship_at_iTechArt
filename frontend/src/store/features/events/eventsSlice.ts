import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Event, EventSearchRequest } from '../../../models/Event';

export interface EventsState {
  items: Event[];
  selectedEvent: Event | null;
  participationCounts: Record<string, number>;
  totalPages: number;
  search: EventSearchRequest;
  error: string | null;
}

const initialState: EventsState = {
  items: [],
  selectedEvent: null,
  participationCounts: {},
  totalPages: 0,
  search: {
    page: 1,
    pageSize: 10,
  },
  error: null,
};

export const eventsSlice = createSlice({
  name: 'events',
  initialState,
  reducers: {
    getEvents(state, action: PayloadAction<EventSearchRequest>) {
      state.search = action.payload;
      state.error = null;
    },
    setEvents(
      state,
      action: PayloadAction<{
        items: Event[];
        participationCounts: Record<string, number>;
        totalPages: number;
      }>,
    ) {
      state.items = action.payload.items;
      state.participationCounts = action.payload.participationCounts;
      state.totalPages = action.payload.totalPages;
      state.error = null;
    },
    getEventById(state, action: PayloadAction<{ id: string; showLoader?: boolean }>) {
      state.selectedEvent = null;
      state.error = null;
    },
    setEventById(
      state,
      action: PayloadAction<{
        event: Event;
        participantCount?: number;
      }>,
    ) {
      state.selectedEvent = action.payload.event;
      if (action.payload.participantCount !== undefined) {
        state.participationCounts[action.payload.event.id] = action.payload.participantCount;
      }
      state.error = null;
    },
    getEventByCommunityId(
      state,
      action: PayloadAction<{ communityId: string; showLoader?: boolean }>,
    ) {
      state.items = [];
      state.participationCounts = {};
      state.error = null;
    },
    setEventByCommunityId(
      state,
      action: PayloadAction<{
        items: Event[];
        participationCounts: Record<string, number>;
      }>,
    ) {
      state.items = action.payload.items;
      state.participationCounts = action.payload.participationCounts;
      state.error = null;
    },
    setEventsError(state, action: PayloadAction<string>) {
      state.error = action.payload;
      state.items = [];
      state.selectedEvent = null;
      state.participationCounts = {};
      state.totalPages = 0;
    },
  },
});

export const {
  getEvents,
  setEvents,
  getEventById,
  setEventById,
  getEventByCommunityId,
  setEventByCommunityId,
  setEventsError,
} = eventsSlice.actions;

export default eventsSlice.reducer;
