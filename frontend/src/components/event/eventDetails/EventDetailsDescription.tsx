import { Box, Typography } from '@mui/material';
import React from 'react';
import EventVenueMap from '../../event/eventMap/EventVenueMap';
import { eventDetailsStyles } from './EventDetails.styles';

interface EventDescriptionProps {
  description: string;
  address: string;
  city?: string | null;
  country?: string | null;
  lat?: number;
  lng?: number;
  error?: string | null;
}

const EventDescriptionDetails: React.FC<EventDescriptionProps> = ({
  description,
  address,
  city,
  country,
  lat,
  lng,
  error,
}) => {
  return (
    <Box sx={eventDetailsStyles.detailsBox}>
      <Typography sx={eventDetailsStyles.detailTextBold} variant='h5'>
        Details
      </Typography>
      <Typography sx={eventDetailsStyles.description} variant='body2'>
        {description}
      </Typography>
      <Typography sx={eventDetailsStyles.detailTextBold} variant='subtitle1'>
        {address} | {city} | {country}
      </Typography>
      {lat != null && lng != null && <EventVenueMap address={address} lat={lat} lng={lng} />}
    </Box>
  );
};

export default EventDescriptionDetails;
