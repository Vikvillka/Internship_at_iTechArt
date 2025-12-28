import { Box, Typography } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectUserError } from '../../../../store/features/users/usersSelectors';
import { registerModalStyles } from './RegisterModal.styles';

const RegisterErrors: React.FC = () => {
  const registerError = useSelector(selectUserError);
  return (
    <Box sx={registerModalStyles.errorBox}>
      <Typography variant='body2' sx={registerModalStyles.errorText}>
        {registerError}
      </Typography>
    </Box>
  );
};

export default RegisterErrors;
