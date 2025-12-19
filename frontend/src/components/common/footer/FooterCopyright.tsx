import { Box, Typography } from '@mui/material';
import React from 'react';
import { footerStyles } from './Footer.styles';

const FooterCopyright: React.FC = () => {
  return (
    <Box sx={footerStyles.bottomSection}>
      <Typography>© 2025 Meets</Typography>
      <Typography>Made with 🤍 by Vikvillka & AllekseyLeonov</Typography>
    </Box>
  );
};

export default FooterCopyright;
