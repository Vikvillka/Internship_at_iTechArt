import '@testing-library/jest-dom';
import { render, screen } from '@testing-library/react';
import React from 'react';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';
import { rootReducer } from '../../store/rootReducer';
import CommunityDetailsPage from './CommunityDetailsPage';

jest.mock(
  'react-router-dom',
  () => ({
    useParams: () => ({ communityId: 'community-1' }),
  }),
  { virtual: true },
);

jest.mock('../../components/community/communityDetails/CommunityDetailsHeader', () => () => (
  <div>Community Header</div>
));

jest.mock('../../containers/community/CommunityDetailsInfoContainer', () => () => (
  <div>Community Info</div>
));

jest.mock('../../containers/community/CommunityDetailsEventsListContainer', () => () => (
  <div>Community Events</div>
));

const createStore = (preloadedState: Partial<ReturnType<typeof rootReducer>>) =>
  configureStore({
    reducer: rootReducer,
    preloadedState: preloadedState as ReturnType<typeof rootReducer>,
  });

describe('CommunityDetailsPage', () => {
  it('keeps rendered community content visible during follow-up loading', () => {
    const store = createStore({
      loader: { counter: 1 },
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

    render(
      <Provider store={store}>
        <CommunityDetailsPage />
      </Provider>,
    );

    expect(screen.getByText('Community Header')).toBeInTheDocument();
    expect(screen.getByText('Community Info')).toBeInTheDocument();
    expect(screen.getByText('Community Events')).toBeInTheDocument();
    expect(screen.queryByRole('progressbar')).not.toBeInTheDocument();
  });

  it('shows the blocking loader before the community is available', () => {
    const store = createStore({
      loader: { counter: 1 },
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

    render(
      <Provider store={store}>
        <CommunityDetailsPage />
      </Provider>,
    );

    expect(screen.getByRole('progressbar')).toBeInTheDocument();
  });
});