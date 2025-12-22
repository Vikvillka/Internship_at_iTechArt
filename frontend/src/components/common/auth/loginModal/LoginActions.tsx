import { Box, Button, Typography } from '@mui/material';
import React from 'react';
import { loginModalStyles } from './LoginModal.styles';

interface LoginActionsProps {
  onSubmit: () => void;
  onSwitchToRegister?: () => void;
}

const LoginActions: React.FC<LoginActionsProps> = ({ onSubmit, onSwitchToRegister }) => {
  return (
    <>
      <Button variant='contained' fullWidth sx={loginModalStyles.submitButton} onClick={onSubmit}>
        Log in
      </Button>

      <Typography variant='body2' sx={loginModalStyles.switchText}>
        Do not have an account yet?{' '}
        <Box component='span' sx={loginModalStyles.linkSignUp} onClick={onSwitchToRegister}>
          Sign up
        </Box>
      </Typography>
    </>
  );
};

export default LoginActions;
