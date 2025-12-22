import { RootState } from '../../rootReducer';

export const selectUserById = (state: RootState) => state.userDetails.user;

export const selectUserError = (state: RootState) => state.userDetails.error;
