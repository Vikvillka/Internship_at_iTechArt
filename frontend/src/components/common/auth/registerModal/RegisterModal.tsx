import { Box, Modal } from '@mui/material';
import React from 'react';
import { Gender } from '../../../../models/Gender';
import RegisterForm from './RegisterForm';
import { registerModalStyles } from './RegisterModal.styles';
import RegisterModalHeader from './RegisterModalHeader';

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: (
    username: string,
    email: string,
    password: string,
    gender: Gender,
    location: string,
  ) => void;
  onSwitchToLogin?: () => void;
}

const RegisterModal: React.FC<Props> = ({ isOpen, onClose, onSubmit, onSwitchToLogin }) => {
  return (
    <Modal open={isOpen} onClose={onClose}>
      <Box sx={registerModalStyles.modalBox}>
        <RegisterModalHeader onClose={onClose} />
        <RegisterForm onSubmit={onSubmit} onSwitchToLogin={onSwitchToLogin} />
      </Box>
    </Modal>
  );
};

export default RegisterModal;
