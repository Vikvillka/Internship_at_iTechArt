import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { authApi } from '../../../api/auth';
import { AuthCreds } from '../../../models/Auth';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { login, logout, setAuthError, setTokens } from './authSlice';

function* handleLogin(action: { payload: AuthCreds }): SagaIterator {
  try {
    yield put(showLoader());
    const tokens = yield call(authApi.getTokens, action.payload);
    yield put(setTokens(tokens));
    localStorage.setItem('accessToken', tokens.accessToken);
    localStorage.setItem('refreshToken', tokens.refreshToken);
  } catch (error: any) {
    yield put(setAuthError(error.message || 'Login failed'));
  } finally {
    yield put(hideLoader());
  }
}

function* handleLogout(): SagaIterator {
  try {
    yield put(showLoader());
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  } catch (error: any) {
    yield put(setAuthError(error.message || 'Logout failed'));
  } finally {
    yield put(hideLoader());
  }
}

export function* authSaga(): SagaIterator {
  yield takeLatest(login, handleLogin);
  yield takeLatest(logout, handleLogout);
}
