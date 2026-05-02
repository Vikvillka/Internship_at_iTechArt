import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import EventDetailsPage from '../../pages/eventDetailsPage/EventDetailsPage';
import { selectActiveUser } from '../../store/features/auth/authSelectors';
import {
  selectEventById,
  selectEventsError,
  selectParticipationCounts,
} from '../../store/features/events/eventsSelectors';
import { getEventById } from '../../store/features/events/eventsSlice';
import { getUserParticipations } from '../../store/features/participation/participationSlice';

const EventDetailsPageContainer: React.FC = () => {
  const { eventId } = useParams<{ eventId: string }>();
  const dispatch = useDispatch();

  const event = useSelector(selectEventById);
  const participantsCount = useSelector(selectParticipationCounts);
  const error = useSelector(selectEventsError);
  const user = useSelector(selectActiveUser);

  useEffect(() => {
    if (eventId) {
      dispatch(getEventById({ id: eventId, showLoader: true }));
    }
  }, [eventId, dispatch]);

  useEffect(() => {
    if (user?.id) {
      dispatch(getUserParticipations({ userId: user.id }));
    }
  }, [dispatch, user?.id]);

  return (
    <EventDetailsPage
      event={event}
      participantCount={participantsCount[eventId || ''] || 0}
      error={error}
    />
  );
};

export default EventDetailsPageContainer;
