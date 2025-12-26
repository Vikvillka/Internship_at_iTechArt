import { useSelector } from 'react-redux';
import { Route, Routes } from 'react-router-dom';
import HomePageContainer from './containers/common/HomePageContainer';
import CommunityDetailsPageContainer from './containers/community/CommunityDetailsPageContainer';
import EventDetailsPageContainer from './containers/event/EventDetailsPageContainer';
import UserCommunitiesContainer from './containers/user/userCommunities/UserCommunitiesContainer';
import AppLayout from './layouts/AppLayout';
import { selectIsLoggedIn } from './store/features/auth/authSelectors';

const AppRoutes = () => {
  const isLoggedIn = useSelector(selectIsLoggedIn);

  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route path='/' element={<HomePageContainer />} />
        <Route path='/event/:eventId' element={<EventDetailsPageContainer />} />
        <Route path='/community/:communityId' element={<CommunityDetailsPageContainer />} />
        <Route
          path='/user-communities'
          element={isLoggedIn ? <UserCommunitiesContainer /> : <HomePageContainer />}
        />
        <Route path='*' element={<HomePageContainer />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
