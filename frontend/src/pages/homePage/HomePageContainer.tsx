import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { eventsApi } from '../../api/event';
import { participationApi } from '../../api/participation';
import { getDateRange } from '../../helpers/getDateRange';
import { PagedResponse } from '../../models/Common';
import { Event, EventSearchRequest } from '../../models/Event';
import HomePageView from './HomePageView';

type DateOption = 'any' | 'today' | 'tomorrow' | 'thisWeek' | 'nextWeek';

const HomePageContainer: React.FC = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [participantCounts, setParticipantCounts] = useState<Record<string, number>>({});
  const [page, setPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [selectedCategory, setSelectedCategory] = useState<string>('All events');
  const [selectedDate, setSelectedDate] = useState<DateOption>('any');
  const [searchParams] = useSearchParams();
  const keywordsFromUrl = searchParams.get('keywords') || undefined;
  const locationFromUrl = searchParams.get('location') || undefined;
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setPage(1);
  }, [keywordsFromUrl, locationFromUrl]);

  const handleDateChange = (value: DateOption) => {
    setSelectedDate(value);
    setPage(1);
  };

  const loadEvents = async (pageNumber: number, category: string) => {
    setLoading(true);

    const { dateFrom, dateTo } =
      selectedDate === 'any'
        ? { dateFrom: undefined, dateTo: undefined }
        : getDateRange(selectedDate);

    const params: EventSearchRequest = {
      keywords: keywordsFromUrl,
      location: locationFromUrl,
      category: category === 'All events' ? undefined : category,
      dateFrom,
      dateTo,
      page: pageNumber,
      pageSize: 20,
    };

    try {
      const data: PagedResponse<Event> = await eventsApi.getEventsBySearch(params);

      setEvents(data.items);
      setTotalPages(Math.ceil(data.totalCount / data.pageSize));

      const eventIds = data.items.map((event) => event.id);

      const counts = await participationApi.getParticipationCounts(eventIds);
      const countsMap: Record<string, number> = {};
      counts.forEach((count) => {
        countsMap[count.eventId] = count.count;
      });
      setParticipantCounts(countsMap);
    } catch (err: any) {
      setError(err.message || 'Failed to fetch events');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadEvents(page, selectedCategory);
  }, [page, keywordsFromUrl, locationFromUrl, selectedCategory, selectedDate]);

  return (
    <HomePageView
      events={events}
      participantCounts={participantCounts}
      page={page}
      totalPages={totalPages}
      loading={loading}
      error={error}
      selectedCategory={selectedCategory}
      selectedDate={selectedDate}
      onPageChange={setPage}
      onCategorySelect={setSelectedCategory}
      onDateChange={handleDateChange}
    />
  );
};

export default HomePageContainer;
