import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import EventDetailsLeftColumn from '../../components/event/eventDetails/EventDetailsLeftColumn';
import { Event } from '../../models/Event';
import {
  selectCommunityDetails,
  selectCommunityDetailsError,
} from '../../store/features/communityDetails/communityDetailsSelectors';
import { getCommunityDetails } from '../../store/features/communityDetails/communityDetailsSlice';
import {
  selectUserDetails,
  selectUserDetailsError,
} from '../../store/features/userDetails/userDetailsSelectors';

interface Props {
  event: Event;
}

const EventDetailsLeftColumnContainer: React.FC<Props> = ({ event }) => {
  const dispatch = useDispatch();
  const community = useSelector(selectCommunityDetails);
  const owner = useSelector(selectUserDetails);
  const errorCommunity = useSelector(selectCommunityDetailsError);
  const errorUser = useSelector(selectUserDetailsError);

  useEffect(() => {
    if (event.communityId) {
      dispatch(getCommunityDetails({ id: event.communityId }));
    }
  }, [event.communityId, dispatch]);

  const error = errorCommunity || errorUser || null;

  return (
    <EventDetailsLeftColumn
      event={event}
      community={community ?? undefined}
      owner={owner ?? undefined}
      error={error}
    />
  );
};

export default EventDetailsLeftColumnContainer;
