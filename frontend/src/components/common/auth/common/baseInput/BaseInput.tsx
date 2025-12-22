import { Box, Typography } from '@mui/material';
import React from 'react';
import { baseInputStyles } from './BaseInput.styles';

interface BaseInputProps {
  value: string;
  label?: string;
  placeholder?: string;
  type?: string;
  onChange: (value: string) => void;
}

const BaseInput: React.FC<BaseInputProps> = ({
  value,
  label,
  placeholder,
  type = 'text',
  onChange,
}) => {
  const [focused, setFocused] = React.useState(false);

  return (
    <Box>
      {label && (
        <Typography
          sx={{
            ...(baseInputStyles.label as any),
            ...(focused ? (baseInputStyles.labelFocused as any) : {}),
          }}
        >
          {label}
        </Typography>
      )}

      <Box sx={baseInputStyles.field}>
        <Box
          sx={{
            ...(baseInputStyles.outline as any),
            ...(focused ? (baseInputStyles.fieldFocused as any) : {}),
          }}
        />
        <Box
          component='input'
          type={type}
          value={value}
          placeholder={placeholder}
          onFocus={() => setFocused(true)}
          onBlur={() => setFocused(false)}
          onChange={(e) => onChange(e.target.value)}
          sx={baseInputStyles.input}
        />
      </Box>
    </Box>
  );
};

export default BaseInput;
