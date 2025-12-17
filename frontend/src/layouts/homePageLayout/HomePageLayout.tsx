import EventIcon from '@mui/icons-material/Event';
import GroupsIcon from '@mui/icons-material/Groups';
import { Box, Button, ButtonGroup, CircularProgress, Container, Typography } from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import CategorySlider from '../../components/common/categorySlider/CategorySlider';
import DateFilter from '../../components/common/dataFilter/DateFilter';
import PageTitle from '../../components/common/pageTitle/PageTitle';
import { DateOption } from '../../models/Date';
import { ContentMode } from '../../models/Mode';
import { errorContainer, errorText, loadingBox, pageContainer } from '../../styles/common';
import { homePageStyles } from './HomePage.styles';

type Props = {
  mode: ContentMode.Events | ContentMode.Communities;
  setMode: (mode: ContentMode.Events | ContentMode.Communities) => void;
  loading?: boolean;
  error?: string | null;
  selectedDate?: DateOption;
  onDateChange: (value: DateOption) => void;
  onCategorySelect: (category: string) => void;
  children: React.ReactNode;
};

const HomePageLayout: React.FC<Props> = ({
  mode,
  setMode,
  loading,
  error,
  selectedDate,
  onDateChange,
  onCategorySelect,
  children,
}) => {
  const navigate = useNavigate();

  const handleModeChange = (newMode: ContentMode.Events | ContentMode.Communities) => {
    setMode(newMode);
    navigate(`/${newMode}`);
  };

  return (
    <Container sx={pageContainer} disableGutters>
      <Box>
        <ButtonGroup sx={homePageStyles.buttonGroup} variant='text'>
          <Button
            startIcon={<EventIcon />}
            sx={
              mode === ContentMode.Events
                ? homePageStyles.activeButton
                : homePageStyles.inactiveButton
            }
            onClick={() => handleModeChange(ContentMode.Events)}
          >
            Events
          </Button>
          <Button
            startIcon={<GroupsIcon />}
            sx={
              mode === ContentMode.Communities
                ? homePageStyles.activeButton
                : homePageStyles.inactiveButton
            }
            onClick={() => handleModeChange(ContentMode.Communities)}
          >
            Communities
          </Button>
        </ButtonGroup>
      </Box>
      <PageTitle
        title={mode === ContentMode.Events ? 'See upcoming events!' : 'Explore communities!'}
        rightSlot={
          mode === ContentMode.Events ? (
            <DateFilter value={selectedDate} onChange={onDateChange} />
          ) : null
        }
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
