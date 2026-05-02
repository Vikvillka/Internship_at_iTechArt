import { Container, Typography } from '@mui/material';
import EventParticipationActionBar from '../../components/event/eventDetails/EventParticipationActionBar';
import EventDetails from '../../components/event/eventDetails/EventDetails';
import { Event } from '../../models/Event';
import { errorContainer, errorText, pageContainer } from '../../styles/common';

interface Props {
  event: Event | null;
  participantCount: number;
  error: string | null;
}

const EventDetailsPage: React.FC<Props> = ({ event, participantCount, error }) => {
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
      <EventDetails />
      <EventParticipationActionBar event={event} participantCount={participantCount} />
    </Container>
  );
};

export default EventDetailsPage;
