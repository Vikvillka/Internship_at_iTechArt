import React, { useEffect, useState } from 'react';
import { useCommunitiesData } from '../../hooks/useCommunityData';
import { useEventsData } from '../../hooks/useEventsData';
import { useHomePageData } from '../../hooks/useHomePageData';
import { Community } from '../../models/Community';
import { Event } from '../../models/Event';
import { ContentMode as Mode } from '../../models/Mode';
import HomePage from '../../pages/homePage/HomePage';

const HomePageContainer: React.FC = () => {
  const { loadEvents } = useEventsData();
  const { loadCommunities } = useCommunitiesData();

  const {
    mode,
    setMode,
    page,
    setPage,
    selectedCategory,
    setSelectedCategory,
    selectedDate,
    setSelectedDate,
    keywordsFromUrl,
    locationFromUrl,
    resetPage,
  } = useHomePageData();

  const [items, setItems] = useState<Event[] | Community[]>([]);
  const [participantCounts, setParticipantCounts] = useState<Record<string, number>>({});
  const [subscriptionCounts, setSubscriptionCounts] = useState<Record<string, number>>({});
  const [totalPages, setTotalPages] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    resetPage();
  }, [keywordsFromUrl, locationFromUrl, selectedCategory, selectedDate, mode, resetPage]);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        if (mode === Mode.Events) {
          const data = await loadEvents(
            page,
            selectedCategory,
            selectedDate,
            keywordsFromUrl,
            locationFromUrl,
          );
          setItems(data.items);
          setParticipantCounts(data.participantCounts);
          setTotalPages(data.totalPages);
          setSubscriptionCounts({});
        } else {
          const data = await loadCommunities(
            page,
            selectedCategory,
            keywordsFromUrl,
            locationFromUrl,
          );
          setItems(data.items);
          setSubscriptionCounts(data.subscriptionCounts);
          setTotalPages(data.totalPages);
          setParticipantCounts({});
        }
      } catch (err: any) {
        setError(err.message || 'Failed to fetch data');
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [
    page,
    keywordsFromUrl,
    locationFromUrl,
    selectedCategory,
    selectedDate,
    mode,
    loadEvents,
    loadCommunities,
  ]);

  return (
    <HomePage
      mode={mode}
      setMode={setMode}
      items={items}
      participantCounts={participantCounts}
      subscriptionCounts={subscriptionCounts}
      page={page}
      totalPages={totalPages}
      loading={loading}
      error={error}
      selectedCategory={selectedCategory}
      selectedDate={selectedDate}
      onPageChange={setPage}
      onCategorySelect={setSelectedCategory}
      onDateChange={setSelectedDate}
    />
  );
};

export default HomePageContainer;
