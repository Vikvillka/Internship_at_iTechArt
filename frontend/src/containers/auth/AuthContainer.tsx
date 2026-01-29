import React from 'react';
import { useDispatch } from 'react-redux';
import AuthButtons from '../../components/common/auth/AuthButtons';
import { openLoginModal, openRegisterModal } from '../../store/features/ui/uiSlice';
import LoginModalContainer from './LoginModalContainer';
import RegisterModalContainer from './RegisterModalContainer';

const AuthContainer: React.FC = () => {
  const dispatch = useDispatch();

  const handleLoginOpen = () => {
    dispatch(openLoginModal());
  };

  const handleRegisterOpen = () => {
    dispatch(openRegisterModal());
  };

  return (
    <>
      <AuthButtons onLoginClick={handleLoginOpen} onRegisterClick={handleRegisterOpen} />
      <LoginModalContainer />
      <RegisterModalContainer />
    </>
  );
};

export default AuthContainer;
