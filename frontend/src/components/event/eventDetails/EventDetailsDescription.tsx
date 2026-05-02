import { Box, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectCommunityById } from '../../../store/features/communities/communitiesSelectors';
import { selectEventById } from '../../../store/features/events/eventsSelectors';
import EventVenueMap from '../../event/eventMap/EventVenueMap';
import { eventDetailsStyles } from './EventDetails.styles';

const EventDescriptionDetails: React.FC = () => {
  const event = useSelector(selectEventById);
  const community = useSelector(selectCommunityById);

  return (
    <Box sx={eventDetailsStyles.detailsBox}>
      <Typography sx={eventDetailsStyles.detailTextBold} variant='h5'>
        Details
      </Typography>
      <Typography sx={eventDetailsStyles.description} variant='body2'>
        {event?.description}
      </Typography>
      <Typography sx={eventDetailsStyles.detailTextBold} variant='subtitle1'>
        {event?.address} | {community?.city} | {community?.country}
      </Typography>
      {event?.latitude != null && event?.longitude != null && (
        <EventVenueMap address={event?.address} lat={event?.latitude} lng={event?.longitude} />
      )}
    </Box>
  );
};

export default EventDescriptionDetails;
