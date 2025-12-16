import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { eventsApi } from '../../api/event';
import { participationApi } from '../../api/participation';
import { Event } from '../../models/Event';
import EventDetailsView from '../../pages/eventDetailsPage/EventDetailsPage';

const EventDetailsPageContainer: React.FC = () => {
  const { eventId } = useParams();
  const [event, setEvent] = useState<Event | null>(null);
  const [participantsCount, setParticipantsCount] = useState<number>(0);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchEventDetails = async () => {
      try {
        const eventData = await eventsApi.getEventById(eventId!);
        setEvent(eventData);

        const counts = await participationApi.getParticipationCounts([eventId!]);
        setParticipantsCount(counts.length > 0 ? counts[0].count : 0);
      } catch (err: any) {
        setError(err.message || 'Failed to fetch event details');
      } finally {
        setLoading(false);
      }
    };

    fetchEventDetails();
  }, [eventId]);

  return (
    <EventDetailsView
      event={event}
      participantCount={participantsCount}
      loading={loading}
      error={error}
    />
  );
};

export default EventDetailsPageContainer;
