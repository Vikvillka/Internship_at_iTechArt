import { Container } from '@mui/material';
import React from 'react';
import EventDetailsLeftColumnContainer from '../../../containers/event/EventDetailsLeftColumnContainer';
import { Event } from '../../../models/Event';
import { useStyles } from './EventDetails.styles';
import EventDetailsRightColumn from './EventDetailsRightColumn';

interface EventDetailsProps {
  event: Event;
  participantCount: number;
}

const EventDetails: React.FC<EventDetailsProps> = ({ event, participantCount }) => {
  const classes = useStyles();

  return (
    <Container className={classes.container}>
      <EventDetailsLeftColumnContainer event={event} />
      <EventDetailsRightColumn event={event} />
    </Container>
  );
};

export default EventDetails;
