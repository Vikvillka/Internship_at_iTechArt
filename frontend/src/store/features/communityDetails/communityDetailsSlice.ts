import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Community } from '../../../models/Community';

export interface CommunityDetailsState {
  community: Community | null;
  subscriptionsCount: number;
  error: string | null;
}

const initialState: CommunityDetailsState = {
  community: null,
  subscriptionsCount: 0,
  error: null,
};

export const communityDetailsSlice = createSlice({
  name: 'communityDetails',
  initialState,
  reducers: {
    getCommunityDetails(state, action: PayloadAction<{ id: string; showLoader?: boolean }>) {},
    setCommunityDetails(
      state,
      action: PayloadAction<{ community: Community; subscriptionsCount?: number }>,
    ) {
      state.community = action.payload.community;
      state.subscriptionsCount = action.payload.subscriptionsCount || 0;
      state.error = null;
    },
    setCommunityDetailsError(state, action: PayloadAction<string>) {
      state.error = action.payload;
      state.community = null;
      state.subscriptionsCount = 0;
    },
  },
});

export const { getCommunityDetails, setCommunityDetails, setCommunityDetailsError } =
  communityDetailsSlice.actions;
export default communityDetailsSlice.reducer;
