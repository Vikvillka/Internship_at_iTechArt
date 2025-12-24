import { RootState } from '../../rootReducer';

export const selectUserById = (state: RootState) => state.users.user;

export const selectUserError = (state: RootState) => state.users.error;
