import { Box, Switch, Typography } from '@mui/material';
import React from 'react';
import BaseInput from '../common/baseInput/BaseInput';
import { loginModalStyles } from './LoginModal.styles';

interface LoginFieldsProps {
  login: string;
  password: string;
  showPassword: boolean;
  onLoginChange: (v: string) => void;
  onPasswordChange: (v: string) => void;
  onToggleShowPassword: () => void;
}

const LoginFields: React.FC<LoginFieldsProps> = ({
  login,
  password,
  showPassword,
  onLoginChange,
  onPasswordChange,
  onToggleShowPassword,
}) => {
  return (
    <Box sx={loginModalStyles.inputContainer}>
      <BaseInput label='Username' value={login} onChange={onLoginChange} />

      <Box sx={loginModalStyles.passwordContainer}>
        <BaseInput
          label='Password'
          type={showPassword ? 'text' : 'password'}
          value={password}
          onChange={onPasswordChange}
        />
        <Box sx={loginModalStyles.showPasswordContainer}>
          <Typography variant='body2'>Show password</Typography>
          <Switch checked={showPassword} onChange={onToggleShowPassword} />
        </Box>
      </Box>
    </Box>
  );
};

export default LoginFields;
