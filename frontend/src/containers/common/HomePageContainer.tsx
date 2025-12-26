import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useHomePageData } from '../../hooks/useHomePageData';
import { ContentMode as Mode } from '../../models/Mode';
import HomePage from '../../pages/homePage/HomePage';
import {
  selectCommunities,
  selectCommunitySubscriptionCounts,
  selectCommunityTotalPages,
} from '../../store/features/communities/communitiesSelectors';
import { getCommunities } from '../../store/features/communities/communitiesSlice';
import {
  selectEvents,
  selectParticipationCounts,
  selectTotalEventPages,
} from '../../store/features/events/eventsSelectors';
import { getEvents } from '../../store/features/events/eventsSlice';

const HomePageContainer: React.FC = () => {
  const dispatch = useDispatch();

  const {
    mode,
    setMode,
    page,
    setPage,
    category,
    setSelectedCategory,
    dateFrom,
    dateTo,
    selectedDate,
    setSelectedDate,
    keywordsFromUrl,
    locationFromUrl,
  } = useHomePageData();

  const events = useSelector(selectEvents);
  const participantCounts = useSelector(selectParticipationCounts);
  const totalEventPages = useSelector(selectTotalEventPages);

  const communities = useSelector(selectCommunities);
  const subscriptionCounts = useSelector(selectCommunitySubscriptionCounts);
  const totalCommunityPages = useSelector(selectCommunityTotalPages);

  useEffect(() => {
    if (mode === Mode.Events) {
      dispatch(
        getEvents({
          page,
          pageSize: 20,
          dateTo: dateTo,
          dateFrom: dateFrom,
          category: category || undefined,
          keywords: keywordsFromUrl,
          location: locationFromUrl,
        }),
      );
    } else {
      dispatch(
        getCommunities({
          page,
          pageSize: 20,
          category: category || undefined,
          keywords: keywordsFromUrl,
          location: locationFromUrl,
        }),
      );
    }
  }, [
    mode,
    page,
    category,
    dateFrom,
    dateTo,
    selectedDate,
    keywordsFromUrl,
    locationFromUrl,
    dispatch,
  ]);

  return (
    <HomePage
      mode={mode}
      setMode={setMode}
      items={mode === Mode.Events ? events : communities}
      participantCounts={participantCounts}
      subscriptionCounts={subscriptionCounts}
      page={page}
      totalPages={mode === Mode.Events ? totalEventPages : totalCommunityPages}
      selectedCategory={category}
      selectedDate={selectedDate}
      onPageChange={setPage}
      onCategorySelect={setSelectedCategory}
      onDateChange={setSelectedDate}
    />
  );
};

export default HomePageContainer;
