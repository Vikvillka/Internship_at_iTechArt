import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import EventDetailsLeftColumn from '../../components/event/eventDetails/EventDetailsLeftColumn';
import {
  selectCommunitiesError,
  selectCommunityById,
} from '../../store/features/communities/communitiesSelectors';
import { getCommunityById } from '../../store/features/communities/communitiesSlice';
import { selectEventById } from '../../store/features/events/eventsSelectors';
import { selectUserError } from '../../store/features/users/usersSelectors';
import { getUserById } from '../../store/features/users/usersSlice';

const EventDetailsLeftColumnContainer: React.FC = () => {
  const dispatch = useDispatch();
  const event = useSelector(selectEventById);
  const community = useSelector(selectCommunityById);
  const errorCommunity = useSelector(selectCommunitiesError);
  const errorUser = useSelector(selectUserError);

  useEffect(() => {
    if (event?.communityId) {
      dispatch(getCommunityById({ id: event.communityId, showLoader: true }));
    }
  }, [event?.communityId, dispatch]);

  useEffect(() => {
    if (community?.ownerId) {
      dispatch(getUserById({ id: community.ownerId, showLoader: false }));
    }
  }, [community?.ownerId, dispatch]);

  const error = errorCommunity || errorUser || null;

  return <EventDetailsLeftColumn error={error} />;
};

export default EventDetailsLeftColumnContainer;
