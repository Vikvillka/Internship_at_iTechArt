import { Box, Button } from '@mui/material';
import React from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick-theme.css';
import 'slick-carousel/slick/slick.css';
import { categories } from '../../../helpers/sliderCategory/categories';
import useBreakpoints from '../../../hooks/useBreakpoints';
import { sliderStyles } from './CategorySlider.styles';
import SliderArrow from './SliderArrow';

interface CategorySliderProps {
  onSelect?: (category: string) => void;
}

const CategorySlider: React.FC<CategorySliderProps> = ({ onSelect }) => {
  const { isXs, isSm, isMd } = useBreakpoints();

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

          return (
            <Box key={cat.label}>
              <Button
                onClick={() => {
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
