import { DAYS_IN_WEEK, MS_IN_DAY } from '../constants/timeConstant';
import { DateOption } from '../models/Date';

export const getDateRange = (filter?: DateOption) => {
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  switch (filter) {
    case DateOption.Today:
      return {
        dateFrom: today.toISOString(),
        dateTo: new Date(today.getTime() + MS_IN_DAY).toISOString(),
      };

    case DateOption.Tomorrow:
      const tomorrow = new Date(today.getTime() + MS_IN_DAY);
      return {
        dateFrom: tomorrow.toISOString(),
        dateTo: new Date(tomorrow.getTime() + MS_IN_DAY).toISOString(),
      };

    case DateOption.ThisWeek:
      const dayOfWeek = today.getDay();
      const daysUntilEndOfWeek = DAYS_IN_WEEK - dayOfWeek;
      return {
        dateFrom: today.toISOString(),
        dateTo: new Date(today.getTime() + daysUntilEndOfWeek * MS_IN_DAY).toISOString(),
      };

    case DateOption.NextWeek:
      const daysUntilNextWeekStart = DAYS_IN_WEEK - today.getDay();
      const nextWeekStart = new Date(today.getTime() + daysUntilNextWeekStart * MS_IN_DAY);
      const nextWeekEnd = new Date(nextWeekStart.getTime() + DAYS_IN_WEEK * MS_IN_DAY);
      return { dateFrom: nextWeekStart.toISOString(), dateTo: nextWeekEnd.toISOString() };
    case DateOption.Any:
    default:
      return { dateFrom: undefined, dateTo: undefined };
  }
};
