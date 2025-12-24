import { Box, CardMedia } from '@mui/material';
import React from 'react';
import { getEventImageUrl } from '../../../helpers/getEventImageUrl';
import { Event } from '../../../models/Event';
import { eventDetailsStyles } from './EventDetails.styles';
import EventDetailsPaper from './EventDetailsPaper';
import EventDetailsTags from './EventDetailsTags';

interface Props {
  event: Event;
}

const EventDetailsRightColumn: React.FC<Props> = ({ event }) => {
  const imageUrl = getEventImageUrl(event.imagePath || '');

  return (
    <Box sx={eventDetailsStyles.rightColumn}>
      <CardMedia
        sx={eventDetailsStyles.cardMedia}
        component='img'
        image={imageUrl}
        alt={event.title}
      />
      <EventDetailsTags tags={event.tags} />
      <EventDetailsPaper eventDate={event.eventDate} address={event.address} venue={event.venue} />
    </Box>
  );
};

export default EventDetailsRightColumn;
