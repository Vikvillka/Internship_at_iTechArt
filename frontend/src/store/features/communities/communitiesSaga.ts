import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { communityApi } from '../../../api/community';
import { subscriptionApi } from '../../../api/subscription';
import { PagedResponse } from '../../../models/Common';
import { Community, CommunitySearchRequest } from '../../../models/Community';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { getCommunities, setCommunities } from './communitiesSlice';

function* fetchCommunities(action: { payload: CommunitySearchRequest }): SagaIterator {
  try {
    yield put(showLoader());

    const response: PagedResponse<Community> = yield call(
      communityApi.getCommunitiesBySearch,
      action.payload,
    );

    const ids = response.items.map((c) => c.id);
    const counts = yield call(subscriptionApi.getSubscriptionCounts, ids);

    const subscriptionCounts: Record<string, number> = {};
    counts.forEach((c: { communityId: string; count: number }) => {
      subscriptionCounts[c.communityId] = c.count;
    });

    yield put(
      setCommunities({
        items: response.items,
        subscriptionCounts,
        totalPages: Math.ceil(response.totalCount / response.pageSize),
      }),
    );
  } catch (error) {
    console.error('Failed to fetch communities:', error);
  } finally {
    yield put(hideLoader());
  }
}

export function* communitiesSaga() {
  yield takeLatest(getCommunities, fetchCommunities);
}
