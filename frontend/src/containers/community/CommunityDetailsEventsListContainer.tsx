import { useEffect, useState } from 'react';
import { eventsApi } from '../../api/event';
import { participationApi } from '../../api/participation';
import CommunityDetailsEventsList from '../../components/community/communityDetails/CommunityDetailsEventsList';
import { Event } from '../../models/Event';

interface Props {
  communityId: string;
}

const CommunityDetailsInfoContainer: React.FC<Props> = ({ communityId }) => {
  const [events, setEvents] = useState<Event[]>([]);
  const [participantCount, setParticipantCount] = useState<Record<string, number>>({});
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const eventsData = await eventsApi.getEventsByCommunityId(communityId);
        setEvents(eventsData);
        const eventIds = eventsData.map((event) => event.id);
        const counts = await participationApi.getParticipationCounts(eventIds);
        const countsMap: Record<string, number> = {};
        counts.forEach((count) => {
          countsMap[count.eventId] = count.count;
        });
        setParticipantCount(countsMap);
      } catch (err: any) {
        setError(err.message || 'Failed to fetch community events');
      }
    };
    fetchData();
  }, [communityId]);

  return (
    <CommunityDetailsEventsList events={events} participantCount={participantCount} error={error} />
  );
};

export default CommunityDetailsInfoContainer;
