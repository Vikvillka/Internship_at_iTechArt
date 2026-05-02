import '@testing-library/jest-dom';
import { render, waitFor } from '@testing-library/react';
import React from 'react';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';
import { rootReducer } from '../../store/rootReducer';
import CommunityDetailsEventsListContainer from './CommunityDetailsEventsListContainer';
import CommunityDetailsInfoContainer from './CommunityDetailsInfoContainer';

jest.mock(
  'react-router-dom',
  () => ({
    useParams: () => ({ communityId: 'community-1' }),
  }),
  { virtual: true },
);

jest.mock('../../components/community/communityDetails/CommunityDetailsInfo', () => () => (
  <div>Info Content</div>
));

jest.mock('../../components/community/communityDetails/CommunityDetailsEventsList', () => () => (
  <div>Events Content</div>
));

const createStore = (preloadedState: Partial<ReturnType<typeof rootReducer>>) =>
  configureStore({
    reducer: rootReducer,
    preloadedState: preloadedState as ReturnType<typeof rootReducer>,
  });

describe('Community detail containers', () => {
  it('requests the owner without turning on the global loader', async () => {
    const store = createStore({
      loader: { counter: 0 },
      communities: {
        items: [],
        selectedCommunity: {
          id: 'community-1',
          name: 'Chess Club',
          description: 'Play weekly matches.',
          category: 'Hobby',
          country: 'Poland',
          city: 'Warsaw',
          ownerId: 'owner-1',
          events: [],
        },
        subscriptionCounts: {},
        totalPages: 0,
        search: { page: 1, pageSize: 20 },
        error: null,
      },
      events: {
        items: [],
        selectedEvent: null,
        participationCounts: {},
        totalPages: 0,
        search: { page: 1, pageSize: 10 },
        error: null,
      },
      users: {
        user: null,
        error: null,
        registrationSuccess: false,
      },
      auth: {
        accessToken: null,
        refreshToken: null,
        user: null,
        error: null,
      },
      ui: {
        isLoginModalOpen: false,
        isRegisterModalOpen: false,
      },
    });
    const dispatchSpy = jest.spyOn(store, 'dispatch');

    render(
      <Provider store={store}>
        <CommunityDetailsInfoContainer />
      </Provider>,
    );

    await waitFor(() => {
      expect(dispatchSpy).toHaveBeenCalledWith(
        expect.objectContaining({
          type: 'userDetails/getUserById',
          payload: { id: 'owner-1' },
        }),
      );
    });
  });

  it('requests community events without turning on the global loader', async () => {
    const store = createStore({
      loader: { counter: 0 },
      communities: {
        items: [],
        selectedCommunity: null,
        subscriptionCounts: {},
        totalPages: 0,
        search: { page: 1, pageSize: 20 },
        error: null,
      },
      events: {
        items: [],
        selectedEvent: null,
        participationCounts: {},
        totalPages: 0,
        search: { page: 1, pageSize: 10 },
        error: null,
      },
      users: {
        user: null,
        error: null,
        registrationSuccess: false,
      },
      auth: {
        accessToken: null,
        refreshToken: null,
        user: null,
        error: null,
      },
      ui: {
        isLoginModalOpen: false,
        isRegisterModalOpen: false,
      },
    });
    const dispatchSpy = jest.spyOn(store, 'dispatch');

    render(
      <Provider store={store}>
        <CommunityDetailsEventsListContainer />
      </Provider>,
    );

    await waitFor(() => {
      expect(dispatchSpy).toHaveBeenCalledWith(
        expect.objectContaining({
          type: 'events/getEventByCommunityId',
          payload: { communityId: 'community-1' },
        }),
      );
    });
  });
});