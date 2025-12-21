import { combineReducers } from '@reduxjs/toolkit';
import communitiesReducer from './features/communities/communitiesSlice';
import communityDetailsReducer from './features/communityDetails/communityDetailsSlice';
import eventDetailsReduser from './features/eventDetails/eventDetailsSlice';
import eventsReducer from './features/events/eventsSlice';
import loaderReducer from './features/loader/loaderSlice';
import userDetailsReducer from './features/userDetails/userDetailsSlice';

export const rootReducer = combineReducers({
  loader: loaderReducer,
  events: eventsReducer,
  communities: communitiesReducer,
  communityDetails: communityDetailsReducer,
  eventDetails: eventDetailsReduser,
  userDetails: userDetailsReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
