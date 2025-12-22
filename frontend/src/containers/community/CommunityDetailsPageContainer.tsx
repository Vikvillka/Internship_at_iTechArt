import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import CommunityDetailsPage from '../../pages/communityDetailsPage/CommunityDetailsPage';
import {
  selectCommunitiesError,
  selectCommunityById,
  selectCommunitySubscriptionCounts,
} from '../../store/features/communities/communitiesSelectors';
import { getCommunityById } from '../../store/features/communities/communitiesSlice';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';

const CommunityDetailsPageContainer: React.FC = () => {
  const { communityId } = useParams();
  const dispatch = useDispatch();

  const community = useSelector(selectCommunityById);
  const subscriptionCount = useSelector(selectCommunitySubscriptionCounts);
  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectCommunitiesError);

  useEffect(() => {
    if (communityId) {
      dispatch(getCommunityById({ id: communityId, showLoader: true }));
    }
  }, [communityId, dispatch]);

  return (
    <CommunityDetailsPage
      community={community}
      subscriptionCount={subscriptionCount[communityId || ''] || 0}
      loading={loading}
      error={error}
    />
  );
};

export default CommunityDetailsPageContainer;
