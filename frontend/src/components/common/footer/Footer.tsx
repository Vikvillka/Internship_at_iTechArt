import { Box } from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { footerStyles } from './Footer.styles';
import FooterColumn from './FooterColumn';
import FooterCopyright from './FooterCopyright';
import FooterTopSection from './FooterTopSection';
import SocialIcons from './SocialIcons';

const Footer: React.FC = () => {
  const navigate = useNavigate();

  const handleCreateCommunity = () => {
    // TODO: navigate to create community functionality
  };

  const accountItems = [
    { text: 'Registration', onClick: () => navigate('/signup') },
    { text: 'Log in', onClick: () => navigate('/signin') },
  ];

  const discoverItems = [
    { text: 'Communities', onClick: () => navigate('/communities') },
    { text: 'Events', onClick: () => navigate('/') },
  ];

  return (
    <Box component='footer' sx={footerStyles.footer}>
      <Box sx={footerStyles.container}>
        <FooterTopSection onCreateCommunity={handleCreateCommunity} />
        <Box sx={footerStyles.columnsContainer}>
          <FooterColumn title='Your Account' items={accountItems} />
          <FooterColumn title='Discover' items={discoverItems} />
          <Box sx={footerStyles.column}>
            <Box sx={footerStyles.columnTitle}>Follow us</Box>
            <SocialIcons />
          </Box>
        </Box>
        <FooterCopyright />
      </Box>
    </Box>
  );
};

export default Footer;
