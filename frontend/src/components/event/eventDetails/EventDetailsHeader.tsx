import PeopleIcon from '@mui/icons-material/PeopleAltTwoTone';
import { Box, Typography } from '@mui/material';
import React from 'react';
import { eventDetailsStyles } from './EventDetails.styles';

interface EventHeaderProps {
  title: string;
  ownerName?: string | null;
  communityName?: string | null;
  error?: string | null;
}

const EventDetailsHeader: React.FC<EventHeaderProps> = ({
  title,
  ownerName,
  communityName,
  error,
}) => {
  return (
    <Box>
      <Typography sx={eventDetailsStyles.title} variant='h4'>
        {title}
      </Typography>
      {error && (
        <Typography color='error' variant='subtitle2'>
          {error}
        </Typography>
      )}
      <Box sx={eventDetailsStyles.ownerBox}>
        <PeopleIcon />
        <Typography variant='subtitle1'>
          Hosted by <strong>{ownerName}</strong>
        </Typography>
      </Box>
      <Box sx={eventDetailsStyles.ownerBox}>
        <Typography variant='subtitle1'>
          Meet the community: <br />
          <strong>{communityName}</strong>
        </Typography>
      </Box>
    </Box>
  );
};

export default EventDetailsHeader;
