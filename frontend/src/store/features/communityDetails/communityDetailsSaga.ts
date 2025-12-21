import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { communityApi } from '../../../api/community';
import { subscriptionApi } from '../../../api/subscription';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { getUserDetails } from '../userDetails/userDetailsSlice';
import {
  getCommunityDetails,
  setCommunityDetails,
  setCommunityDetailsError,
} from './communityDetailsSlice';

function* fetchCommunityDetails(action: {
  payload: { id: string; showLoader?: boolean };
}): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(showLoader());

    const community = yield call(communityApi.getCommunityById, action.payload.id);
    const count = yield call(subscriptionApi.getSubscriptionCounts, [action.payload.id]);
    const subscriptionsCount = count.length > 0 ? count[0].count : 0;

    yield put(setCommunityDetails({ community, subscriptionsCount }));
    if (community.ownerId) {
      yield put(getUserDetails({ id: community.ownerId }));
    }
  } catch (error: any) {
    console.error('Failed to fetch community details:', error);
    yield put(setCommunityDetailsError(error.message || 'Failed to fetch community details'));
  } finally {
    if (action.payload.showLoader) yield put(hideLoader());
  }
}

export function* communityDetailsSaga() {
  yield takeLatest(getCommunityDetails, fetchCommunityDetails);
}
