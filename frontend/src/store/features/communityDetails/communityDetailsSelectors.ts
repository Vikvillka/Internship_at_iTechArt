import { RootState } from '../../rootReducer';

export const selectCommunityDetails = (state: RootState) => state.communityDetails.community;

export const selectCommunitySubscriptionCounts = (state: RootState) =>
  state.communityDetails.subscriptionsCount;

export const selectCommunityDetailsError = (state: RootState) => state.communityDetails.error;
