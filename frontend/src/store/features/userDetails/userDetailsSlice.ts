import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from '../../../models/User';

export interface UserDetailsState {
  user: User | null;
  error: string | null;
}

const initialState: UserDetailsState = {
  user: null,
  error: null,
};

export const userDetailsSlice = createSlice({
  name: 'userDetails',
  initialState,
  reducers: {
    getUserDetails(state, action: PayloadAction<{ id: string; showLoader?: boolean }>) {},
    setUserDetails(state, action: PayloadAction<{ user: User }>) {
      state.user = action.payload.user;
      state.error = null;
    },
    setUserDetailsError(state, action: PayloadAction<string>) {
      state.user = null;
      state.error = action.payload;
    },
  },
});

export const { getUserDetails, setUserDetails, setUserDetailsError } = userDetailsSlice.actions;
export default userDetailsSlice.reducer;
