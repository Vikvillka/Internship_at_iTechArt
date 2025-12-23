import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import AuthButtons from '../../components/common/auth/AuthButtons';
import LoginModal from '../../components/common/auth/loginModal/LoginModal';
import { isLoginModalOpen } from '../../store/features/ui/uiSelectors';
import { closeLoginModal, openLoginModal } from '../../store/features/ui/uiSlice';
// import { loginUser, registerUser } from '../../store/authSlice';

const AuthContainer: React.FC = () => {
  const dispatch = useDispatch();
  const isLoginOpen = useSelector(isLoginModalOpen);
  // const isRegisterOpen = useSelector(isRegisterModalOpen);

  const handleLoginOpen = () => {
    dispatch(openLoginModal());
  };

  const handleLoginClose = () => {
    dispatch(closeLoginModal());
  };

  // const handleRegisterOpen = () => {
  //   dispatch(openRegisterModal());
  // }

  // const handleRegisterClose = () => {
  //   dispatch(closeRegisterModal());
  // }

  const handleSubmitLogin = (login: string, password: string) => {
    // dispatch(loginUser({ login, password }));
    handleLoginClose();
  };

  const handleSubmitRegister = (login: string, password: string) => {
    //  dispatch(registerUser({ login, password }));
    handleLoginClose();
  };

  return (
    <>
      <AuthButtons onLoginClick={handleLoginOpen} onRegisterClick={handleLoginOpen} />
      <LoginModal isOpen={isLoginOpen} onClose={handleLoginClose} onSubmit={handleSubmitLogin} />
    </>
  );
};

export default AuthContainer;
