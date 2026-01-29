import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { userApi } from '../../../api/user';
import { decrementLoader, incrementLoader } from '../loader/loaderSlice';
import { getUserById, setUserById, setUserError } from './usersSlice';

function* fetchUserById(action: { payload: { id: string; showLoader?: boolean } }): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(incrementLoader());
    const user = yield call(userApi.getUserById, action.payload.id);
    yield put(setUserById({ user }));
  } catch (error: any) {
    yield put(setUserError(error.message || 'Failed to fetch user details'));
  } finally {
    if (action.payload.showLoader) yield put(decrementLoader());
  }
}

export function* usersSaga() {
  yield takeLatest(getUserById, fetchUserById);
}
