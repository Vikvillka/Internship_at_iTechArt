import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import PeopleIcon from '@mui/icons-material/People';
import { Button, Paper, Typography, Box } from '@mui/material';
import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { formatDate } from '../../../helpers/formatDate';
import { Event } from '../../../models/Event';
import {
  selectActiveUser,
  selectIsLoggedIn,
} from '../../../store/features/auth/authSelectors';
import {
  selectIsParticipatingInEvent,
  selectParticipationIsMutationLoading,
} from '../../../store/features/participation/participationSelectors';
import { joinEventParticipation } from '../../../store/features/participation/participationSlice';
import { eventDetailsStyles } from './EventDetails.styles';

interface EventParticipationActionBarProps {
  event: Event | null;
  participantCount: number;
}

const EventParticipationActionBar: React.FC<EventParticipationActionBarProps> = ({
  event,
  participantCount,
}) => {
  const dispatch = useDispatch();
  const isLoggedIn = useSelector(selectIsLoggedIn);
  const user = useSelector(selectActiveUser);
  const isMutationLoading = useSelector(selectParticipationIsMutationLoading);
  const isParticipating = useSelector(selectIsParticipatingInEvent(event?.id || ''));

  if (!isLoggedIn || !user?.id || !event) {
    return null;
  }

  const handleAttendClick = () => {
    if (isParticipating) {
      return;
    }

    dispatch(
      joinEventParticipation({
        userId: user.id,
        eventId: event.id,
      }),
    );
  };

  return (
    <Box sx={eventDetailsStyles.actionBarWrapper}>
      <Paper sx={eventDetailsStyles.actionBar}>
        <Box sx={eventDetailsStyles.actionBarEventInfo}>
          <Typography variant='subtitle1' fontWeight={700}>
            {event.title}
          </Typography>
          <Box sx={eventDetailsStyles.actionBarMeta}>
            <Box sx={eventDetailsStyles.actionBarMetaItem}>
              <CalendarTodayIcon fontSize='small' />
              <Typography variant='body2'>{formatDate(event.eventDate)}</Typography>
            </Box>
            <Box sx={eventDetailsStyles.actionBarMetaItem}>
              <PeopleIcon fontSize='small' />
              <Typography variant='body2'>{participantCount} participants</Typography>
            </Box>
          </Box>
        </Box>

        <Button
          variant='contained'
          onClick={handleAttendClick}
          disabled={isMutationLoading || isParticipating}
        >
          {isParticipating ? 'Attending' : 'Attend'}
        </Button>
      </Paper>
    </Box>
  );
};

export default EventParticipationActionBar;
