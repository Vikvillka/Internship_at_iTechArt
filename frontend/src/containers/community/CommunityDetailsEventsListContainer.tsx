import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CommunityDetailsEventsList from '../../components/community/communityDetails/CommunityDetailsEventsList';
import {
  selectEventsByCommunityId,
  selectEventsError,
  selectParticipationCounts,
} from '../../store/features/events/eventsSelectors';
import { getEventByCommunityId } from '../../store/features/events/eventsSlice';

interface Props {
  communityId: string;
}

const CommunityDetailsInfoContainer: React.FC<Props> = ({ communityId }) => {
  const dispatch = useDispatch();
  const events = useSelector(selectEventsByCommunityId);
  const participantCount = useSelector(selectParticipationCounts);
  const error = useSelector(selectEventsError);

  useEffect(() => {
    if (communityId) {
      dispatch(getEventByCommunityId({ communityId: communityId, showLoader: false }));
    }
  }, [communityId, dispatch]);

  return (
    <CommunityDetailsEventsList events={events} participantCount={participantCount} error={error} />
  );
};

export default CommunityDetailsInfoContainer;
