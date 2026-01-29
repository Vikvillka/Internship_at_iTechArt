import { combineReducers } from '@reduxjs/toolkit';
import communitiesReducer from './features/communities/communitiesSlice';
import eventsReducer from './features/events/eventsSlice';
import loaderReducer from './features/loader/loaderSlice';
import userDetailsReducer from './features/users/usersSlice';

export const rootReducer = combineReducers({
  loader: loaderReducer,
  events: eventsReducer,
  communities: communitiesReducer,
  userDetails: userDetailsReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
