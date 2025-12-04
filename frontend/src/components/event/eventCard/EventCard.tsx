import React from 'react';
import { Event } from '../../../models/Event';
import { Card, CardContent, Box, CardMedia, Typography } from '@mui/material';
import PeopleIcon from '@mui/icons-material/People';
import { formatDate } from '../../../helpers/formatDate';
import { getEventImageUrl } from '../../../helpers/getEventImageUrl';
import { useStyles } from './EventCard.styles';

interface EventCardProps {
  event: Event;
}

const EventCard: React.FC<EventCardProps> = ({ event }) => {
  const classes = useStyles();
  const imageUrl = getEventImageUrl(event.imagePath || '');

  return (
    <Card className={classes.card}>
      <CardMedia className={classes.cardMedia} component='img' image={imageUrl} alt={event.title} />
      <CardContent>
        <Typography variant='subtitle2' color='text.secondary'>
          {formatDate(event.eventDate)}
        </Typography>
        <Typography className={classes.title} variant='body1'>
          {event.title}
        </Typography>
        <Typography className={classes.communityName} variant='subtitle2' color='text.secondary'>
          {event.communityName}
        </Typography>
        {/* I forgot to get the API data on how many participants there are. That's why it's a stub now */}
        <Box className={classes.participants}>
          <PeopleIcon fontSize='small' sx={{ mr: 1 }} />
          <Typography variant='body2'>{event.duration || 0} attendees</Typography>
        </Box>
      </CardContent>
    </Card>
  );
};

export default EventCard;
