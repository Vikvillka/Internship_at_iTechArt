import CalendarTodayIcon from '@mui/icons-material/CalendarMonth';
import { getRandomColor } from '../../../helpers/getRandomColor';
import VenueIcon from '@mui/icons-material/HomeWork';
import PeopleIcon from '@mui/icons-material/PersonTwoTone';
import AddressIcon from '@mui/icons-material/Place';
import { Box, CardMedia, Chip, Container, Divider, Paper, Typography } from '@mui/material';
import React from 'react';
import { formatDate } from '../../../helpers/formatDate';
import { getEventImageUrl } from '../../../helpers/getEventImageUrl';
import { Community } from '../../../models/Community';
import { Event } from '../../../models/Event';
import { User } from '../../../models/User';
import { useStyles } from './EventDetails.styles';

interface EventDetailsProps {
  event: Event;
  participantCount: number;
  community: Community;
  owner: User;
}

const EventDetails: React.FC<EventDetailsProps> = ({
  event,
  participantCount,
  community,
  owner,
}) => {
  const classes = useStyles();
  const imageUrl = getEventImageUrl(event.imagePath || '');

  return (
    <Container className={classes.container}>
      <Box className={classes.leftColumn}>
        <Typography className={classes.title} variant='h4'>
          {event.title}
        </Typography>

        <Box className={classes.ownerBox}>
          <PeopleIcon />
          <Typography className={classes.ownerBox} variant='subtitle1'>
            Hosted by <strong>{owner.username}</strong>
          </Typography>
        </Box>
        <Typography variant='subtitle1'>
          Meet the community: <br />
          <strong>{community.name}</strong>
        </Typography>

        <Box className={classes.detailsBox}>
          <Typography className={classes.detailTextBold} variant='h5'>
            Details
          </Typography>
          <Typography className={classes.description} variant='body2'>
            {event.description}
          </Typography>
        </Box>
        <Typography className={classes.detailTextBold} variant='subtitle1'>
          {event.address} | {community.city} | {community.country}
        </Typography>
      </Box>

      <Box className={classes.rightColumn}>
        <CardMedia
          className={classes.cardMedia}
          component='img'
          image={imageUrl}
          alt={event.title}
        />
        <Typography variant='h5' className={classes.detailTextBold}>
          What's interesting about us?
        </Typography>
        <Box className={classes.tagsBox}>
          {event.tags.map((tag) => (
            <Chip
              className={classes.chip}
              key={tag.id}
              label={tag.name}
              size='medium'
              variant='outlined'
              sx={{ backgroundColor: getRandomColor() }}
            />
          ))}
        </Box>

        <Paper className={classes.paperBox}>
          <Box className={classes.dateBox}>
            <CalendarTodayIcon fontSize='small' />
            <Typography variant='body1'>{formatDate(event.eventDate)}</Typography>
          </Box>
          <Divider sx={{ my: 3 }} />
          <Box className={classes.dateBox}>
            <AddressIcon fontSize='small' />
            <Typography variant='body1'>{event.address}</Typography>
          </Box>
          <Divider sx={{ my: 3 }} />
          <Box className={classes.dateBox}>
            <VenueIcon fontSize='small' />
            <Typography variant='subtitle1'>{event.venue}</Typography>
          </Box>
        </Paper>
      </Box>
    </Container>
  );
};

export default EventDetails;
