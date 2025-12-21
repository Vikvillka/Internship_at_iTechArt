import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { eventsApi } from '../../../api/event';
import { participationApi } from '../../../api/participation';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { getEventDetails, setEventDetails, setEventDetailsError } from './eventDetailsSlice';

function* fetchEventDetails(action: {
  payload: { id: string; showLoader?: boolean };
}): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(showLoader());

    const event = yield call(eventsApi.getEventById, action.payload.id);
    const count = yield call(participationApi.getParticipationCounts, [action.payload.id]);
    const participantCount = count.length > 0 ? count[0].count : 0;

    yield put(setEventDetails({ event, participantCount }));
  } catch (error: any) {
    console.error('Failed to fetch event details:', error);
    yield put(setEventDetailsError(error.message || 'Failed to fetch event details'));
  } finally {
    if (action.payload.showLoader) yield put(hideLoader());
  }
}

export function* eventDetailsSaga() {
  yield takeLatest(getEventDetails, fetchEventDetails);
}
