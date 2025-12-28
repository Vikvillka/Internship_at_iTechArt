import { Grid } from '@mui/material';
import { useSelector } from 'react-redux';
import EmptyState from '../../../components/common/emptyState/EmptyState';
import {
  selectCommunities,
  selectCommunitySubscriptionCounts,
} from '../../../store/features/communities/communitiesSelectors';
import UserCommunitiesCard from './UserCommunitiesCard';

const UserCommunitiesPage: React.FC = () => {
  const communities = useSelector(selectCommunities);
  const subscriberCounts = useSelector(selectCommunitySubscriptionCounts);

  if (communities.length === 0) return <EmptyState message='No communities found' />;

  return (
    <Grid container spacing={2}>
      {communities.map((community) => (
        <Grid key={community.id} size={{ xs: 12, sm: 6, md: 3 }}>
          <UserCommunitiesCard
            community={community}
            subscriptionCount={subscriberCounts[community.id] || 0}
          />
        </Grid>
      ))}
    </Grid>
  );
};

export default UserCommunitiesPage;
