import { Box, Typography } from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { headerStyles } from './Header.styles';

const HeaderLogo: React.FC = () => {
  const navigate = useNavigate();

  return (
    <Box sx={headerStyles.logo} onClick={() => navigate('/')}>
      <Box sx={headerStyles.logoImage} component='img' src='/imgs/icon.png' alt='Meets logo' />
      <Typography variant='h5'>Meets</Typography>
    </Box>
  );
};

export default HeaderLogo;
