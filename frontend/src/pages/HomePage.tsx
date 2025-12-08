import React, { useEffect, useState } from 'react';
import Grid from '@mui/material/Grid';
import { Container, Typography, CircularProgress, Box } from '@mui/material';
import EventCard from '../components/event/eventCard/EventCard';
import { eventsApi } from '../api/event';
import { participationApi } from '../api/participation';
import { Event } from '../models/Event';
import { loadingBox, errorContainer, errorText, pageContainer } from '../styles/common';

const HomePage: React.FC = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [participantCounts, setParticipantCounts] = useState<Record<string, number>>({});
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchEventsAndParticipation = async () => {
      try {
        const eventsData = await eventsApi.getEvents();
        setEvents(eventsData);

        const eventIds = eventsData.map((event) => event.id);
        const counts = await participationApi.getParticipationCounts(eventIds);

        const countsMap: Record<string, number> = {};
        counts.forEach((count) => {
          countsMap[count.eventId] = count.count;
        });
        setParticipantCounts(countsMap);
      } catch (err: any) {
        setError(`Failed to fetch events. ${err.message}`);
      } finally {
        setLoading(false);
      }
    };
    fetchEventsAndParticipation();
  }, []);

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
    <Container sx={pageContainer} disableGutters>
      <Typography variant='h4' gutterBottom>
        Events
      </Typography>
      <Grid container spacing={1} mt={3}>
        {events.map((event) => (
          <Grid key={event.id} size={{ xs: 12, sm: 6, md: 3 }}>
            <EventCard event={event} participantCount={participantCounts[event.id] ?? 0} />
          </Grid>
        ))}
      </Grid>
    </Container>
  );
};

export default HomePage;
