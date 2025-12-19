import { Box, CircularProgress, Container, Typography } from '@mui/material';
import EventDetails from '../../components/event/eventDetails/EventDetails';
import { Event } from '../../models/Event';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';

interface Props {
  event: Event | null;
  participantCount: number;
  loading: boolean;
  error: string | null;
}

const EventDetailsPage: React.FC<Props> = ({ event, participantCount, loading, error }) => {
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
      {event && <EventDetails event={event} participantCount={participantCount} />}
    </Container>
  );
};

export default EventDetailsPage;
