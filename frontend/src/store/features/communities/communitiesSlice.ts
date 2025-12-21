import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Community, CommunitySearchRequest } from '../../../models/Community';

export interface CommunitiesState {
  items: Community[];
  subscriptionCounts: Record<string, number>;
  totalPages: number;
  search: CommunitySearchRequest;
}

const initialState: CommunitiesState = {
  items: [],
  subscriptionCounts: {},
  totalPages: 0,
  search: {
    page: 1,
    pageSize: 20,
  },
};

export const communitiesSlice = createSlice({
  name: 'communities',
  initialState,
  reducers: {
    getCommunities(state, action: PayloadAction<CommunitySearchRequest>) {
      state.search = action.payload;
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
    },
  },
});

export const { getCommunities, setCommunities } = communitiesSlice.actions;
export default communitiesSlice.reducer;
