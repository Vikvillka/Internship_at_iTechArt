import CommunityListSection from '../../components/community/communityList/CommunityList';
import EventsListSection from '../../components/event/eventsList/EventsList';
import HomePageLayout from '../../layouts/homePageLayout/HomePageLayout';
import { Community } from '../../models/Community';
import { DateOption } from '../../models/Date';
import { Event } from '../../models/Event';
import { ContentMode } from '../../models/Mode';

interface Props {
  mode: ContentMode.Events | ContentMode.Communities;
  setMode: (mode: ContentMode.Events | ContentMode.Communities) => void;
  items: Event[] | Community[];
  participantCounts: Record<string, number>;
  subscriptionCounts: Record<string, number>;
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

const HomePage: React.FC<Props> = ({
  mode,
  setMode,
  items,
  participantCounts,
  subscriptionCounts,
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
    <>
      <HomePageLayout
        mode={mode}
        setMode={setMode}
        loading={loading}
        error={error}
        selectedDate={selectedDate}
        onDateChange={onDateChange}
        onCategorySelect={onCategorySelect}
      >
        {mode === ContentMode.Events && (
          <EventsListSection
            events={items as Event[]}
            participantCounts={participantCounts}
            page={page}
            totalPages={totalPages}
            onPageChange={onPageChange}
          />
        )}
        {mode === ContentMode.Communities && (
          <CommunityListSection
            communities={items as Community[]}
            subscriptionCounts={subscriptionCounts}
            page={page}
            totalPages={totalPages}
            onPageChange={onPageChange}
          />
        )}
      </HomePageLayout>
    </>
  );
};

export default HomePage;
