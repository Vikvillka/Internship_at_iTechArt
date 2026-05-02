import '@testing-library/jest-dom';
import { fireEvent, render, screen } from '@testing-library/react';
import React from 'react';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';
import { rootReducer } from '../../../store/rootReducer';
import { joinEventParticipation } from '../../../store/features/participation/participationSlice';
import EventParticipationActionBar from './EventParticipationActionBar';

const createStore = (preloadedState: Partial<ReturnType<typeof rootReducer>>) =>
  configureStore({
    reducer: rootReducer,
    preloadedState: preloadedState as ReturnType<typeof rootReducer>,
  });

const event = {
  id: 'event-1',
  title: 'Frontend Meetup',
  description: 'Meetup',
  eventDate: '2026-05-03T17:00:00.000Z',
  maxParticipants: 100,
  venue: 'Main Hall',
  address: 'Main St 1',
  status: 'Planned',
  duration: 120,
  imagePath: '',
  latitude: null,
  longitude: null,
  communityName: 'Community Hub',
  communityId: 'community-1',
  tags: [],
};

describe('EventParticipationActionBar', () => {
  it('renders for authenticated users', () => {
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

    render(
      <Provider store={store}>
        <EventParticipationActionBar event={event} participantCount={5} />
      </Provider>,
    );

    expect(screen.getByText('Frontend Meetup')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Attend' })).toBeInTheDocument();
  });

  it('does not render for unauthenticated users', () => {
    const store = createStore({
      auth: {
        accessToken: null,
        refreshToken: null,
        user: null,
        error: null,
      },
      participation: {
        items: [],
        error: null,
        isLoading: false,
        isMutationLoading: false,
      },
    });

    render(
      <Provider store={store}>
        <EventParticipationActionBar event={event} participantCount={5} />
      </Provider>,
    );

    expect(screen.queryByRole('button', { name: 'Attend' })).not.toBeInTheDocument();
  });

  it('dispatches join action when attend is clicked', () => {
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
        <EventParticipationActionBar event={event} participantCount={5} />
      </Provider>,
    );

    fireEvent.click(screen.getByRole('button', { name: 'Attend' }));

    expect(dispatchSpy).toHaveBeenCalledWith(
      joinEventParticipation({
        userId: 'user-1',
        eventId: 'event-1',
      }),
    );
  });
});
