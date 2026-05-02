import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Participation, ParticipationRequest } from '../../../models/Participation';

export interface ParticipationState {
  items: Participation[];
  error: string | null;
  isLoading: boolean;
  isMutationLoading: boolean;
}

const initialState: ParticipationState = {
  items: [],
  error: null,
  isLoading: false,
  isMutationLoading: false,
};

export const participationSlice = createSlice({
  name: 'participation',
  initialState,
  reducers: {
    getUserParticipations(state, _action: PayloadAction<{ userId: string }>) {
      state.isLoading = true;
      state.error = null;
    },
    setUserParticipations(state, action: PayloadAction<Participation[]>) {
      state.items = action.payload;
      state.isLoading = false;
      state.error = null;
    },
    joinEventParticipation(state, _action: PayloadAction<ParticipationRequest>) {
      state.isMutationLoading = true;
      state.error = null;
    },
    setJoinEventParticipationSuccess(state, action: PayloadAction<Participation>) {
      const existing = state.items.find((item) => item.eventId === action.payload.eventId);

      if (existing) {
        existing.id = action.payload.id;
        existing.eventName = action.payload.eventName;
        existing.isConfirmed = action.payload.isConfirmed;
      } else {
        state.items.push(action.payload);
      }

      state.isMutationLoading = false;
      state.error = null;
    },
    cancelEventParticipation(state, _action: PayloadAction<ParticipationRequest>) {
      state.isMutationLoading = true;
      state.error = null;
    },
    setCancelEventParticipationSuccess(state, action: PayloadAction<{ eventId: string }>) {
      state.items = state.items.filter((item) => item.eventId !== action.payload.eventId);
      state.isMutationLoading = false;
      state.error = null;
    },
    setParticipationError(state, action: PayloadAction<string>) {
      state.error = action.payload;
      state.isLoading = false;
      state.isMutationLoading = false;
    },
  },
});

export const {
  getUserParticipations,
  setUserParticipations,
  joinEventParticipation,
  setJoinEventParticipationSuccess,
  cancelEventParticipation,
  setCancelEventParticipationSuccess,
  setParticipationError,
} = participationSlice.actions;

export default participationSlice.reducer;
