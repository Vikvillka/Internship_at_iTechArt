import { all } from 'redux-saga/effects';
import { authSaga } from './features/auth/authSaga';
import { communitiesSaga } from './features/communities/communitiesSaga';
import { eventsSaga } from './features/events/eventsSaga';
import { participationSaga } from './features/participation/participationSaga';
import { usersSaga } from './features/users/usersSaga';

export function* rootSaga() {
  yield all([eventsSaga(), participationSaga(), communitiesSaga(), usersSaga(), authSaga()]);
}
