import { Button } from '@mui/material';
import React from 'react';
import { sliderStyles } from './CategorySlider.styles';

interface SliderArrowProps {
  direction: 'left' | 'right';
  onClick?: () => void;
}

const SliderArrow: React.FC<SliderArrowProps> = ({ direction, onClick }) => {
  return (
    <Button
      onClick={onClick}
      sx={{
        ...sliderStyles.arrowBase,
        left: direction === 'left' ? -16 : undefined,
        right: direction === 'right' ? -16 : undefined,
      }}
    >
      {direction === 'left' ? '<' : '>'}
    </Button>
  );
};

export default SliderArrow;
