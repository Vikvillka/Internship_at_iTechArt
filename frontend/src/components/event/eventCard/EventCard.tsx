import React, { useEffect, useState } from 'react';
import { Event } from '../../../models/Event';
import { Card, CardContent, Box, CardMedia, Typography } from '@mui/material';
import PeopleIcon from '@mui/icons-material/People';
import { formatDate } from '../../../helpers/formatDate';
import { getEventImageUrl } from '../../../helpers/getEventImageUrl';
import { useStyles } from './EventCard.styles';
import { participationApi } from '../../../api/participation';

interface EventCardProps {
  event: Event;
}

const EventCard: React.FC<EventCardProps> = ({ event }) => {
  const classes = useStyles();
  const imageUrl = getEventImageUrl(event.imagePath || '');
  const [participantCount, setParticipantCount] = useState<number>(0);

  useEffect(() => {
    participationApi.getParticipationCounts(event.id)
      .then((data) => {
        if (data.length > 0) {
          setParticipantCount(data[0].count);
        }
      });
  }, [event.id]);

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
        <Box className={classes.participants}>
          <PeopleIcon fontSize='small' sx={{ mr: 1 }} />
          <Typography variant='body2'>{participantCount} attendees</Typography>
        </Box>
      </CardContent>
    </Card>
  );
};

export default EventCard;
