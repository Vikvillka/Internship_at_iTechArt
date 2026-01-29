import { useDispatch, useSelector } from 'react-redux';
import UserCommunityActions from '../../../components/user/userCommunities/UserCommunityActions';
import { selectActiveUser } from '../../../store/features/auth/authSelectors';
import { deleteCommunity } from '../../../store/features/communities/communitiesSlice';

interface Props {
  communityId: string;
}

const UserCommunityActionsContainer: React.FC<Props> = ({ communityId }) => {
  const dispatch = useDispatch();
  const activeUser = useSelector(selectActiveUser);

  const handleDelete = () => {
    if (activeUser) {
      dispatch(deleteCommunity({ communityId, userId: activeUser.id }));
    }
  };

  return <UserCommunityActions communityId={communityId} onDelete={handleDelete} />;
};

export default UserCommunityActionsContainer;
