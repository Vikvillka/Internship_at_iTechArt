import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { userApi } from '../../../api/user';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { closeRegisterModal, openLoginModal } from '../ui/uiSlice';
import {
  getUserById,
  registerUser,
  registerUserSuccess,
  setUserById,
  setUserError,
} from './usersSlice';

function* fetchUserById(action: { payload: { id: string; showLoader?: boolean } }): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(showLoader());
    const user = yield call(userApi.getUserById, action.payload.id);
    yield put(setUserById({ user }));
  } catch (error: any) {
    yield put(setUserError(error.message || 'Failed to fetch user details'));
  } finally {
    if (action.payload.showLoader) yield put(hideLoader());
  }
}

function* fetchRegisterUser(action: {
  payload: { userRegistration: any; showLoader?: boolean };
}): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(showLoader());
    const user = yield call(userApi.registerUser, action.payload.userRegistration);
    yield put(registerUserSuccess());
    yield put(closeRegisterModal());
    yield put(openLoginModal());
  } catch (error: any) {
    yield put(setUserError(error.message || 'Failed to register user'));
  } finally {
    if (action.payload.showLoader) yield put(hideLoader());
  }
}

export function* usersSaga() {
  yield takeLatest(getUserById, fetchUserById);
  yield takeLatest(registerUser, fetchRegisterUser);
}
