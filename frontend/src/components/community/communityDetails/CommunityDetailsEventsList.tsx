import { Grid, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import {
  selectEventsByCommunityId,
  selectEventsError,
  selectParticipationCounts,
} from '../../../store/features/events/eventsSelectors';
import EmptyState from '../../common/emptyState/EmptyState';
import EventCard from '../../event/eventCard/EventCard';
import { communityDetailsStyles } from './CommunityDetails.styles';

const CommunityDetailsEventsList: React.FC = () => {
  const events = useSelector(selectEventsByCommunityId);
  const participantCount = useSelector(selectParticipationCounts);
  const error = useSelector(selectEventsError);

  return (
    <>
      {error && (
        <Typography color='error' variant='subtitle2'>
          {error}
        </Typography>
      )}
      <Typography variant='h5' sx={communityDetailsStyles.titleSection}>
        Upcoming events
      </Typography>
      {events.length === 0 && !error && (
        <EmptyState message='No upcoming events in this community.' />
      )}
      {events.length > 0 && (
        <Grid container spacing={1}>
          {events.map((event) => (
            <Grid key={event.id} size={{ xs: 12, sm: 6, md: 3 }}>
              <EventCard event={event} participantCount={participantCount[event.id] ?? 0} />
            </Grid>
          ))}
        </Grid>
      )}
    </>
  );
};

export default CommunityDetailsEventsList;
