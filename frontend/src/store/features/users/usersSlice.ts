import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User, UserRegistration } from '../../../models/User';

export interface UserDetailsState {
  user: User | null;
  error: string | null;
  registrationSuccess: boolean;
}

const initialState: UserDetailsState = {
  user: null,
  error: null,
  registrationSuccess: false,
};

export const userDetailsSlice = createSlice({
  name: 'userDetails',
  initialState,
  reducers: {
    getUserById(state, action: PayloadAction<{ id: string; showLoader?: boolean }>) {
      state.user = null;
      state.error = null;
    },
    setUserById(state, action: PayloadAction<{ user: User }>) {
      state.user = action.payload.user;
      state.error = null;
    },
    setUserError(state, action: PayloadAction<string>) {
      state.user = null;
      state.error = action.payload;
    },
    registerUser(
      state,
      action: PayloadAction<{ userRegistration: UserRegistration; showLoader?: boolean }>,
    ) {
      state.error = null;
      state.registrationSuccess = false;
    },
    registerUserSuccess(state) {
      state.error = null;
      state.registrationSuccess = true;
    },
    resetRegistrationSuccess(state) {
      state.registrationSuccess = false;
    },
  },
});

export const {
  getUserById,
  setUserById,
  setUserError,
  registerUser,
  registerUserSuccess,
  resetRegistrationSuccess,
} = userDetailsSlice.actions;
export default userDetailsSlice.reducer;
