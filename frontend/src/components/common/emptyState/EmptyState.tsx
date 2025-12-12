import { Box, Typography } from '@mui/material';
import React from 'react';
import { emptyStateStyles } from './EmptyState.styles';

interface EmptyStateProps {
  message: string;
}

const EmptyState: React.FC<EmptyStateProps> = ({ message }) => {
  return (
    <Box sx={{ ...emptyStateStyles.container }}>
      <Typography variant='h6'>{message}</Typography>
    </Box>
  );
};

export default EmptyState;
