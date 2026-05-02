import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import UserProfilePage from '../../../pages/userProfilePage/UserProfilePage';
import { selectActiveUser } from '../../../store/features/auth/authSelectors';
import {
  selectParticipationError,
  selectParticipationIsLoading,
  selectParticipations,
} from '../../../store/features/participation/participationSelectors';
import { getUserParticipations } from '../../../store/features/participation/participationSlice';

const UserProfileContainer: React.FC = () => {
  const dispatch = useDispatch();
  const user = useSelector(selectActiveUser);
  const participations = useSelector(selectParticipations);
  const error = useSelector(selectParticipationError);
  const isLoading = useSelector(selectParticipationIsLoading);

  useEffect(() => {
    if (user?.id) {
      dispatch(getUserParticipations({ userId: user.id }));
    }
  }, [dispatch, user?.id]);

  return <UserProfilePage participations={participations} isLoading={isLoading} error={error} />;
};

export default UserProfileContainer;
