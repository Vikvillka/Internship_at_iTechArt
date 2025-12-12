import { Box, Button, useMediaQuery, useTheme } from '@mui/material';
import React, { useState } from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick-theme.css';
import 'slick-carousel/slick/slick.css';
import { categories } from '../../../helpers/sliderCategory/categories';
import { sliderStyles } from './CategorySlider.styles';
import SliderArrow from './SliderArrow';

interface CategorySliderProps {
  onSelect?: (category: string) => void;
}

const CategorySlider: React.FC<CategorySliderProps> = ({ onSelect }) => {
  const [selectedCategory, setSelectedCategory] = useState('All events');

  const theme = useTheme();
  const isXs = useMediaQuery(theme.breakpoints.down('sm'));
  const isSm = useMediaQuery(theme.breakpoints.between('sm', 'md'));
  const isMd = useMediaQuery(theme.breakpoints.between('md', 'lg'));

  const settings = {
    dots: false,
    infinite: false,
    speed: 300,
    slidesToShow: isXs ? 3 : isSm ? 4 : isMd ? 6 : 8,
    slidesToScroll: 1,
    draggable: true,
    nextArrow: <SliderArrow direction='right' />,
    prevArrow: <SliderArrow direction='left' />,
  };

  return (
    <Box position='relative'>
      <Slider {...settings}>
        {categories.map((cat) => {
          const IconComponent = cat.icon;
          const isSelected = selectedCategory === cat.label;

          return (
            <Box key={cat.label}>
              <Button
                onClick={() => {
                  setSelectedCategory(cat.label);
                  onSelect?.(cat.label);
                }}
                sx={{
                  ...sliderStyles.buttonBase,
                }}
              >
                <IconComponent />
                <Box mt={0.5} fontSize={12}>
                  {cat.label}
                </Box>
              </Button>
            </Box>
          );
        })}
      </Slider>
    </Box>
  );
};

export default CategorySlider;
