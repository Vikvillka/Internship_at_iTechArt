import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import EventDetailsPage from '../../pages/eventDetailsPage/EventDetailsPage';
import { getEventById } from '../../store/features/events/eventsSlice';

const EventDetailsPageContainer: React.FC = () => {
  const { eventId } = useParams<{ eventId: string }>();
  const dispatch = useDispatch();

  useEffect(() => {
    if (eventId) {
      dispatch(getEventById({ id: eventId, showLoader: true }));
    }
  }, [eventId, dispatch]);

  return <EventDetailsPage />;
};

export default EventDetailsPageContainer;
