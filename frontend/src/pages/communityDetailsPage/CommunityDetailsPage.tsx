import { Box, Container, Typography } from '@mui/material';
import CommunityDetails from '../../components/community/communityDetails/CommunityDetails';
import CommunityDetailsEventsListContainer from '../../containers/community/CommunityDetailsEventsListContainer';
import CommunityDetailsInfoContainer from '../../containers/community/CommunityDetailsInfoContainer';
import { Community } from '../../models/Community';
import { errorContainer, errorText, pageContainer } from '../../styles/common';

interface Props {
  community: Community | null;
  subscriptionCount: number;
  loading: boolean;
  error: string | null;
}

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
      {community && (
        <Box>
          <CommunityDetails community={community} subscriptionCount={subscriptionCount} />
          <CommunityDetailsInfoContainer
            ownerId={community.ownerId}
            communityName={community.name}
            description={community.description}
          />
          <CommunityDetailsEventsListContainer communityId={community.id} />
        </Box>
      )}
    </Container>
  );
};

export default CommunityDetailsPage;
