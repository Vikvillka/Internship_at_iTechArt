import { useEffect, useState } from 'react';
import { userApi } from '../../api/user';
import CommunityDetailsInfo from '../../components/community/communityDetails/CommunityDetailsInfo';
import { User } from '../../models/User';

interface Props {
  ownerId: string;
  communityName: string;
  description: string;
}

const CommunityDetailsInfoContainer: React.FC<Props> = ({
  ownerId,
  communityName,
  description,
}) => {
  const [owner, setOwner] = useState<User | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const ownerData = await userApi.getUserById(ownerId);
        setOwner(ownerData);
      } catch (err: any) {
        setError(err.message || 'Failed to fetch community owner details');
      }
    };
    fetchData();
  }, [ownerId]);

  return (
    <CommunityDetailsInfo
      communityName={communityName}
      description={description}
      owner={owner ?? undefined}
      error={error}
    />
  );
};

export default CommunityDetailsInfoContainer;
