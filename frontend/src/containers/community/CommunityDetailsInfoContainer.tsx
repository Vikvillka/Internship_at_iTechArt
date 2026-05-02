import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CommunityDetailsInfo from '../../components/community/communityDetails/CommunityDetailsInfo';
import { selectCommunityById } from '../../store/features/communities/communitiesSelectors';
import { getUserById } from '../../store/features/users/usersSlice';

const CommunityDetailsInfoContainer: React.FC = () => {
  const dispatch = useDispatch();
  const community = useSelector(selectCommunityById);
  const ownerId = community?.ownerId;

  useEffect(() => {
    if (ownerId) {
      dispatch(getUserById({ id: ownerId }));
    }
  }, [dispatch, ownerId]);

  return <CommunityDetailsInfo />;
};

export default CommunityDetailsInfoContainer;
