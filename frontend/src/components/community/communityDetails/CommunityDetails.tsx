import { Container } from '@mui/material';
import React from 'react';
import { Community } from '../../../models/Community';

interface CommunityDetailsProps {
  community: Community;
  subscriptionCount: number;
}

const CommunityDetails: React.FC<CommunityDetailsProps> = ({ community, subscriptionCount }) => {
  return (
    <Container>
      <h1>{community.name}</h1>
    </Container>
  );
};

export default CommunityDetails;
