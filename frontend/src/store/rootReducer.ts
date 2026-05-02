import { combineReducers } from '@reduxjs/toolkit';
import authReducer from './features/auth/authSlice';
import communitiesReducer from './features/communities/communitiesSlice';
import eventsReducer from './features/events/eventsSlice';
import loaderReducer from './features/loader/loaderSlice';
import participationReducer from './features/participation/participationSlice';
import uiReduser from './features/ui/uiSlice';
import userReducer from './features/users/usersSlice';

export const rootReducer = combineReducers({
  loader: loaderReducer,
  events: eventsReducer,
  participation: participationReducer,
  communities: communitiesReducer,
  users: userReducer,
  auth: authReducer,
  ui: uiReduser,
});

export type RootState = ReturnType<typeof rootReducer>;
