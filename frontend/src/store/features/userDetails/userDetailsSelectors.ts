import { RootState } from '../../rootReducer';

export const selectUserDetails = (state: RootState) => state.userDetails.user;

export const selectUserDetailsError = (state: RootState) => state.userDetails.error;
