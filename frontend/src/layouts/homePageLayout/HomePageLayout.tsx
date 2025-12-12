import { Box, CircularProgress, Container, Typography } from '@mui/material';
import React from 'react';
import CategorySlider from '../../components/common/categorySlider/CategorySlider';
import DateFilter from '../../components/common/dataFilter/DateFilter';
import PageTitle from '../../components/common/pageTitle/PageTitle';
import { DateOption } from '../../models/Date';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';

type Props = {
  loading?: boolean;
  error?: string | null;
  selectedDate?: DateOption;
  onDateChange: (value: DateOption) => void;
  onCategorySelect: (category: string) => void;
  children: React.ReactNode;
};

const HomePageLayout: React.FC<Props> = ({
  loading,
  error,
  selectedDate,
  onDateChange,
  onCategorySelect,
  children,
}) => {
  return (
    <Container sx={pageContainer} disableGutters>
      <PageTitle
        title='See upcoming events!'
        rightSlot={<DateFilter value={selectedDate} onChange={onDateChange} />}
      />
      <CategorySlider onSelect={onCategorySelect} />
      {loading && (
        <Box sx={loadingBox}>
          <CircularProgress />
        </Box>
      )}
      {!loading && error && (
        <Box sx={errorContainer}>
          <Typography sx={errorText}>{error}</Typography>
        </Box>
      )}
      {!loading && !error && <Box mt={3}>{children}</Box>}
    </Container>
  );
};

export default HomePageLayout;
