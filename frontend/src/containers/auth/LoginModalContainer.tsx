import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import LoginModal from '../../components/common/auth/loginModal/LoginModal';
import { AuthCreds } from '../../models/Auth';
import { selectAuthError } from '../../store/features/auth/authSelectors';
import { login } from '../../store/features/auth/authSlice';
import { isLoginModalOpen } from '../../store/features/ui/uiSelectors';
import { closeLoginModal, switchToRegisterModal } from '../../store/features/ui/uiSlice';

const LoginModalContainer: React.FC = () => {
  const dispatch = useDispatch();
  const isOpen = useSelector(isLoginModalOpen);
  const error = useSelector(selectAuthError);

  const handleClose = () => {
    dispatch(closeLoginModal());
  };

  const handleSubmit = (AuthCreds: AuthCreds) => {
    dispatch(login(AuthCreds));
  };

  const handleSwitchToRegister = () => {
    dispatch(switchToRegisterModal());
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
