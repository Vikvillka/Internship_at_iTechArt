import { useEffect, useState } from 'react';
import { communityApi } from '../../api/community';
import { userApi } from '../../api/user';
import EventDetailsLeftColumn from '../../components/event/eventDetails/EventDetailsLeftColumn';
import { Community } from '../../models/Community';
import { Event } from '../../models/Event';
import { User } from '../../models/User';

interface Props {
  event: Event;
}

const EventDetailsLeftColumnContainer: React.FC<Props> = ({ event }) => {
  const [community, setCommunity] = useState<Community | null>(null);
  const [owner, setOwner] = useState<User | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const community = await communityApi.getCommunityById(event.communityId);
        setCommunity(community);

        const ownerData = await userApi.getUserById(community.ownerId);
        setOwner(ownerData);
      } catch (err: any) {
        setError(err.message || 'Failed to fetch event details');
      }
    };

    fetchData();
  }, [event.communityId]);

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
