import { Box } from '@mui/material';
import React from 'react';
import { Community } from '../../../models/Community';
import { Event } from '../../../models/Event';
import { User } from '../../../models/User';
import { useStyles } from './EventDetails.styles';
import EventDescriptionDetails from './EventDetailsDescription';
import EventDetailsHeader from './EventDetailsHeader';

interface Props {
  event: Event;
  community?: Community;
  owner?: User;
  error?: string | null;
}

const EventDetailsLeftColumn: React.FC<Props> = ({ event, community, owner, error }) => {
  const classes = useStyles();

  return (
    <Box className={classes.leftColumn}>
      <EventDetailsHeader
        title={event.title}
        ownerName={owner?.username}
        communityName={community?.name}
        error={error ?? undefined}
      />
      <EventDescriptionDetails
        description={event.description}
        address={event.address}
        city={community?.city}
        country={community?.country}
        lat={event.latitude ?? undefined}
        lng={event.longitude ?? undefined}
        error={error ?? undefined}
      />
    </Box>
  );
};

export default EventDetailsLeftColumn;
