import '@testing-library/jest-dom';
import { render, screen, waitFor } from '@testing-library/react';
import React from 'react';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';
import { rootReducer } from '../../../store/rootReducer';
import { getUserParticipations } from '../../../store/features/participation/participationSlice';
import UserProfileContainer from './UserProfileContainer';

const createStore = (preloadedState: Partial<ReturnType<typeof rootReducer>>) =>
  configureStore({
    reducer: rootReducer,
    preloadedState: preloadedState as ReturnType<typeof rootReducer>,
  });

describe('UserProfileContainer', () => {
  it('fetches participations for active user', async () => {
    const store = createStore({
      auth: {
        accessToken: 'token',
        refreshToken: 'refresh',
        user: {
          id: 'user-1',
          username: 'john',
          email: 'john@mail.com',
          gender: 0,
          city: 'City',
          country: 'Country',
        },
        error: null,
      },
      participation: {
        items: [],
        error: null,
        isLoading: false,
        isMutationLoading: false,
      },
    });

    const dispatchSpy = jest.spyOn(store, 'dispatch');

    render(
      <Provider store={store}>
        <UserProfileContainer />
      </Provider>,
    );

    await waitFor(() => {
      expect(dispatchSpy).toHaveBeenCalledWith(getUserParticipations({ userId: 'user-1' }));
    });
  });

  it('renders joined events from participation state', () => {
    const store = createStore({
      auth: {
        accessToken: 'token',
        refreshToken: 'refresh',
        user: null,
        error: null,
      },
      participation: {
        items: [
          {
            id: 'p-1',
            userId: 'user-1',
            eventId: 'event-1',
            eventName: 'Frontend Meetup',
            isConfirmed: true,
          },
        ],
        error: null,
        isLoading: false,
        isMutationLoading: false,
      },
    });

    render(
      <Provider store={store}>
        <UserProfileContainer />
      </Provider>,
    );

    expect(screen.getByText('Joined events')).toBeInTheDocument();
    expect(screen.getByText('Frontend Meetup')).toBeInTheDocument();
  });
});
