import { Box, Tooltip, Typography } from '@mui/material';
import React, { ReactNode } from 'react';
import { baseInputStyles } from './BaseInput.styles';

interface BaseInputProps {
  value: string;
  label?: string;
  placeholder?: string;
  type?: string;
  onChange: (value: string) => void;
  labelIcon?: ReactNode;
  labelTooltip?: string;
  startIcon?: ReactNode;
  caption?: ReactNode;
}

const BaseInput: React.FC<BaseInputProps> = ({
  value,
  label,
  placeholder,
  type = 'text',
  onChange,
  labelIcon,
  labelTooltip,
  startIcon,
  caption,
}) => {
  const [focused, setFocused] = React.useState(false);
  const hasStartIcon = Boolean(startIcon);

  return (
    <Box>
      {label && (
        <Box sx={baseInputStyles.labelRow}>
          <Typography
            sx={{
              ...(baseInputStyles.label as any),
              ...(focused ? (baseInputStyles.labelFocused as any) : {}),
            }}
          >
            {label}
          </Typography>

          {labelIcon && (
            <Tooltip title={labelTooltip ?? ''} arrow>
              <Box component='span' sx={baseInputStyles.tooltip}>
                {labelIcon}
              </Box>
            </Tooltip>
          )}
        </Box>
      )}

      <Box sx={baseInputStyles.field}>
        <Box
          sx={{
            ...(baseInputStyles.outline as any),
            ...(focused ? (baseInputStyles.fieldFocused as any) : {}),
          }}
        />

        {startIcon && (
          <Box
            sx={{
              ...(baseInputStyles.iconInside as any),
              ...(baseInputStyles.iconLeft as any),
            }}
          >
            {startIcon}
          </Box>
        )}

        <Box
          component='input'
          type={type}
          value={value}
          placeholder={placeholder}
          onFocus={() => setFocused(true)}
          onBlur={() => setFocused(false)}
          onChange={(e) => onChange(e.target.value)}
          sx={{
            ...(baseInputStyles.input as any),
            ...(hasStartIcon ? baseInputStyles.inputWithLeftIcon : ({} as any)),
          }}
        />
      </Box>

      {caption && <Typography sx={baseInputStyles.caption}>{caption}</Typography>}
    </Box>
  );
};

export default BaseInput;
