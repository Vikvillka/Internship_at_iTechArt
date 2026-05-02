import UserIcon from '@mui/icons-material/PersonTwoTone';
import { Box, Paper, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectUserById, selectUserError } from '../../../store/features/users/usersSelectors';
import { communityDetailsStyles } from './CommunityDetails.styles';

const CommunityDetailsOwnerPaper: React.FC = () => {
  const owner = useSelector(selectUserById);
  const ownerError = useSelector(selectUserError);
  return (
    <Paper sx={communityDetailsStyles.ownerContainer}>
      <Typography variant='h6' gutterBottom>
        Organizer
      </Typography>
      {ownerError ? (
        <Typography variant='body1' color='error'>
          {ownerError}
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
