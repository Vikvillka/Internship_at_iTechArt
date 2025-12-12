import { Box, Chip, Typography } from '@mui/material';
import React from 'react';
import { getRandomColor } from '../../../helpers/getRandomColor';
import { useStyles } from './EventDetails.styles';

interface EventTagsProps {
  tags: { id: string; name: string }[];
}

const EventDetailsTags: React.FC<EventTagsProps> = ({ tags }) => {
  const classes = useStyles();

  return (
    <Box>
      <Typography variant='h5' className={classes.detailTextBold}>
        What's interesting about us?
      </Typography>
      <Box className={classes.tagsBox}>
        {tags.map((tag) => (
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
    </Box>
  );
};

export default EventDetailsTags;
