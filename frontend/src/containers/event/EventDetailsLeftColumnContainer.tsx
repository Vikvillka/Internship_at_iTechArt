import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import EventDetailsLeftColumn from '../../components/event/eventDetails/EventDetailsLeftColumn';
import { Event } from '../../models/Event';
import {
  selectCommunitiesError,
  selectCommunityById,
} from '../../store/features/communities/communitiesSelectors';
import { getCommunityById } from '../../store/features/communities/communitiesSlice';
import {
  selectUserDetails,
  selectUserDetailsError,
} from '../../store/features/userDetails/userDetailsSelectors';

interface Props {
  event: Event;
}

const EventDetailsLeftColumnContainer: React.FC<Props> = ({ event }) => {
  const dispatch = useDispatch();
  const community = useSelector(selectCommunityById);
  const owner = useSelector(selectUserDetails);
  const errorCommunity = useSelector(selectCommunitiesError);
  const errorUser = useSelector(selectUserDetailsError);

  useEffect(() => {
    if (event.communityId) {
      dispatch(getCommunityById({ id: event.communityId, showLoader: false }));
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
