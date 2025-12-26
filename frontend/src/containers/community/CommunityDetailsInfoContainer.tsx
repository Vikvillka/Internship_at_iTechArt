import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CommunityDetailsInfo from '../../components/community/communityDetails/CommunityDetailsInfo';
import { selectCommunityById } from '../../store/features/communities/communitiesSelectors';
import { getUserById } from '../../store/features/users/usersSlice';

const CommunityDetailsInfoContainer: React.FC = () => {
  const dispatch = useDispatch();
  const community = useSelector(selectCommunityById);

  useEffect(() => {
    if (community?.ownerId) dispatch(getUserById({ id: community.ownerId, showLoader: false }));
  }, [dispatch, community?.ownerId]);

  return <CommunityDetailsInfo />;
};

export default CommunityDetailsInfoContainer;
