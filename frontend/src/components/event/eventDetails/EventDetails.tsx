import { Container } from '@mui/material';
import React from 'react';
import EventDetailsLeftColumnContainer from '../../../containers/event/EventDetailsLeftColumnContainer';
import { eventDetailsStyles } from './EventDetails.styles';
import EventDetailsRightColumn from './EventDetailsRightColumn';

const EventDetails: React.FC = () => {
  return (
    <Container sx={eventDetailsStyles.containerEventDetails}>
      <>
        <EventDetailsLeftColumnContainer />
        <EventDetailsRightColumn />
      </>
    </Container>
  );
};

export default EventDetails;
