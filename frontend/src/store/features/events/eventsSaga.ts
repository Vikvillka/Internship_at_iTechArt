import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { eventsApi } from '../../../api/event';
import { participationApi } from '../../../api/participation';
import { PagedResponse } from '../../../models/Common';
import { Event, EventSearchRequest } from '../../../models/Event';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import {
  getEventByCommunityId,
  getEventById,
  getEvents,
  setEventByCommunityId,
  setEventById,
  setEvents,
  setEventsError,
} from './eventsSlice';

function* fetchEvents(action: { payload: EventSearchRequest }): SagaIterator {
  try {
    yield put(showLoader());

    const events: PagedResponse<Event> = yield call(eventsApi.getEventsBySearch, action.payload);
    const eventIds = events.items.map((event) => event.id);
    const counts = yield call(participationApi.getParticipationCounts, eventIds);

    const participantCounts: Record<string, number> = {};
    counts.forEach((count: { eventId: string; count: number }) => {
      participantCounts[count.eventId] = count.count;
    });

    yield put(
      setEvents({
        items: events.items,
        participationCounts: participantCounts,
        totalPages: Math.ceil(events.totalCount / events.pageSize),
      }),
    );
  } catch (error: any) {
    yield put(setEventsError(error.message || 'Failed to fetch events'));
  } finally {
    yield put(hideLoader());
  }
}

function* fetchEventById(action: { payload: { id: string } }): SagaIterator {
  try {
    yield put(showLoader());

    const event: Event = yield call(eventsApi.getEventById, action.payload.id);
    const counts = yield call(participationApi.getParticipationCounts, [action.payload.id]);
    const participantCount = counts.length > 0 ? counts[0].count : 0;

    yield put(
      setEventById({
        event,
        participantCount,
      }),
    );
  } catch (error: any) {
    yield put(setEventsError(error.message || 'Failed to fetch event details'));
  } finally {
    yield put(hideLoader());
  }
}

function* fetchEventByCommunityId(action: {
  payload: { communityId: string; showLoader?: boolean };
}): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(showLoader());

    const events: Event[] = yield call(
      eventsApi.getEventsByCommunityId,
      action.payload.communityId,
    );
    const eventIds = events.map((event) => event.id);
    const counts = yield call(participationApi.getParticipationCounts, eventIds);

    const participantCounts: Record<string, number> = {};
    counts.forEach((count: { eventId: string; count: number }) => {
      participantCounts[count.eventId] = count.count;
    });

    yield put(
      setEventByCommunityId({
        items: events,
        participationCounts: participantCounts,
      }),
    );
  } catch (error: any) {
    console.error('Failed to fetch events by community ID:', error);
    yield put(setEventsError(error.message || 'Failed to fetch events by community ID'));
  } finally {
    if (action.payload.showLoader) yield put(hideLoader());
  }
}

export function* eventsSaga() {
  yield takeLatest(getEvents, fetchEvents);
  yield takeLatest(getEventById, fetchEventById);
  yield takeLatest(getEventByCommunityId, fetchEventByCommunityId);
}
