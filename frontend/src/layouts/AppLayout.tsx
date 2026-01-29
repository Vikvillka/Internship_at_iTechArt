import { useSelector } from 'react-redux';
import { Outlet } from 'react-router-dom';
import Footer from '../components/common/footer/Footer';
import Header from '../components/common/header/Header';
import Loader from '../components/common/loader/Loader';
import { selectIsLoading } from '../store/features/loader/loaderSelectors';

const AppLayout = () => {
  const isLoader = useSelector(selectIsLoading);

  return (
    <>
      <Header />
      {isLoader && <Loader />}
      <Outlet />
      <Footer />
    </>
  );
};

export default AppLayout;
