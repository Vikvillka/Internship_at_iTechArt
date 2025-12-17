import { Grid } from '@mui/material';
import EmptyState from '../../../components/common/emptyState/EmptyState';
import Pagination from '../../../components/common/pagination/Pagination';
import CommunityCard from '../../../components/community/communityCard/CommunityCard';
import { Community } from '../../../models/Community';

interface Props {
  communities: Community[];
  subscriptionCounts: Record<string, number>;
  page: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

const CommunityListSection: React.FC<Props> = ({
  communities,
  subscriptionCounts,
  page,
  totalPages,
  onPageChange,
}) => {
  if (communities.length === 0) return <EmptyState message='No communities found' />;

  return (
    <>
      <Grid container spacing={1}>
        {communities.map((community) => (
          <Grid key={community.id} size={{ xs: 12, sm: 6, md: 3 }}>
            <CommunityCard
              community={community}
              subscriberCount={subscriptionCounts[community.id] ?? 0}
            />
          </Grid>
        ))}
      </Grid>
      {totalPages > 1 && (
        <Pagination totalPages={totalPages} page={page} onPageChange={onPageChange} />
      )}
    </>
  );
};

export default CommunityListSection;
