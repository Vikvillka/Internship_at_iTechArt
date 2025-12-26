import { Box, CardMedia } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { getEventImageUrl } from '../../../helpers/getEventImageUrl';
import { selectEventById } from '../../../store/features/events/eventsSelectors';
import { eventDetailsStyles } from './EventDetails.styles';
import EventDetailsPaper from './EventDetailsPaper';
import EventDetailsTags from './EventDetailsTags';

const EventDetailsRightColumn: React.FC = () => {
  const event = useSelector(selectEventById);

  const imageUrl = getEventImageUrl(event?.imagePath || '');

  return (
    <Box sx={eventDetailsStyles.rightColumn}>
      <CardMedia
        sx={eventDetailsStyles.cardMedia}
        component='img'
        image={imageUrl}
        alt={event?.title}
      />
      <EventDetailsTags tags={event?.tags} />
      <EventDetailsPaper
        eventDate={event?.eventDate}
        address={event?.address}
        venue={event?.venue}
      />
    </Box>
  );
};

export default EventDetailsRightColumn;
