import { Box, FormControlLabel, Radio, RadioGroup, Typography } from '@mui/material';
import React from 'react';
import { Gender } from '../../../../models/Gender';
import { registerModalStyles } from './RegisterModal.styles';

interface GenderSelectProps {
  value: Gender | null;
  onChange: (gender: Gender) => void;
}

const GenderSelect: React.FC<GenderSelectProps> = ({ value, onChange }) => {
  return (
    <Box>
      <Typography sx={registerModalStyles.genderTitle} variant='body2'>
        Gender
      </Typography>

      <RadioGroup
        row
        value={value ?? ''}
        onChange={(e) => onChange(Number(e.target.value) as Gender)}
      >
        <FormControlLabel
          value={Gender.Male}
          control={<Radio sx={registerModalStyles.radio} size='small' />}
          label='Male'
        />
        <FormControlLabel
          value={Gender.Female}
          control={<Radio sx={registerModalStyles.radio} size='small' />}
          label='Female'
        />
        <FormControlLabel
          value={Gender.Other}
          control={<Radio sx={registerModalStyles.radio} size='small' />}
          label='Other'
        />
      </RadioGroup>
    </Box>
  );
};

export default GenderSelect;
