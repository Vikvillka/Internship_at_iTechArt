import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import UserCommunitiesPage from '../../../pages/userCommunitiesPage/UserCommunitiesPage';
import { selectActiveUser } from '../../../store/features/auth/authSelectors';
import { getUserCommunities } from '../../../store/features/communities/communitiesSlice';

const UserCommunitiesContainer: React.FC = () => {
  const dispatch = useDispatch();
  const user = useSelector(selectActiveUser);

  useEffect(() => {
    if (user?.id) {
      dispatch(getUserCommunities({ userId: user.id, showLoader: true }));
    }
  }, [user?.id, dispatch]);

  return <UserCommunitiesPage />;
};

export default UserCommunitiesContainer;
