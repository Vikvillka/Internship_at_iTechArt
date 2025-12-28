import CityIcon from '@mui/icons-material/LocationCity';
import PeopleIcon from '@mui/icons-material/People';
import { Box, Card, CardContent, Typography } from '@mui/material';
import React from 'react';
import { Link } from 'react-router-dom';
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
    <Link to={`/community/${community.id}`} style={{ textDecoration: 'none' }}>
      <Card sx={userCommunitiesStyles.card}>
        <CardContent>
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
        </CardContent>
      </Card>
    </Link>
  );
};

export default UserCommunitiesCard;
