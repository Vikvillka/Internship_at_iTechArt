import { SagaIterator } from 'redux-saga';
import { call, put, takeLatest } from 'redux-saga/effects';
import { communityApi } from '../../../api/community';
import { subscriptionApi } from '../../../api/subscription';
import { PagedResponse } from '../../../models/Common';
import { Community, CommunitySearchRequest } from '../../../models/Community';
import { hideLoader, showLoader } from '../loader/loaderSlice';
import { getUserById } from '../users/usersSlice';

import {
  deleteCommunity,
  getCommunities,
  getCommunityById,
  getUserCommunities,
  setCommunities,
  setCommunitiesError,
  setCommunityById,
  setUserCommunities,
} from './communitiesSlice';

function* fetchCommunities(action: { payload: CommunitySearchRequest }): SagaIterator {
  try {
    yield put(incrementLoader());

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
  } catch (error: any) {
    yield put(setCommunitiesError(error.message || 'Failed to fetch communities'));
  } finally {
    yield put(decrementLoader());
  }
}

function* fetchCommunityById(action: {
  payload: { id: string; showLoader?: boolean };
}): SagaIterator {
  try {
    if (action.payload.showLoader) yield put(incrementLoader());

    const community: Community = yield call(communityApi.getCommunityById, action.payload.id);
    const counts = yield call(subscriptionApi.getSubscriptionCounts, [action.payload.id]);
    const subscriptionCount = counts.length > 0 ? counts[0].count : 0;

    yield put(
      setCommunityById({
        community,
        subscriptionCount,
      }),
    );
  } catch (error: any) {
    yield put(setCommunitiesError(error.message || 'Failed to fetch community details'));
  } finally {
    if (action.payload.showLoader) yield put(decrementLoader());
  }
}

function* fetchUserCommunities(action: { payload: { userId: string } }): SagaIterator {
  try {
    yield put(showLoader());

    const communities: Community[] = yield call(
      communityApi.getUserCommunities,
      action.payload.userId,
    );

    const ids = communities.map((c) => c.id);
    const counts = yield call(subscriptionApi.getSubscriptionCounts, ids);

    const subscriptionCounts: Record<string, number> = {};
    counts.forEach((c: { communityId: string; count: number }) => {
      subscriptionCounts[c.communityId] = c.count;
    });
    yield put(setUserCommunities({ communities, subscriptionCounts }));
  } catch (error: any) {
    yield put(setCommunitiesError(error.message || 'Failed to fetch user communities'));
  } finally {
    yield put(hideLoader());
  }
}

function* handleDeleteCommunity(action: {
  payload: { communityId: string; userId: string };
}): SagaIterator {
  try {
    yield put(showLoader());

    yield call(communityApi.deleteCommunity, action.payload.communityId);

    if (action.payload.userId) {
      yield put(getUserCommunities({ userId: action.payload.userId }));
    }
  } catch (error: any) {
    yield put(setCommunitiesError(error.message || 'Failed to delete community'));
  } finally {
    yield put(hideLoader());
  }
}

export function* communitiesSaga() {
  yield takeLatest(getCommunities, fetchCommunities);
  yield takeLatest(getCommunityById, fetchCommunityById);
  yield takeLatest(getUserCommunities, fetchUserCommunities);
  yield takeLatest(deleteCommunity, handleDeleteCommunity);
}
