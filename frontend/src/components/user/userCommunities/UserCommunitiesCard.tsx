import CityIcon from '@mui/icons-material/LocationCity';
import PeopleIcon from '@mui/icons-material/People';
import { Box, Card, CardContent, Typography } from '@mui/material';
import React from 'react';
import UserCommunityActionsContainer from '../../../containers/user/userCommunities/UserCommunityActionsContainer';
import { Community } from '../../../models/Community';
import { userCommunitiesStyles } from './UserCommunities.styles';

interface UserCommunitiesCardProps {
  community: Community;
  subscriptionCount: number;
}

const UserCommunitiesCard: React.FC<UserCommunitiesCardProps> = ({
  community,
  subscriptionCount,
}) => {
  return (
    <Card sx={userCommunitiesStyles.card}>
      <CardContent>
        <Box>
          <Typography variant='h6' sx={userCommunitiesStyles.cardTitle}>
            {community.name}
          </Typography>
          <Box sx={userCommunitiesStyles.iconLableBox}>
            <CityIcon fontSize='small' sx={{ mr: 1 }} />
            <Typography variant='body2'>
              {community.city}, {community.country}
            </Typography>
          </Box>
          <Box sx={userCommunitiesStyles.iconLableBox}>
            <PeopleIcon fontSize='small' sx={{ mr: 1 }} />
            <Typography variant='body2'>{subscriptionCount} members</Typography>
          </Box>
        </Box>
      </CardContent>
      <Box sx={userCommunitiesStyles.actionsBox}>
        <UserCommunityActionsContainer communityId={community.id} />
      </Box>
    </Card>
  );
};

export default UserCommunitiesCard;
