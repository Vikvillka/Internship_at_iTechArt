import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import EventDetailsLeftColumn from '../../components/event/eventDetails/EventDetailsLeftColumn';
import { Event } from '../../models/Event';
import {
  selectCommunitiesError,
  selectCommunityById,
} from '../../store/features/communities/communitiesSelectors';
import { getCommunityById } from '../../store/features/communities/communitiesSlice';
import { selectUserById, selectUserError } from '../../store/features/users/usersSelectors';

interface Props {
  event: Event;
}

const EventDetailsLeftColumnContainer: React.FC<Props> = ({ event }) => {
  const dispatch = useDispatch();
  const community = useSelector(selectCommunityById);
  const owner = useSelector(selectUserById);
  const errorCommunity = useSelector(selectCommunitiesError);
  const errorUser = useSelector(selectUserError);

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
