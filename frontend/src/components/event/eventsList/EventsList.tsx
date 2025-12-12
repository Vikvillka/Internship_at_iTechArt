import { Grid } from '@mui/material';
import EmptyState from '../../../components/common/emptyState/EmptyState';
import Pagination from '../../../components/common/pagination/Pagination';
import EventCard from '../../../components/event/eventCard/EventCard';
import { Event } from '../../../models/Event';

interface Props {
  events: Event[];
  participantCounts: Record<string, number>;
  page: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

const EventsListSection: React.FC<Props> = ({
  events,
  participantCounts,
  page,
  totalPages,
  onPageChange,
}) => {
  if (events.length === 0) return <EmptyState message='No events found' />;

  return (
    <>
      <Grid container spacing={1}>
        {events.map((event) => (
          <Grid key={event.id} size={{ xs: 12, sm: 6, md: 3 }}>
            <EventCard event={event} participantCount={participantCounts[event.id] ?? 0} />
          </Grid>
        ))}
      </Grid>
      {totalPages > 1 && (
        <Pagination totalPages={totalPages} page={page} onPageChange={onPageChange} />
      )}
    </>
  );
};

export default EventsListSection;
