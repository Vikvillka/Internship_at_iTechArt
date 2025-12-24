import { RootState } from '../../rootReducer';

export const selectAccessToken = (state: RootState) => state.auth.accessToken;

export const selectRefreshToken = (state: RootState) => state.auth.refreshToken;

export const selectAuthError = (state: RootState) => state.auth.error;

export const selectIsLoggedIn = (state: RootState) => !!state.auth.accessToken;
