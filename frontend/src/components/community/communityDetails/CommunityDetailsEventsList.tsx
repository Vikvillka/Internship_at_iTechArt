import { Grid, Typography } from '@mui/material';
import React from 'react';
import { Event } from '../../../models/Event';
import EmptyState from '../../common/emptyState/EmptyState';
import EventCard from '../../event/eventCard/EventCard';
import { communityDetailsStyles } from './CommunityDetails.styles';

interface Props {
  events: Event[];
  participantCount: Record<string, number>;
  error?: string | null;
}

const CommunityDetailsEventsList: React.FC<Props> = ({ events, participantCount, error }) => {
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
