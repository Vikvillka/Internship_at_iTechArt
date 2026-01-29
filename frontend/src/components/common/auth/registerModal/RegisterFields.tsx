import { InfoOutlined } from '@mui/icons-material';
import LocationIcon from '@mui/icons-material/LocationOn';
import { Box, Switch, Typography } from '@mui/material';
import React from 'react';
import { Gender } from '../../../../models/Gender';
import BaseInput from '../common/baseInput/BaseInput';
import GenderSelect from './RegisterGenderSelect';
import { registerModalStyles } from './RegisterModal.styles';

interface RegisterFieldsProps {
  username: string;
  email: string;
  password: string;
  gender: Gender | null;
  location: string;
  showPassword: boolean;
  onUsernameChange: (v: string) => void;
  onEmailChange: (v: string) => void;
  onPasswordChange: (v: string) => void;
  onGenderChange: (v: Gender) => void;
  onLocationChange: (v: string) => void;
  onToggleShowPassword: () => void;
}

const RegisterFields: React.FC<RegisterFieldsProps> = ({
  username,
  email,
  password,
  gender,
  location,
  showPassword,
  onUsernameChange,
  onEmailChange,
  onPasswordChange,
  onGenderChange,
  onLocationChange,
  onToggleShowPassword,
}) => {
  return (
    <Box sx={registerModalStyles.inputContainer}>
      <BaseInput
        label='Username'
        value={username}
        onChange={onUsernameChange}
        caption='Your username will be public on your Meets profile'
      />
      <BaseInput label='Email' value={email} onChange={onEmailChange} />
      <Box sx={registerModalStyles.passwordContainer}>
        <BaseInput
          label='Password'
          type={showPassword ? 'text' : 'password'}
          value={password}
          onChange={onPasswordChange}
          labelIcon={<InfoOutlined fontSize='small' />}
          labelTooltip='At least 8 characters and must use a mix of upper case, lower case, numbers, and symbols.'
        />
        <Box sx={registerModalStyles.showPasswordContainer}>
          <Typography variant='body2'>Show password</Typography>
          <Switch checked={showPassword} onChange={onToggleShowPassword} />
        </Box>
      </Box>
      <GenderSelect value={gender} onChange={onGenderChange} />
      <BaseInput
        label='Location'
        value={location}
        labelIcon={<InfoOutlined fontSize='small' />}
        labelTooltip='Please enter an example: "city, country".'
        onChange={onLocationChange}
        startIcon={<LocationIcon fontSize='small' />}
        caption='We won’t use your location for anything else, sorry!'
      />
    </Box>
  );
};

export default RegisterFields;
