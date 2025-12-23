import CloseIcon from '@mui/icons-material/Close';
import { IconButton, Typography } from '@mui/material';
import React from 'react';
import { registerModalStyles } from './RegisterModal.styles';

interface HeaderProps {
  onClose: () => void;
}

const RegisterModalHeader: React.FC<HeaderProps> = ({ onClose }) => {
  return (
    <>
      <IconButton sx={registerModalStyles.closeButton} onClick={onClose}>
        <CloseIcon />
      </IconButton>
      <Typography variant='h5' sx={registerModalStyles.title}>
        Sign up
      </Typography>
    </>
  );
};

export default RegisterModalHeader;
