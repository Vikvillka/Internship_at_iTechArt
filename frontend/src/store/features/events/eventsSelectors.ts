import { RootState } from '../../rootReducer';

export const selectEvents = (state: RootState) => state.events.items;

export const selectEventSearch = (state: RootState) => state.events.search;

export const selectParticipationCounts = (state: RootState) => state.events.participationCounts;

export const selectTotalEventPages = (state: RootState) => state.events.totalPages;

export const selectEventById = (state: RootState) => state.events.selectedEvent;

export const selectEventsError = (state: RootState) => state.events.error;

export const selectEventsByCommunityId = (state: RootState) => state.events.items;
