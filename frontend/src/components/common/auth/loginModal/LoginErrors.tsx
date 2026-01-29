import { Box, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectAuthError } from '../../../../store/features/auth/authSelectors';
import { loginModalStyles } from './LoginModal.styles';

const LoginErrors: React.FC = () => {
  const loginError = useSelector(selectAuthError);
  return (
    <Box sx={loginModalStyles.errorBox}>
      <Typography variant='body2' sx={loginModalStyles.errorText}>
        {loginError}
      </Typography>
    </Box>
  );
};

export default LoginErrors;
