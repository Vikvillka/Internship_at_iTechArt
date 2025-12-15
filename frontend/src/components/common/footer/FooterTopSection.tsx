import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import { Box, Button, Typography } from '@mui/material';
import React from 'react';
import { footerStyles } from './Footer.styles';

interface FooterTopSectionProps {
  onCreateCommunity: () => void;
}

const FooterTopSection: React.FC<FooterTopSectionProps> = ({ onCreateCommunity }) => {
  return (
    <Box sx={footerStyles.topSection}>
      <Typography variant='h5'>Meets. The platform from people for people</Typography>
      <Button
        onClick={onCreateCommunity}
        sx={footerStyles.createButton}
        endIcon={<ArrowForwardIcon />}
      >
        Create your own Community group
      </Button>
    </Box>
  );
};

export default FooterTopSection;
