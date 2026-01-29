import { Box, CircularProgress, Container, Typography } from '@mui/material';
import { useSelector } from 'react-redux';
import EventDetails from '../../components/event/eventDetails/EventDetails';
import { selectEventsError } from '../../store/features/events/eventsSelectors';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';

const EventDetailsPage: React.FC = () => {
  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectEventsError);

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
      <EventDetails />
    </Container>
  );
};

export default EventDetailsPage;
