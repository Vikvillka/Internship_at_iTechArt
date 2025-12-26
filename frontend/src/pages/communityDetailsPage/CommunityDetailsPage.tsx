import { Box, CircularProgress, Container, Typography } from '@mui/material';
import { useSelector } from 'react-redux';
import CommunityDetailsHeader from '../../components/community/communityDetails/CommunityDetailsHeader';
import CommunityDetailsEventsListContainer from '../../containers/community/CommunityDetailsEventsListContainer';
import CommunityDetailsInfoContainer from '../../containers/community/CommunityDetailsInfoContainer';
import { selectCommunitiesError } from '../../store/features/communities/communitiesSelectors';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';

const CommunityDetailsPage: React.FC = () => {
  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectCommunitiesError);

  if (loading) {
    return (
      <Box sx={loadingBox}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Container sx={errorContainer}>
        <Typography variant='h6' sx={errorText}>
          {error}
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
