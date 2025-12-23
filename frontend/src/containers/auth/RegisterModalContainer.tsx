import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import RegisterModal from '../../components/common/auth/registerModal/RegisterModal';
import { isRegisterModalOpen } from '../../store/features/ui/uiSelectors';
import { closeRegisterModal, switchToLoginModal } from '../../store/features/ui/uiSlice';

const RegisterModalContainer: React.FC = () => {
  const dispatch = useDispatch();
  const isOpen = useSelector(isRegisterModalOpen);

  const handleClose = () => {
    dispatch(closeRegisterModal());
  };

  const handleSubmit = (login: string, password: string) => {
    //dispatch(register({ login, password }));
  };

  const handleSwitchToLogin = () => {
    dispatch(switchToLoginModal());
  };

  return (
    <RegisterModal
      isOpen={isOpen}
      onClose={handleClose}
      onSubmit={handleSubmit}
      onSwitchToLogin={handleSwitchToLogin}
    />
  );
};

export default RegisterModalContainer;
