import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { eventsApi } from '../../../api/event';
import { participationApi } from '../../../api/participation';
import { PagedResponse } from '../../../models/Common';
import { Event, EventSearchRequest } from '../../../models/Event';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { getEvents, setEvents } from './eventsSlice';

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
  } catch (error) {
    console.error('Failed to fetch events:', error);
  } finally {
    yield put(hideLoader());
  }
}

export function* eventsSaga() {
  yield takeLatest(getEvents, fetchEvents);
}
