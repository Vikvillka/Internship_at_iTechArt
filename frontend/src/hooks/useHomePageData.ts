import { useCallback, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { DateOption } from '../models/Date';
import { ContentMode as Mode } from '../models/Mode';

export const useHomePageData = () => {
  const [mode, setMode] = useState<Mode>(Mode.Events);
  const [page, setPage] = useState<number>(1);
  const [selectedCategory, setSelectedCategory] = useState<string>('');
  const [selectedDate, setSelectedDate] = useState<DateOption>(DateOption.Any);
  const [searchParams] = useSearchParams();

  const keywordsFromUrl = searchParams.get('keywords') || undefined;
  const locationFromUrl = searchParams.get('location') || undefined;

  const handleDateChange = useCallback((value: DateOption) => {
    setSelectedDate(value);
    setPage(1);
  }, []);

  const handleCategorySelect = useCallback((category: string) => {
    setSelectedCategory(category);
    setPage(1);
  }, []);

  const handleModeChange = useCallback((newMode: Mode) => {
    setMode(newMode);
    setPage(1);
  }, []);

  const resetPage = useCallback(() => {
    setPage(1);
  }, []);

  return {
    mode,
    setMode: handleModeChange,
    page,
    setPage,
    selectedCategory,
    setSelectedCategory: handleCategorySelect,
    selectedDate,
    setSelectedDate: handleDateChange,
    keywordsFromUrl,
    locationFromUrl,
    resetPage,
  };
};
