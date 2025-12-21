import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Event } from '../../../models/Event';

interface EventDetailsState {
  event: Event | null;
  participantCount: number;
  error: string | null;
}

const initialState: EventDetailsState = {
  event: null,
  participantCount: 0,
  error: null,
};

export const eventDetailsSlice = createSlice({
  name: 'eventDetails',
  initialState,
  reducers: {
    getEventDetails(state, action: PayloadAction<{ id: string; showLoader?: boolean }>) {},
    setEventDetails(state, action: PayloadAction<{ event: Event; participantCount?: number }>) {
      state.event = action.payload.event;
      state.participantCount = action.payload.participantCount || 0;
      state.error = null;
    },
    setEventDetailsError(state, action: PayloadAction<string>) {
      state.error = action.payload;
      state.event = null;
      state.participantCount = 0;
    },
  },
});

export const { getEventDetails, setEventDetails, setEventDetailsError } = eventDetailsSlice.actions;
export default eventDetailsSlice.reducer;
