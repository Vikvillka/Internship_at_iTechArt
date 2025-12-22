import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { userApi } from '../../../api/user';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { getUserById, setUserById, setUserError } from './usersSlice';

function* fetchUserById(action: { payload: { id: string; showLoader?: boolean } }): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(showLoader());
    const user = yield call(userApi.getUserById, action.payload.id);
    yield put(setUserById({ user }));
  } catch (error: any) {
    console.error('Failed to fetch user details:', error);
    yield put(setUserError(error.message || 'Failed to fetch user details'));
  } finally {
    if (action.payload.showLoader) yield put(hideLoader());
  }
}

export function* usersSaga() {
  yield takeLatest(getUserById, fetchUserById);
}
