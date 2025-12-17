import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { communityApi } from '../../api/community';
import { subscriptionApi } from '../../api/subscription';
import { Community } from '../../models/Community';
import CommunityDetailsPage from '../../pages/communityDetailsPage/CommunityDetailsPage';

const CommunityDetailsPageContainer: React.FC = () => {
  const { communityId } = useParams();
  const [community, setCommunity] = useState<Community | null>(null);
  const [subscriptionCount, setSubscriptionCount] = useState<number>(0);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCommunityDetails = async () => {
      try {
        const communityData = await communityApi.getCommunityById(communityId!);
        setCommunity(communityData);

        const counts = await subscriptionApi.getSubscriptionCounts([communityId!]);
        setSubscriptionCount(counts.length > 0 ? counts[0].count : 0);
      } catch (err: any) {
        setError(err.message || 'Failed to fetch community details');
      } finally {
        setLoading(false);
      }
    };

    fetchCommunityDetails();
  }, [communityId]);

  return (
    <CommunityDetailsPage
      community={community}
      subscriptionCount={subscriptionCount}
      loading={loading}
      error={error}
    />
  );
};

export default CommunityDetailsPageContainer;
