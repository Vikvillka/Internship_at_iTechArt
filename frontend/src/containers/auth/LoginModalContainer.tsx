import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import LoginModal from '../../components/common/auth/loginModal/LoginModal';
import { AuthCreds } from '../../models/Auth';
import { login, setAuthError } from '../../store/features/auth/authSlice';
import { isLoginModalOpen } from '../../store/features/ui/uiSelectors';
import { closeLoginModal, switchToRegisterModal } from '../../store/features/ui/uiSlice';

const LoginModalContainer: React.FC = () => {
  const dispatch = useDispatch();
  const isOpen = useSelector(isLoginModalOpen);

  const handleClose = () => {
    dispatch(closeLoginModal());
    dispatch(setAuthError(''));
  };

  const handleSubmit = (AuthCreds: AuthCreds) => {
    dispatch(login(AuthCreds));
  };

  const handleSwitchToRegister = () => {
    dispatch(switchToRegisterModal());
    dispatch(setAuthError(''));
  };

  return (
    <LoginModal
      isOpen={isOpen}
      onClose={handleClose}
      onSubmit={handleSubmit}
      onSwitchToRegister={handleSwitchToRegister}
    />
  );
};

export default LoginModalContainer;
