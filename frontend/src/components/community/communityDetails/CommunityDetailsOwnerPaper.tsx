import UserIcon from '@mui/icons-material/PersonTwoTone';
import { Box, Paper, Typography } from '@mui/material';
import React from 'react';
import { User } from '../../../models/User';
import { communityDetailsStyles } from './CommunityDetails.styles';

interface Props {
  owner?: User;
  error?: string | null;
}

const CommunityDetailsOwnerPaper: React.FC<Props> = ({ owner, error }) => {
  return (
    <Paper sx={communityDetailsStyles.ownerContainer}>
      <Typography variant='h6' gutterBottom>
        Organizer
      </Typography>
      {error ? (
        <Typography variant='body1' color='error'>
          {error}
        </Typography>
      ) : owner ? (
        <Box sx={communityDetailsStyles.ownerNameIconBox}>
          <UserIcon fontSize='large' />
          <Typography variant='subtitle1'>
            <strong>{owner.username}</strong>
          </Typography>
        </Box>
      ) : (
        <Typography variant='body1' color='textSecondary'>
          No owner information available.
        </Typography>
      )}
    </Paper>
  );
};

export default CommunityDetailsOwnerPaper;
