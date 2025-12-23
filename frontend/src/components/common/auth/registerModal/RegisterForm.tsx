import { Box } from '@mui/material';
import React, { useState } from 'react';
import { Gender } from '../../../../models/Gender';
import RegisterActions from './RegisterActions';
import RegisterFields from './RegisterFields';
import { registerModalStyles } from './RegisterModal.styles';

interface RegisterFormProps {
  onSubmit: (
    username: string,
    email: string,
    password: string,
    gender: Gender,
    location: string,
  ) => void;
  onSwitchToLogin?: () => void;
}

const RegisterForm: React.FC<RegisterFormProps> = ({ onSubmit, onSwitchToLogin }) => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [gender, setGender] = useState<Gender | null>(null);
  const [location, setLocation] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  return (
    <Box sx={registerModalStyles.modalContent}>
      <RegisterFields
        username={username}
        email={email}
        password={password}
        gender={gender}
        location={location}
        showPassword={showPassword}
        onUsernameChange={setUsername}
        onEmailChange={setEmail}
        onPasswordChange={setPassword}
        onGenderChange={setGender}
        onLocationChange={setLocation}
        onToggleShowPassword={() => setShowPassword((p) => !p)}
      />
      <RegisterActions
        onSubmit={() => {
          if (gender === null) return;
          onSubmit(username, email, password, gender, location);
        }}
        onSwitchToLogin={onSwitchToLogin}
      />
    </Box>
  );
};

export default RegisterForm;
