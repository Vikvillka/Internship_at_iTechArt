import { Box, Button, Typography } from '@mui/material';
import React from 'react';
import { registerModalStyles } from './RegisterModal.styles';

interface RegisterActionsProps {
  onSubmit: () => void;
  onSwitchToLogin?: () => void;
}

const RegisterActions: React.FC<RegisterActionsProps> = ({ onSubmit, onSwitchToLogin }) => {
  return (
    <>
      <Button
        variant='contained'
        fullWidth
        sx={registerModalStyles.submitButton}
        onClick={onSubmit}
      >
        Sign up
      </Button>
      <Typography variant='body2' sx={registerModalStyles.switchText}>
        Already have an account?{' '}
        <Box component='span' sx={registerModalStyles.linkLogIn} onClick={onSwitchToLogin}>
          Log in
        </Box>
      </Typography>
    </>
  );
};

export default RegisterActions;
