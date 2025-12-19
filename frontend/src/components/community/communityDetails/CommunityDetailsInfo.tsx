import { Box } from '@mui/material';
import React from 'react';
import { User } from '../../../models/User';
import { communityDetailsStyles } from './CommunityDetails.styles';
import CommunityDetailsDescription from './CommunityDetailsDescription';
import CommunityDetailsOwnerPaper from './CommunityDetailsOwnerPaper';

interface Props {
  communityName: string;
  description: string;
  owner?: User;
  error?: string | null;
}

const CommunityDetailsInfo: React.FC<Props> = ({ communityName, description, owner, error }) => {
  return (
    <Box sx={communityDetailsStyles.infoContainer}>
      <CommunityDetailsDescription name={communityName} description={description} />
      <CommunityDetailsOwnerPaper owner={owner} error={error} />
    </Box>
  );
};

export default CommunityDetailsInfo;
