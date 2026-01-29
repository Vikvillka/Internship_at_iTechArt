import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CommunityDetailsEventsList from '../../components/community/communityDetails/CommunityDetailsEventsList';
import { selectCommunityById } from '../../store/features/communities/communitiesSelectors';
import { getEventByCommunityId } from '../../store/features/events/eventsSlice';

const CommunityDetailsInfoContainer: React.FC = () => {
  const dispatch = useDispatch();
  const community = useSelector(selectCommunityById);

  useEffect(() => {
    if (community?.id) {
      dispatch(getEventByCommunityId({ communityId: community.id, showLoader: false }));
    }
  }, [community?.id, dispatch]);

  return <CommunityDetailsEventsList />;
};

export default CommunityDetailsInfoContainer;
