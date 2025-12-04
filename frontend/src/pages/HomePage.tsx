import React, { useEffect, useState } from 'react';
import { eventsApi } from '../api/event';
import { Event } from '../models/Event';
import { Container, Typography, CircularProgress, Box } from '@mui/material';
import Grid from '@mui/material/Grid';
import EventCard from '../components/event/eventCard/EventCard';

const HomePage: React.FC = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    eventsApi
      .getEvents()
      .then((data) => {
        setEvents(data);
      })
      .catch((err) => {
        setError(`Failed to fetch events. ${err.message}`);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 10 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Container sx={{ mt: 5 }}>
        <Typography variant='h6' color='error'>
          {error}
        </Typography>
      </Container>
    );
  }

  return (
    <Container sx={{ mt: 5 }} disableGutters>
      <Typography variant='h4' gutterBottom>
        Events
      </Typography>
      <Grid container spacing={1} mt={3}>
        {events.map((event) => (
          <Grid key={event.id} size={{ xs: 12, sm: 6, md: 3 }}>
            <EventCard event={event} />
          </Grid>
        ))}
      </Grid>
    </Container>
  );
};

export default HomePage;
