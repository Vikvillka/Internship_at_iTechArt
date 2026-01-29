import { Box, Chip, Typography } from '@mui/material';
import React from 'react';
import { getRandomColor } from '../../../helpers/getRandomColor';
import { eventDetailsStyles } from './EventDetails.styles';

interface EventTagsProps {
  tags?: { id: string; name: string }[];
}

const EventDetailsTags: React.FC<EventTagsProps> = ({ tags }) => {
  return (
    <Box>
      <Typography variant='h5' sx={eventDetailsStyles.detailTextBold}>
        What's interesting about us?
      </Typography>
      <Box sx={eventDetailsStyles.tagsBox}>
        {tags?.map((tag) => (
          <Chip
            sx={{ ...eventDetailsStyles.chip, backgroundColor: getRandomColor() }}
            key={tag.id}
            label={tag.name}
            size='medium'
            variant='outlined'
          />
        ))}
      </Box>
    </Box>
  );
};

export default EventDetailsTags;
