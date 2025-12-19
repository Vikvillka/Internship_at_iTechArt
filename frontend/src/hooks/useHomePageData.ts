import { useCallback } from 'react';
import { useLocation, useNavigate, useSearchParams } from 'react-router-dom';
import { DateOption } from '../models/Date';
import { ContentMode as Mode } from '../models/Mode';

export const useHomePageData = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const mode: Mode = location.pathname.startsWith('/communities') ? Mode.Communities : Mode.Events;

  const page = Number(searchParams.get('page') ?? 1);
  const selectedCategory = searchParams.get('category') ?? '';
  const selectedDate = (searchParams.get('date') as DateOption) ?? DateOption.Any;

  const keywordsFromUrl = searchParams.get('keywords') ?? undefined;
  const locationFromUrl = searchParams.get('location') ?? undefined;

  const updateParams = (updates: Record<string, string | undefined>) => {
    const next = new URLSearchParams(searchParams);

    Object.entries(updates).forEach(([key, value]) => {
      if (!value) next.delete(key);
      else next.set(key, value);
    });

    next.set('page', '1');
    setSearchParams(next);
  };

  const setMode = useCallback(
    (newMode: Mode) => {
      navigate(`/${newMode}?${searchParams.toString()}`);
    },
    [navigate, searchParams],
  );

  return {
    mode,
    setMode,
    page,
    setPage: (p: number) => updateParams({ page: String(p) }),
    selectedCategory,
    setSelectedCategory: (c: string) => updateParams({ category: c }),
    selectedDate,
    setSelectedDate: (d: DateOption) => updateParams({ date: d }),
    keywordsFromUrl,
    locationFromUrl,
  };
};
