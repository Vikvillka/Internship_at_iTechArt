import { Box } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectCommunityById } from '../../../store/features/communities/communitiesSelectors';
import { communityDetailsStyles } from './CommunityDetails.styles';
import CommunityDetailsDescription from './CommunityDetailsDescription';
import CommunityDetailsOwnerPaper from './CommunityDetailsOwnerPaper';

const CommunityDetailsInfo: React.FC = () => {
  const community = useSelector(selectCommunityById);

  return (
    <Box sx={communityDetailsStyles.infoContainer}>
      <CommunityDetailsDescription name={community?.name} description={community?.description} />
      <CommunityDetailsOwnerPaper />
    </Box>
  );
};

export default CommunityDetailsInfo;
