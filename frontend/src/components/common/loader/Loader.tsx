import { Box, CircularProgress } from '@mui/material';
import React from 'react';
import { loaderStyles } from './Loader.styles';

const Loader: React.FC = () => {
  return (
    <Box sx={loaderStyles.loaderBox}>
      <CircularProgress />
    </Box>
  );
};

export default Loader;
