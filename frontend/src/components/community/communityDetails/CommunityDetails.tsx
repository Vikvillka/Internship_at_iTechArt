import React from 'react';
import { Community } from '../../../models/Community';
import CommunityDetailsHeader from './CommunityDetailsHeader';

interface CommunityDetailsProps {
  community: Community;
  subscriptionCount?: number | undefined;
}

const CommunityDetails: React.FC<CommunityDetailsProps> = ({ community, subscriptionCount }) => {
  return <CommunityDetailsHeader community={community} subscriptionCount={subscriptionCount} />;
};

export default CommunityDetails;
