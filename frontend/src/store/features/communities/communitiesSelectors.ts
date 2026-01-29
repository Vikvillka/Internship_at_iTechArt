import { RootState } from '../../rootReducer';

export const selectCommunities = (state: RootState) => state.communities.items;

export const selectCommunitySubscriptionCounts = (state: RootState) =>
  state.communities.subscriptionCounts;

export const selectCommunityTotalPages = (state: RootState) => state.communities.totalPages;

export const selectCommunitiesError = (state: RootState) => state.communities.error;

export const selectCommunityById = (state: RootState) => state.communities.selectedCommunity;
