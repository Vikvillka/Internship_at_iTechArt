import { Container } from '@mui/material';
import EventsListSection from '../../components/event/eventsList/EventsList';
import HomePageLayout from '../../layouts/homePageLayout/HomePageLayout';
import { DateOption } from '../../models/Date';
import { Event } from '../../models/Event';
import { pageContainer } from '../../styles/common';

interface Props {
  events: Event[];
  participantCounts: Record<string, number>;
  page: number;
  totalPages: number;
  loading: boolean;
  error: string | null;
  selectedCategory: string;
  selectedDate?: DateOption;
  onPageChange: (page: number) => void;
  onCategorySelect: (category: string) => void;
  onDateChange: (value: DateOption) => void;
}

const HomePageView: React.FC<Props> = ({
  events,
  participantCounts,
  page,
  totalPages,
  loading,
  error,
  onPageChange,
  onCategorySelect,
  selectedDate,
  onDateChange,
}) => {
  return (
    <Container sx={pageContainer} disableGutters>
      <HomePageLayout
        loading={loading}
        error={error}
        selectedDate={selectedDate}
        onDateChange={onDateChange}
        onCategorySelect={onCategorySelect}
      >
        <EventsListSection
          events={events}
          participantCounts={participantCounts}
          page={page}
          totalPages={totalPages}
          onPageChange={onPageChange}
        />
      </HomePageLayout>
    </Container>
  );
};

export default HomePageView;
