import { Box, Typography } from '@mui/material';
import React from 'react';
import { communityDetailsStyles } from './CommunityDetails.styles';

interface Props {
  name: string | undefined;
  description: string | undefined;
}

const CommunityDetailsDescription: React.FC<Props> = ({ name, description }) => {
  return (
    <Box sx={communityDetailsStyles.descriptionContainer}>
      <Typography variant='h5' sx={communityDetailsStyles.titleSection}>
        What we’re about
      </Typography>
      <Typography variant='body1'>{description}</Typography>
    </Box>
  );
};

export default CommunityDetailsDescription;
