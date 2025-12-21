import { RootState } from '../../rootReducer';

export const selectEventDetails = (state: RootState) => state.eventDetails.event;

export const selectParticipationCount = (state: RootState) => state.eventDetails.participantCount;

export const selectEventDetailsError = (state: RootState) => state.eventDetails.error;
