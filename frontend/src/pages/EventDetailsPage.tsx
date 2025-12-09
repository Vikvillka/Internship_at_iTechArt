import { Box, CircularProgress, Container, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { communityApi } from '../api/community';
import { eventsApi } from '../api/event';
import { participationApi } from '../api/participation';
import { userApi } from '../api/user';
import EventDetails from '../components/event/eventDetails/EventDetails';
import { Community } from '../models/Community';
import { Event } from '../models/Event';
import { User } from '../models/User';
import { errorContainer, errorText, loadingBox, pageContainer } from '../styles/common';

const EventDetailsPage: React.FC = () => {
  const { eventId } = useParams();
  const [event, setEvent] = useState<Event | null>(null);
  const [participantsCount, setParticipantsCount] = useState<number>(0);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [community, setCommunity] = useState<Community | null>(null);
  const [owner, setOwner] = useState<User | null>(null);

  useEffect(() => {
    const fetchEventDetails = async () => {
      try {
        const eventData = await eventsApi.getEventById(eventId!);
        setEvent(eventData);

        const communityData = await communityApi.getCommunityById(eventData.communityId);
        setCommunity(communityData);

        const ownerData = await userApi.getUserById(communityData.ownerId);
        setOwner(ownerData);

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

  if (loading) {
    return (
      <Box sx={loadingBox}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Container sx={errorContainer}>
        <Typography variant='h6' sx={errorText}>
          {error}
        </Typography>
      </Container>
    );
  }

  return (
    <Container sx={pageContainer}>
      {event && community && owner && (
        <EventDetails
          event={event}
          participantCount={participantsCount}
          community={community}
          owner={owner}
        />
      )}
    </Container>
  );
};

export default EventDetailsPage;
