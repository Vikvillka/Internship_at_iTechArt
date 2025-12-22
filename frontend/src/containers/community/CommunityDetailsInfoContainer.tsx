import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CommunityDetailsInfo from '../../components/community/communityDetails/CommunityDetailsInfo';
import {
  selectUserDetails,
  selectUserDetailsError,
} from '../../store/features/userDetails/userDetailsSelectors';
import { getUserDetails } from '../../store/features/userDetails/userDetailsSlice';

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
  const user = useSelector(selectUserDetails);
  const error = useSelector(selectUserDetailsError);

  useEffect(() => {
    dispatch(getUserDetails({ id: ownerId, showLoader: false }));
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
