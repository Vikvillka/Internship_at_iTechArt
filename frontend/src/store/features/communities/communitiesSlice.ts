import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Community, CommunitySearchRequest } from '../../../models/Community';

export interface CommunitiesState {
  items: Community[];
  selectedCommunity: Community | null;
  subscriptionCounts: Record<string, number>;
  totalPages: number;
  search: CommunitySearchRequest;
  error: string | null;
}

const initialState: CommunitiesState = {
  items: [],
  selectedCommunity: null,
  subscriptionCounts: {},
  totalPages: 0,
  search: {
    page: 1,
    pageSize: 20,
  },
  error: null,
};

export const communitiesSlice = createSlice({
  name: 'communities',
  initialState,
  reducers: {
    getCommunities(state, action: PayloadAction<CommunitySearchRequest>) {
      state.search = action.payload;
      state.error = null;
    },
    setCommunities(
      state,
      action: PayloadAction<{
        items: Community[];
        subscriptionCounts: Record<string, number>;
        totalPages: number;
      }>,
    ) {
      state.items = action.payload.items;
      state.subscriptionCounts = action.payload.subscriptionCounts;
      state.totalPages = action.payload.totalPages;
      state.error = null;
    },
    getCommunityById(state, action: PayloadAction<{ id: string; showLoader?: boolean }>) {
      state.selectedCommunity = null;
      state.error = null;
    },
    setCommunityById(
      state,
      action: PayloadAction<{
        community: Community;
        subscriptionCount?: number;
      }>,
    ) {
      state.selectedCommunity = action.payload.community;
      if (action.payload.subscriptionCount !== undefined) {
        state.subscriptionCounts[action.payload.community.id] = action.payload.subscriptionCount;
      }
      state.error = null;
    },
    setCommunitiesError(state, action: PayloadAction<string>) {
      state.error = action.payload;
      state.items = [];
      state.selectedCommunity = null;
      state.subscriptionCounts = {};
      state.totalPages = 0;
    },
    getUserCommunities(state, action: PayloadAction<{ userId: string }>) {
      state.error = null;
    },
    setUserCommunities(
      state,
      action: PayloadAction<{
        communities: Community[];
        subscriptionCounts: Record<string, number>;
      }>,
    ) {
      state.items = action.payload.communities;
      state.subscriptionCounts = action.payload.subscriptionCounts;
      state.error = null;
    },
  },
});

export const {
  getCommunities,
  setCommunities,
  getCommunityById,
  setCommunityById,
  setCommunitiesError,
  getUserCommunities,
  setUserCommunities,
} = communitiesSlice.actions;

export default communitiesSlice.reducer;
