import { Box, Typography } from '@mui/material';
import { SxProps, Theme } from '@mui/material/styles';
import React from 'react';

interface EventDetailItemProps {
  icon: React.ReactNode;
  children: React.ReactNode;
  sx?: SxProps<Theme>;
}

const EventDetailItem: React.FC<EventDetailItemProps> = ({ icon, children, sx }) => {
  return (
    <Box display='flex' alignItems='center' gap={1} sx={sx}>
      {icon}
      <Typography variant='body1'>{children}</Typography>
    </Box>
  );
};

export default EventDetailItem;
