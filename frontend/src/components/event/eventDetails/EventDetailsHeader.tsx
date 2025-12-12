import PeopleIcon from '@mui/icons-material/PeopleAltTwoTone';
import { Box, Typography } from '@mui/material';
import React from 'react';
import { useStyles } from './EventDetails.styles';

interface EventHeaderProps {
  title: string;
  ownerName: string;
  communityName: string;
}

const EventDetailsHeader: React.FC<EventHeaderProps> = ({ title, ownerName, communityName }) => {
  const classes = useStyles();

  return (
    <Box>
      <Typography className={classes.title} variant='h4'>
        {title}
      </Typography>
      <Box className={classes.ownerBox}>
        <PeopleIcon />
        <Typography variant='subtitle1'>
          Hosted by <strong>{ownerName}</strong>
        </Typography>
      </Box>
      <Box className={classes.ownerBox}>
        <Typography variant='subtitle1'>
          Meet the community: <br />
          <strong>{communityName}</strong>
        </Typography>
      </Box>
    </Box>
  );
};

export default EventDetailsHeader;
