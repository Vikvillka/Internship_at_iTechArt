import { Route, Routes } from 'react-router-dom';
import HomePageContainer from './containers/common/HomePageContainer';
import EventDetailsPageContainer from './containers/event/EventDetailsPageContainer';
import AppLayout from './layouts/AppLayout';

const AppRoutes = () => {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route path='/' element={<HomePageContainer />} />
        <Route path='/events/:eventId' element={<EventDetailsPageContainer />} />
        <Route path='*' element={<HomePageContainer />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
