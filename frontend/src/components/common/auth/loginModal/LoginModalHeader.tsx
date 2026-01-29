import CloseIcon from '@mui/icons-material/Close';
import { IconButton, Typography } from '@mui/material';
import React from 'react';
import { loginModalStyles } from './LoginModal.styles';

interface HeaderProps {
  onClose: () => void;
}

const LoginModalHeader: React.FC<HeaderProps> = ({ onClose }) => {
  return (
    <>
      <IconButton sx={loginModalStyles.closeButton} onClick={onClose}>
        <CloseIcon />
      </IconButton>
      <Typography variant='h5' sx={loginModalStyles.title}>
        Log in
      </Typography>
    </>
  );
};

export default LoginModalHeader;
