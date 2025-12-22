import { Box, Modal } from '@mui/material';
import React from 'react';
import LoginForm from './LoginForm';
import { loginModalStyles } from './LoginModal.styles';
import LoginModalHeader from './LoginModalHeader';

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: (login: string, password: string) => void;
  onSwitchToRegister?: () => void;
}

const LoginModal: React.FC<Props> = ({ isOpen, onClose, onSubmit, onSwitchToRegister }) => {
  return (
    <Modal open={isOpen} onClose={onClose}>
      <Box sx={loginModalStyles.modalBox}>
        <LoginModalHeader onClose={onClose} />
        <LoginForm onSubmit={onSubmit} onSwitchToRegister={onSwitchToRegister} />
      </Box>
    </Modal>
  );
};

export default LoginModal;
