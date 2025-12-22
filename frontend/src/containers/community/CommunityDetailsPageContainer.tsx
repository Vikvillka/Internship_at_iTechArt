import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import CommunityDetailsPage from '../../pages/communityDetailsPage/CommunityDetailsPage';
import {
  selectCommunityDetails,
  selectCommunityDetailsError,
  selectCommunitySubscriptionCounts,
} from '../../store/features/communityDetails/communityDetailsSelectors';
import { getCommunityDetails } from '../../store/features/communityDetails/communityDetailsSlice';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';

const CommunityDetailsPageContainer: React.FC = () => {
  const { communityId } = useParams();
  const dispatch = useDispatch();

  const community = useSelector(selectCommunityDetails);
  const subscriptionCount = useSelector(selectCommunitySubscriptionCounts);
  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectCommunityDetailsError);

  useEffect(() => {
    if (communityId) {
      dispatch(getCommunityDetails({ id: communityId, showLoader: true }));
    }
  }, [communityId, dispatch]);

  return (
    <CommunityDetailsPage
      community={community}
      subscriptionCount={subscriptionCount}
      loading={loading}
      error={error}
    />
  );
};

export default CommunityDetailsPageContainer;
