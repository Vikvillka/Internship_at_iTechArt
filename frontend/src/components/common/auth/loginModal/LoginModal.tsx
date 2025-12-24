import { Box, Modal } from '@mui/material';
import React from 'react';
import { AuthCreds } from '../../../../models/Auth';
import LoginForm from './LoginForm';
import { loginModalStyles } from './LoginModal.styles';
import LoginModalHeader from './LoginModalHeader';

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: (creds: AuthCreds) => void;
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
