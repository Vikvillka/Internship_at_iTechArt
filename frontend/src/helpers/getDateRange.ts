export const getDateRange = (filter?: 'any' | 'today' | 'tomorrow' | 'thisWeek' | 'nextWeek') => {
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  switch (filter) {
    case 'today':
      return {
        dateFrom: today.toISOString(),
        dateTo: new Date(today.getTime() + 24 * 60 * 60 * 1000).toISOString(),
      };

    case 'tomorrow':
      const tomorrow = new Date(today.getTime() + 24 * 60 * 60 * 1000);
      return {
        dateFrom: tomorrow.toISOString(),
        dateTo: new Date(tomorrow.getTime() + 24 * 60 * 60 * 1000).toISOString(),
      };

    case 'thisWeek':
      const dayOfWeek = today.getDay();
      const daysUntilEndOfWeek = 7 - dayOfWeek;
      return {
        dateFrom: today.toISOString(),
        dateTo: new Date(today.getTime() + daysUntilEndOfWeek * 24 * 60 * 60 * 1000).toISOString(),
      };

    case 'nextWeek':
      const daysUntilNextWeekStart = 7 - today.getDay();
      const nextWeekStart = new Date(
        today.getTime() + daysUntilNextWeekStart * 24 * 60 * 60 * 1000,
      );
      const nextWeekEnd = new Date(nextWeekStart.getTime() + 7 * 24 * 60 * 60 * 1000);
      return { dateFrom: nextWeekStart.toISOString(), dateTo: nextWeekEnd.toISOString() };
    case 'any':
    default:
      return { dateFrom: undefined, dateTo: undefined };
  }
};
