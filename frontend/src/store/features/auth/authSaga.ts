import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { authApi } from '../../../api/auth';
import { userApi } from '../../../api/user';
import { parseJwt } from '../../../helpers/parseJwt';
import { AuthCreds } from '../../../models/Auth';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { closeLoginModal } from '../ui/uiSlice';
import { login, logout, setAuthError, setTokens, setUser } from './authSlice';

function* handleLogin(action: { payload: AuthCreds }): SagaIterator {
  try {
    yield put(showLoader());
    const tokens = yield call(authApi.getTokens, action.payload);
    yield put(setTokens(tokens));
    localStorage.setItem('accessToken', tokens.accessToken);
    localStorage.setItem('refreshToken', tokens.refreshToken);

    const payload = parseJwt(tokens.accessToken);
    const user = yield call(userApi.getUserById, payload.sub);

    yield put(setUser(user));
    yield put(closeLoginModal());
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

function* initAuth(): SagaIterator {
  const accessToken = localStorage.getItem('accessToken');
  if (accessToken) {
    const payload = parseJwt(accessToken);
    try {
      yield put(showLoader());
      const user = yield call(userApi.getUserById, payload.sub);
      yield put(setUser(user));
    } catch (error: any) {
      yield put(setAuthError(error.message || 'Failed to fetch user data'));
    } finally {
      yield put(hideLoader());
    }
  }
}

export function* authSaga(): SagaIterator {
  yield takeLatest(login, handleLogin);
  yield takeLatest(logout, handleLogout);
  yield call(initAuth);
}
