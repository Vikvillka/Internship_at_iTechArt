import { all } from 'redux-saga/effects';
import { communitiesSaga } from './features/communities/communitiesSaga';
import { communityDetailsSaga } from './features/communityDetails/communityDetailsSaga';
import { eventsSaga } from './features/events/eventsSaga';
import { userDetailsSaga } from './features/userDetails/userDetailsSaga';

export function* rootSaga() {
  yield all([eventsSaga(), communitiesSaga(), communityDetailsSaga(), userDetailsSaga()]);
}
