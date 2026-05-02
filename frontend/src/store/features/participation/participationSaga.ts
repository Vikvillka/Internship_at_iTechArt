import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { participationApi } from '../../../api/participation';
import {
  addParticipantCount,
  decrementParticipantCount,
} from '../events/eventsSlice';
import {
  cancelEventParticipation,
  getUserParticipations,
  joinEventParticipation,
  setCancelEventParticipationSuccess,
  setJoinEventParticipationSuccess,
  setParticipationError,
  setUserParticipations,
} from './participationSlice';

function* fetchUserParticipations(action: {
  payload: { userId: string };
}): SagaIterator {
  try {
    const participations = yield call(participationApi.getUserParticipations, action.payload.userId);
    yield put(setUserParticipations(participations));
  } catch (error: any) {
    yield put(setParticipationError(error.message || 'Failed to fetch user participations'));
  }
}

function* handleJoinEventParticipation(action: {
  payload: { userId: string; eventId: string };
}): SagaIterator {
  try {
    const participation = yield call(participationApi.participate, action.payload);
    yield put(setJoinEventParticipationSuccess(participation));
    yield put(addParticipantCount({ eventId: action.payload.eventId }));
  } catch (error: any) {
    yield put(setParticipationError(error.message || 'Failed to join event'));
  }
}

function* handleCancelEventParticipation(action: {
  payload: { userId: string; eventId: string };
}): SagaIterator {
  try {
    yield call(participationApi.cancelParticipation, action.payload);
    yield put(setCancelEventParticipationSuccess({ eventId: action.payload.eventId }));
    yield put(decrementParticipantCount({ eventId: action.payload.eventId }));
  } catch (error: any) {
    yield put(setParticipationError(error.message || 'Failed to cancel participation'));
  }
}

export function* participationSaga() {
  yield takeLatest(getUserParticipations, fetchUserParticipations);
  yield takeLatest(joinEventParticipation, handleJoinEventParticipation);
  yield takeLatest(cancelEventParticipation, handleCancelEventParticipation);
}
