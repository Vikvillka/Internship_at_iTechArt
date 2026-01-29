import PeopleIcon from '@mui/icons-material/People';
import { Box, Card, CardContent, CardMedia, Typography } from '@mui/material';
import React from 'react';
import { Link } from 'react-router-dom';
import { formatDate } from '../../../helpers/formatDate';
import { getEventImageUrl } from '../../../helpers/getEventImageUrl';
import { Event } from '../../../models/Event';
import { cardStyles } from './EventCard.styles';

interface EventCardProps {
  event: Event;
  participantCount: number;
}

const EventCard: React.FC<EventCardProps> = ({ event, participantCount }) => {
  const imageUrl = getEventImageUrl(event.imagePath || '');

  return (
    <Link style={{ textDecoration: 'none' }} to={`/event/${event.id}`}>
      <Card sx={cardStyles.card}>
        <CardMedia sx={cardStyles.cardMedia} component='img' image={imageUrl} alt={event.title} />
        <CardContent>
          <Typography variant='subtitle2' color='text.secondary'>
            {formatDate(event.eventDate)}
          </Typography>
          <Typography sx={cardStyles.title} variant='body1'>
            {event.title}
          </Typography>
          <Typography sx={cardStyles.communityName} variant='subtitle2' color='text.secondary'>
            {event.communityName}
          </Typography>
          <Box sx={cardStyles.participants}>
            <PeopleIcon fontSize='small' sx={{ mr: 1 }} />
            <Typography variant='body2'>{participantCount} attendees</Typography>
          </Box>
        </CardContent>
      </Card>
    </Link>
  );
};

export default EventCard;
