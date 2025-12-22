import { Box } from '@mui/material';
import React, { useState } from 'react';
import LoginActions from './LoginActions';
import LoginFields from './LoginFields';
import { loginModalStyles } from './LoginModal.styles';

interface LoginFormProps {
  onSubmit: (login: string, password: string) => void;
  onSwitchToRegister?: () => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onSubmit, onSwitchToRegister }) => {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  return (
    <Box sx={loginModalStyles.modalContent}>
      <LoginFields
        login={login}
        password={password}
        showPassword={showPassword}
        onLoginChange={setLogin}
        onPasswordChange={setPassword}
        onToggleShowPassword={() => setShowPassword((p) => !p)}
      />

      <LoginActions
        onSubmit={() => onSubmit(login, password)}
        onSwitchToRegister={onSwitchToRegister}
      />
    </Box>
  );
};

export default LoginForm;
