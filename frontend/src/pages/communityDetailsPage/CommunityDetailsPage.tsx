import { Box, CircularProgress, Container, Typography } from '@mui/material';
import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import CommunityDetailsHeader from '../../components/community/communityDetails/CommunityDetailsHeader';
import CommunityDetailsEventsListContainer from '../../containers/community/CommunityDetailsEventsListContainer';
import CommunityDetailsInfoContainer from '../../containers/community/CommunityDetailsInfoContainer';
import {
  selectCommunitiesError,
  selectCommunityById,
} from '../../store/features/communities/communitiesSelectors';
import { getCommunityById } from '../../store/features/communities/communitiesSlice';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';
import { errorContainer, errorText, pageContainer } from '../../styles/common';

const CommunityDetailsPage: React.FC = () => {
  const dispatch = useDispatch();
  const { communityId } = useParams<{ communityId: string }>();

  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectCommunitiesError);
  const community = useSelector(selectCommunityById);

  const handleRetry = () => {
    if (communityId) {
      dispatch(getCommunityById({ id: communityId, showLoader: true }));
    }
  };

  if (loading && !community) {
    return (
      <Container sx={pageContainer}>
        <Box sx={{ display: 'flex', justifyContent: 'center', py: 4 }}>
          <CircularProgress />
        </Box>
      </Container>
    );
  }

  if (error) {
    return (
      <Container sx={errorContainer}>
        <Typography variant='h6' sx={errorText}>
          {error}
        </Typography>
        <Typography
          variant='body2'
          onClick={handleRetry}
          sx={{ cursor: 'pointer', textDecoration: 'underline' }}
        >
          Retry
        </Typography>
      </Container>
    );
  }

  if (!community) {
    return (
      <Container sx={errorContainer}>
        <Typography variant='h6' sx={errorText}>
          Community not found.
        </Typography>
      </Container>
    );
  }

  return (
    <Container sx={pageContainer}>
      <Box>
        <CommunityDetailsHeader />
        <CommunityDetailsInfoContainer />
        <CommunityDetailsEventsListContainer />
      </Box>
    </Container>
  );
};

export default CommunityDetailsPage;
