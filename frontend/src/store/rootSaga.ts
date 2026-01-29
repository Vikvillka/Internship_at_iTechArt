import { all } from 'redux-saga/effects';
import { communitiesSaga } from './features/communities/communitiesSaga';
import { eventsSaga } from './features/events/eventsSaga';
import { usersSaga } from './features/users/usersSaga';

export function* rootSaga() {
  yield all([eventsSaga(), communitiesSaga(), usersSaga()]);
}
