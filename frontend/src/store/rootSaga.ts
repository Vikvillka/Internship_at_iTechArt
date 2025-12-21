import { all } from 'redux-saga/effects';
import { communitiesSaga } from './features/communities/communitiesSaga';
import { eventsSaga } from './features/events/eventsSaga';

export function* rootSaga() {
  yield all([eventsSaga(), communitiesSaga()]);
}
