import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Event, EventSearchRequest } from '../../../models/Event';

export interface EventsState {
  items: Event[];
  participationCounts: Record<string, number>;
  totalPages: number;
  search: EventSearchRequest;
}

const initialState: EventsState = {
  items: [],
  participationCounts: {},
  totalPages: 0,
  search: {
    page: 1,
    pageSize: 10,
  },
};

export const eventsSlice = createSlice({
  name: 'events',
  initialState,
  reducers: {
    getEvents(state, action: PayloadAction<EventSearchRequest>) {
      state.search = action.payload;
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
    },
  },
});

export const { getEvents, setEvents } = eventsSlice.actions;

export default eventsSlice.reducer;
