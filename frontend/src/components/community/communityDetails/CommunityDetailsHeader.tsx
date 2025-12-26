import LocationIcon from '@mui/icons-material/LocationOn';
import MembersIcon from '@mui/icons-material/PeopleTwoTone';
import { Box, Chip, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { getRandomColor } from '../../../helpers/getRandomColor';
import {
  selectCommunityById,
  selectCommunitySubscriptionCounts,
} from '../../../store/features/communities/communitiesSelectors';
import { communityDetailsStyles } from './CommunityDetails.styles';

const CommunityDetailsHeader: React.FC = () => {
  const community = useSelector(selectCommunityById);
  const subscriptionCount =
    useSelector(selectCommunitySubscriptionCounts)[community?.id || ''] || 0;

  return (
    <Box>
      {community && (
        <>
          <Typography variant='h4'>{community.name}</Typography>
          <Box sx={communityDetailsStyles.infoBox}>
            <LocationIcon />
            <Typography variant='subtitle1'>
              {community.city}, {community.country}
            </Typography>
          </Box>
          <Box sx={communityDetailsStyles.infoBox}>
            <MembersIcon />
            <Typography variant='subtitle1'>{subscriptionCount} members</Typography>
          </Box>
          <Chip
            label={community.category}
            size='medium'
            variant='outlined'
            sx={{ ...communityDetailsStyles.chip, backgroundColor: getRandomColor() }}
          />
        </>
      )}
    </Box>
  );
};

export default CommunityDetailsHeader;
