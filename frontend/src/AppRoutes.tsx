import { Route, Routes } from 'react-router-dom';
import EventDetailsContainer from './pages/eventDetailsPage/EventDetailsContainer';
import HomePageContainer from './pages/homePage/HomePageContainer';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path='/' element={<HomePageContainer />} />
      <Route path='/events/:eventId' element={<EventDetailsContainer />} />
    </Routes>
  );
};

export default AppRoutes;
