import CalendarTodayIcon from '@mui/icons-material/CalendarMonth';
import VenueIcon from '@mui/icons-material/HomeWork';
import AddressIcon from '@mui/icons-material/Place';
import { Divider, Paper } from '@mui/material';
import React from 'react';
import { formatDate } from '../../../helpers/formatDate';
import { eventDetailsStyles } from './EventDetails.styles';
import EventDetailItem from './EventDetailsItem';

interface EventDetailsPaperProps {
  eventDate: string;
  address: string;
  venue: string;
}

const EventDetailsPaper: React.FC<EventDetailsPaperProps> = ({ eventDate, address, venue }) => {
  return (
    <Paper sx={eventDetailsStyles.paperBox}>
      <EventDetailItem icon={<CalendarTodayIcon fontSize='small' />}>
        {formatDate(eventDate)}
      </EventDetailItem>
      <Divider sx={{ my: 3 }} />
      <EventDetailItem icon={<AddressIcon fontSize='small' />}>{address}</EventDetailItem>
      <Divider sx={{ my: 3 }} />
      <EventDetailItem icon={<VenueIcon fontSize='small' />} sx={{ typography: 'subtitle1' }}>
        {venue}
      </EventDetailItem>
    </Paper>
  );
};

export default EventDetailsPaper;
