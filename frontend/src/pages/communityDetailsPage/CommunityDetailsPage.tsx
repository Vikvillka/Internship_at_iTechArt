import { Box, CircularProgress, Container, Typography } from '@mui/material';
import CommunityDetails from '../../components/community/communityDetails/CommunityDetails';
import { Community } from '../../models/Community';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';

interface Props {
  community: Community | null;
  subscriptionCount: number;
  loading: boolean;
  error: string | null;
}

const CommunityDetailsPage: React.FC<Props> = ({
  community,
  subscriptionCount,
  loading,
  error,
}) => {
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
      {community && (
        <CommunityDetails community={community} subscriptionCount={subscriptionCount} />
      )}
    </Container>
  );
};

export default CommunityDetailsPage;
