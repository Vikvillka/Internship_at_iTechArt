import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import LoginModal from '../../components/common/auth/loginModal/LoginModal';
import { isLoginModalOpen } from '../../store/features/ui/uiSelectors';
import { closeLoginModal, switchToRegisterModal } from '../../store/features/ui/uiSlice';

const LoginModalContainer: React.FC = () => {
  const dispatch = useDispatch();
  const isOpen = useSelector(isLoginModalOpen);

  const handleClose = () => {
    dispatch(closeLoginModal());
  };

  const handleSubmit = (login: string, password: string) => {
    // dispatch(login({ login, password }));
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
