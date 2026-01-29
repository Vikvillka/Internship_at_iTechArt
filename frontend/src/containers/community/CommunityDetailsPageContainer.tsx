import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import CommunityDetailsPage from '../../pages/communityDetailsPage/CommunityDetailsPage';
import { getCommunityById } from '../../store/features/communities/communitiesSlice';

const CommunityDetailsPageContainer: React.FC = () => {
  const { communityId } = useParams();
  const dispatch = useDispatch();

  useEffect(() => {
    if (communityId) {
      dispatch(getCommunityById({ id: communityId, showLoader: true }));
    }
  }, [communityId, dispatch]);

  return <CommunityDetailsPage />;
};

export default CommunityDetailsPageContainer;
