import { Box, CircularProgress, Container, Typography } from '@mui/material';
import EventDetails from '../../components/event/eventDetails/EventDetails';
import { Community } from '../../models/Community';
import { Event } from '../../models/Event';
import { User } from '../../models/User';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';

interface Props {
  event: Event | null;
  community: Community | null;
  owner: User | null;
  participantCount: number;
  loading: boolean;
  error: string | null;
}

const EventDetailsView: React.FC<Props> = ({
  event,
  community,
  owner,
  participantCount,
  loading,
  error,
}) => {
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
          participantCount={participantCount}
          community={community}
          owner={owner}
        />
      )}
    </Container>
  );
};

export default EventDetailsView;
