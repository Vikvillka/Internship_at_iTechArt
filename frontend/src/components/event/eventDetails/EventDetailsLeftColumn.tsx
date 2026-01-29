import { Box } from '@mui/material';
import React from 'react';
import { eventDetailsStyles } from './EventDetails.styles';
import EventDescriptionDetails from './EventDetailsDescription';
import EventDetailsHeader from './EventDetailsHeader';

interface Props {
  error?: string | null;
}

const EventDetailsLeftColumn: React.FC<Props> = ({ error }) => {
  return (
    <Box sx={eventDetailsStyles.leftColumn}>
      <EventDetailsHeader error={error ?? undefined} />
      <EventDescriptionDetails />
    </Box>
  );
};

export default EventDetailsLeftColumn;
