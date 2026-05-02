import EventIcon from '@mui/icons-material/Event';
import GroupsIcon from '@mui/icons-material/Groups';
import { Box, Button, ButtonGroup, Container, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import CategorySlider from '../../components/common/categorySlider/CategorySlider';
import DateFilter from '../../components/common/dataFilter/DateFilter';
import PageTitle from '../../components/common/pageTitle/PageTitle';
import { DateOption } from '../../models/Date';
import { ContentMode } from '../../models/Mode';
import {
  selectCommunitiesError,
} from '../../store/features/communities/communitiesSelectors';
import { selectEventsError } from '../../store/features/events/eventsSelectors';
import { selectIsLoading } from '../../store/features/loader/loaderSelectors';
import { errorContainer, errorText, pageContainer } from '../../styles/common';
import { homePageStyles } from './HomePage.styles';

type Props = {
  mode: ContentMode.Events | ContentMode.Communities;
  setMode: (mode: ContentMode.Events | ContentMode.Communities) => void;
  selectedDate?: DateOption;
  onDateChange: (value: DateOption) => void;
  onCategorySelect: (category: string) => void;
  children: React.ReactNode;
};

const HomePageLayout: React.FC<Props> = ({
  mode,
  setMode,
  selectedDate,
  onDateChange,
  onCategorySelect,
  children,
}) => {
  const loading = useSelector(selectIsLoading);
  const errorCommunities = useSelector(selectCommunitiesError);
  const errorEvents = useSelector(selectEventsError);

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
            onClick={() => setMode(ContentMode.Events)}
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
            onClick={() => setMode(ContentMode.Communities)}
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
      {!loading && (errorCommunities || errorEvents) && (
        <Box sx={errorContainer}>
          <Typography sx={errorText}>{errorCommunities || errorEvents}</Typography>
        </Box>
      )}
      {!loading && !errorCommunities && !errorEvents && (
        <Box sx={homePageStyles.content}>{children}</Box>
      )}
    </Container>
  );
};

export default HomePageLayout;
