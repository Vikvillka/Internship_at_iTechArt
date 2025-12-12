import { Box, Typography } from '@mui/material';
import React from 'react';
import EventVenueMap from '../../event/eventMap/EventVenueMap';
import { useStyles } from './EventDetails.styles';

interface EventDescriptionProps {
  description: string;
  address: string;
  city: string;
  country: string;
  lat?: number;
  lng?: number;
}

const EventDescriptionDetails: React.FC<EventDescriptionProps> = ({
  description,
  address,
  city,
  country,
  lat,
  lng,
}) => {
  const classes = useStyles();

  return (
    <Box className={classes.detailsBox}>
      <Typography className={classes.detailTextBold} variant='h5'>
        Details
      </Typography>
      <Typography className={classes.description} variant='body2'>
        {description}
      </Typography>
      <Typography className={classes.detailTextBold} variant='subtitle1'>
        {address} | {city} | {country}
      </Typography>
      {lat != null && lng != null && <EventVenueMap address={address} lat={lat} lng={lng} />}
    </Box>
  );
};

export default EventDescriptionDetails;
