import { combineReducers } from '@reduxjs/toolkit';
import communitiesReducer from './features/communities/communitiesSlice';
import eventsReducer from './features/events/eventsSlice';
import loaderReducer from './features/loader/loaderSlice';

export const rootReducer = combineReducers({
  loader: loaderReducer,
  events: eventsReducer,
  communities: communitiesReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
