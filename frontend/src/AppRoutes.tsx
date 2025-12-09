import { Route, Routes } from 'react-router-dom';
import EventDetailsPage from './pages/EventDetailsPage';
import HomePage from './pages/HomePage';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path='/' element={<HomePage />} />
      <Route path='/events/:eventId' element={<EventDetailsPage />} />
    </Routes>
  );
};

export default AppRoutes;
