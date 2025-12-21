import { all } from 'redux-saga/effects';
import { communitiesSaga } from './features/communities/communitiesSaga';
import { communityDetailsSaga } from './features/communityDetails/communityDetailsSaga';
import { eventDetailsSaga } from './features/eventDetails/eventDetailsSaga';
import { eventsSaga } from './features/events/eventsSaga';
import { userDetailsSaga } from './features/userDetails/userDetailsSaga';

export function* rootSaga() {
  yield all([
    eventsSaga(),
    communitiesSaga(),
    eventDetailsSaga(),
    communityDetailsSaga(),
    userDetailsSaga(),
  ]);
}
