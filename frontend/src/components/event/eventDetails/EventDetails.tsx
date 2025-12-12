import { Container } from '@mui/material';
import React from 'react';
import { Community } from '../../../models/Community';
import { Event } from '../../../models/Event';
import { User } from '../../../models/User';
import { useStyles } from './EventDetails.styles';
import EventDetailsLeftColumn from './EventDetailsLeftColumn';
import EventDetailsRightColumn from './EventDetailsRightColumn';

interface EventDetailsProps {
  event: Event;
  participantCount: number;
  community: Community;
  owner: User;
}

const EventDetails: React.FC<EventDetailsProps> = ({
  event,
  participantCount,
  community,
  owner,
}) => {
  const classes = useStyles();

  return (
    <Container className={classes.container}>
      <EventDetailsLeftColumn event={event} community={community} owner={owner} />
      <EventDetailsRightColumn event={event} />
    </Container>
  );
};

export default EventDetails;
