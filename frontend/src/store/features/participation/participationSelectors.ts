import { RootState } from '../../rootReducer';

export const selectParticipations = (state: RootState) => state.participation.items;

export const selectParticipationError = (state: RootState) => state.participation.error;

export const selectParticipationIsLoading = (state: RootState) => state.participation.isLoading;

export const selectParticipationIsMutationLoading = (state: RootState) =>
  state.participation.isMutationLoading;

export const selectIsParticipatingInEvent =
  (eventId: string) => (state: RootState): boolean =>
    state.participation.items.some((item) => item.eventId === eventId && item.isConfirmed);
