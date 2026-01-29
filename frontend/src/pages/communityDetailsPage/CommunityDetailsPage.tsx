import { Box, Container, Typography } from '@mui/material';
import CommunityDetails from '../../components/community/communityDetails/CommunityDetails';
import CommunityDetailsEventsListContainer from '../../containers/community/CommunityDetailsEventsListContainer';
import CommunityDetailsInfoContainer from '../../containers/community/CommunityDetailsInfoContainer';
import { Community } from '../../models/Community';
import { errorContainer, errorText, pageContainer } from '../../styles/common';

const CommunityDetailsPage: React.FC = () => {
  const loading = useSelector(selectIsLoading);
  const error = useSelector(selectCommunitiesError);

const CommunityDetailsPage: React.FC<Props> = ({ community, subscriptionCount, error }) => {
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
