import { Route, Routes } from 'react-router-dom';
import HomePageContainer from './containers/common/HomePageContainer';
import CommunityDetailsPageContainer from './containers/community/CommunityDetailsPageContainer';
import EventDetailsPageContainer from './containers/event/EventDetailsPageContainer';
import AppLayout from './layouts/AppLayout';

const AppRoutes = () => {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route path='/' element={<HomePageContainer />} />
        <Route path='/event/:eventId' element={<EventDetailsPageContainer />} />
        <Route path='/community/:communityId' element={<CommunityDetailsPageContainer />} />
        <Route path='*' element={<HomePageContainer />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
