import PeopleIcon from '@mui/icons-material/PeopleAltTwoTone';
import { Box, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectCommunityById } from '../../../store/features/communities/communitiesSelectors';
import { selectEventById } from '../../../store/features/events/eventsSelectors';
import { selectUserById } from '../../../store/features/users/usersSelectors';
import { eventDetailsStyles } from './EventDetails.styles';

interface EventHeaderProps {
  error?: string | null;
}

const EventDetailsHeader: React.FC<EventHeaderProps> = ({ error }) => {
  const event = useSelector(selectEventById);
  const community = useSelector(selectCommunityById);
  const owner = useSelector(selectUserById);

  return (
    <Box>
      <Typography sx={eventDetailsStyles.title} variant='h4'>
        {event?.title}
      </Typography>
      {error && (
        <Typography color='error' variant='subtitle2'>
          {error}
        </Typography>
      )}
      <Box sx={eventDetailsStyles.ownerBox}>
        <PeopleIcon />
        <Typography variant='subtitle1'>
          Hosted by <strong>{owner?.username}</strong>
        </Typography>
      </Box>
      <Box sx={eventDetailsStyles.ownerBox}>
        <Typography variant='subtitle1'>
          Meet the community: <br />
          <strong>{community?.name}</strong>
        </Typography>
      </Box>
    </Box>
  );
};

export default EventDetailsHeader;
