import { Box, CircularProgress, Container, Pagination, Typography } from '@mui/material';
import Grid from '@mui/material/Grid';
import React, { useEffect, useState } from 'react';
import { eventsApi } from '../api/event';
import { participationApi } from '../api/participation';
import EventCard from '../components/event/eventCard/EventCard';
import { PagedResponse } from '../models/Common';
import { Event, EventSearchRequest } from '../models/Event';
import { errorContainer, errorText, loadingBox, pageContainer } from '../styles/common';

const HomePage: React.FC = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [participantCounts, setParticipantCounts] = useState<Record<string, number>>({});
  const [page, setPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const loadEvents = async (pageNumber: number) => {
    setLoading(true);

    const params: EventSearchRequest = {
      page: pageNumber,
      pageSize: 20,
    };

    try {
      const data: PagedResponse<Event> = await eventsApi.searchEvents(params);
      setEvents(data.items);
      setTotalPages(Math.ceil(data.totalCount / data.pageSize));

      const eventIds = data.items.map((event) => event.id);
      const counts = await participationApi.getParticipationCounts(eventIds);
      const countsMap: Record<string, number> = {};
      counts.forEach((count) => {
        countsMap[count.eventId] = count.count;
      });
      setParticipantCounts(countsMap);
    } catch (err: any) {
      setError(err.message || 'Failed to fetch events');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadEvents(page);
  }, [page]);

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
      <Box mt={4} display='flex' justifyContent='center' alignItems='center'>
        <Pagination
          count={totalPages}
          page={page}
          onChange={(_, value) => setPage(value)}
          color='primary'
          size='medium'
        />
      </Box>
    </Container>
  );
};

export default HomePage;
