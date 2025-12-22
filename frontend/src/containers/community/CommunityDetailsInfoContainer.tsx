import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CommunityDetailsInfo from '../../components/community/communityDetails/CommunityDetailsInfo';
import { selectUserById, selectUserError } from '../../store/features/users/usersSelectors';
import { getUserById } from '../../store/features/users/usersSlice';

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
  const dispatch = useDispatch();
  const user = useSelector(selectUserById);
  const error = useSelector(selectUserError);

  useEffect(() => {
    dispatch(getUserById({ id: ownerId, showLoader: false }));
  }, [dispatch, ownerId]);

  return (
    <CommunityDetailsInfo
      communityName={communityName}
      description={description}
      owner={user ?? undefined}
      error={error}
    />
  );
};

export default CommunityDetailsInfoContainer;
