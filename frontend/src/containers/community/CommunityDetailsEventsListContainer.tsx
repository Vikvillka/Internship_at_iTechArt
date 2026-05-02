import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import CommunityDetailsEventsList from '../../components/community/communityDetails/CommunityDetailsEventsList';
import { getEventByCommunityId } from '../../store/features/events/eventsSlice';

const CommunityDetailsEventsListContainer: React.FC = () => {
  const dispatch = useDispatch();
  const { communityId } = useParams<{ communityId: string }>();

  useEffect(() => {
    if (communityId) {
      dispatch(getEventByCommunityId({ communityId, showLoader: true }));
    }
  }, [communityId, dispatch]);

  return <CommunityDetailsEventsList />;
};

export default CommunityDetailsEventsListContainer;
