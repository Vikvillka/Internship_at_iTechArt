import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import EventDetailsPage from '../../pages/eventDetailsPage/EventDetailsPage';
import {
  selectEventDetails,
  selectEventDetailsError,
  selectParticipationCount,
} from '../../store/features/eventDetails/eventDetailsSelectors';
import { getEventDetails } from '../../store/features/eventDetails/eventDetailsSlice';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';

const EventDetailsPageContainer: React.FC = () => {
  const { eventId } = useParams<{ eventId: string }>();
  const dispatch = useDispatch();

  const event = useSelector(selectEventDetails);
  const participantsCount = useSelector(selectParticipationCount);
  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectEventDetailsError);

  useEffect(() => {
    if (eventId) {
      dispatch(getEventDetails({ id: eventId, showLoader: true }));
    }
  }, [eventId, dispatch]);

  return (
    <EventDetailsPage
      event={event}
      participantCount={participantsCount}
      loading={loading}
      error={error}
    />
  );
};

export default EventDetailsPageContainer;
