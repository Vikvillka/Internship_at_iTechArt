import LocationIcon from '@mui/icons-material/LocationOn';
import MembersIcon from '@mui/icons-material/PeopleTwoTone';
import { Box, Chip, Typography } from '@mui/material';
import React from 'react';
import { getRandomColor } from '../../../helpers/getRandomColor';
import { Community } from '../../../models/Community';
import { communityDetailsStyles } from './CommunityDetails.styles';

interface Props {
  community: Community;
  subscriptionCount?: number | undefined;
}

const CommunityDetailsHeader: React.FC<Props> = ({ community, subscriptionCount }) => {
  return (
    <Box>
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
    </Box>
  );
};

export default CommunityDetailsHeader;
