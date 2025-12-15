import { Route, Routes } from 'react-router-dom';
import AppLayout from './layouts/AppLayout';
import EventDetailsContainer from './pages/eventDetailsPage/EventDetailsContainer';
import HomePageContainer from './pages/homePage/HomePageContainer';

const AppRoutes = () => {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route path='/' element={<HomePageContainer />} />
        <Route path='/events/:eventId' element={<EventDetailsContainer />} />
        <Route path='*' element={<HomePageContainer />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
