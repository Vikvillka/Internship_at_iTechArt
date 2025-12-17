import MembersIcon from '@mui/icons-material/Group';
import LocationIcon from '@mui/icons-material/LocationOn';
import { Box, Chip, Typography } from '@mui/material';
import React from 'react';
import { getRandomColor } from '../../../helpers/getRandomColor';
import { Community } from '../../../models/Community';

interface Props {
  community: Community;
  participantCount?: number;
}

const CommunityDetailsHeader: React.FC<Props> = ({ community, participantCount }) => {
  return (
    <Box>
      <Typography variant='h4'>{community.name}</Typography>
      <Box>
        <LocationIcon />
        <Typography variant='subtitle1'>
          {community.city}, {community.country}
        </Typography>
      </Box>
      <Box>
        <MembersIcon />
        <Typography variant='subtitle1'>{participantCount} members</Typography>
      </Box>
      <Chip
        label={community.category}
        size='medium'
        variant='outlined'
        sx={{ background: getRandomColor() }}
      />
    </Box>
  );
};

export default CommunityDetailsHeader;
