import { Box, Button } from '@mui/material';
import React from 'react';
import { authButtonsStyles } from './AuthButtons.styles';

interface Props {
  onLoginClick: () => void;
  onRegisterClick: () => void;
}

const AuthButtons: React.FC<Props> = ({ onLoginClick, onRegisterClick }) => {
  return (
    <Box sx={authButtonsStyles.authButtonsContainer}>
      <Button sx={authButtonsStyles.loginButton} variant='outlined' onClick={onLoginClick}>
        Log in
      </Button>
      <Button sx={authButtonsStyles.registerButton} variant='contained' onClick={onRegisterClick}>
        Sign up
      </Button>
    </Box>
  );
};

export default AuthButtons;
