import { useCallback } from 'react';
import { eventsApi } from '../api/event';
import { participationApi } from '../api/participation';
import { getDateRange } from '../helpers/getDateRange';
import { categories } from '../helpers/sliderCategory/categories';
import { PagedResponse } from '../models/Common';
import { DateOption } from '../models/Date';
import { Event, EventSearchRequest } from '../models/Event';

export interface EventsDataResult {
  items: Event[];
  totalPages: number;
  participantCounts: Record<string, number>;
}

export const useEventsData = () => {
  const loadEvents = useCallback(
    async (
      pageNumber: number,
      category: string,
      selectedDate: DateOption,
      keywordsFromUrl?: string,
      locationFromUrl?: string,
    ): Promise<EventsDataResult> => {
      const { dateFrom, dateTo } =
        selectedDate === DateOption.Any
          ? { dateFrom: undefined, dateTo: undefined }
          : getDateRange(selectedDate);

      const params: EventSearchRequest = {
        keywords: keywordsFromUrl,
        location: locationFromUrl,
        category: category === categories[0].label ? undefined : category,
        dateFrom,
        dateTo,
        page: pageNumber,
        pageSize: 20,
      };

      const data: PagedResponse<Event> = await eventsApi.getEventsBySearch(params);

      const eventIds = data.items.map((event) => event.id);
      const counts = await participationApi.getParticipationCounts(eventIds);
      const participantCounts: Record<string, number> = {};
      counts.forEach((count) => {
        participantCounts[count.eventId] = count.count;
      });

      return {
        items: data.items,
        totalPages: Math.ceil(data.totalCount / data.pageSize),
        participantCounts,
      };
    },
    [],
  );

  return { loadEvents };
};
