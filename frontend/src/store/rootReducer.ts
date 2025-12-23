import { combineReducers } from '@reduxjs/toolkit';
import communitiesReducer from './features/communities/communitiesSlice';
import eventsReducer from './features/events/eventsSlice';
import loaderReducer from './features/loader/loaderSlice';
import uiReduser from './features/ui/uiSlice';
import userDetailsReducer from './features/users/usersSlice';

export const rootReducer = combineReducers({
  loader: loaderReducer,
  events: eventsReducer,
  communities: communitiesReducer,
  userDetails: userDetailsReducer,
  ui: uiReduser,
});

export type RootState = ReturnType<typeof rootReducer>;
