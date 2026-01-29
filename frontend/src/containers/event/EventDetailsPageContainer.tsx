import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import EventDetailsPage from '../../pages/eventDetailsPage/EventDetailsPage';
import { getEventById } from '../../store/features/events/eventsSlice';

const EventDetailsPageContainer: React.FC = () => {
  const { eventId } = useParams<{ eventId: string }>();
  const dispatch = useDispatch();

  const event = useSelector(selectEventById);
  const participantsCount = useSelector(selectParticipationCounts);
  const error = useSelector(selectEventsError);

  useEffect(() => {
    if (eventId) {
      dispatch(getEventById({ id: eventId, showLoader: true }));
    }
  }, [eventId, dispatch]);

  return (
    <EventDetailsPage
      event={event}
      participantCount={participantsCount[eventId || ''] || 0}
      error={error}
    />
  );
};

export default EventDetailsPageContainer;
