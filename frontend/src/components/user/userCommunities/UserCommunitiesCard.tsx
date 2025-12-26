import CityIcon from '@mui/icons-material/LocationCity';
import { Box, Card, CardContent, Typography } from '@mui/material';
import React from 'react';
import { Link } from 'react-router-dom';
import { Community } from '../../../models/Community';
import { communityCardStyles } from '../../community/communityCard/CommunityCard.styles';

interface UserCommunitiesCardProps {
  community: Community;
  // subscriberCount: number;
}

const UserCommunitiesCard: React.FC<UserCommunitiesCardProps> = ({ community }) => {
  return (
    <Link to={`/community/${community.id}`} style={{ textDecoration: 'none' }}>
      <Card sx={communityCardStyles.card}>
        <CardContent>
          <Typography variant='h6' sx={communityCardStyles.title}>
            {community.name}
          </Typography>
          <Box sx={communityCardStyles.iconLableBox}>
            <CityIcon fontSize='small' sx={{ mr: 1 }} />
            <Typography variant='body2'>
              {community.city}, {community.country}
            </Typography>
          </Box>
          {/* <Box sx={communityCardStyles.subscription}>
                        <PeopleIcon fontSize='small' sx={{ mr: 1 }} />
                        <Typography variant='body2'>{subscriberCount} subscribers</Typography>
                    </Box> */}
        </CardContent>
      </Card>
    </Link>
  );
};

export default UserCommunitiesCard;
