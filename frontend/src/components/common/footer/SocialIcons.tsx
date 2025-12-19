import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import LinkedInIcon from '@mui/icons-material/LinkedIn';
import TwitterIcon from '@mui/icons-material/Twitter';
import { Box, IconButton } from '@mui/material';
import React from 'react';
import { footerStyles } from './Footer.styles';

const SocialIcons: React.FC = () => {
  const socialLinks = [
    { icon: <FacebookIcon />, href: 'https://facebook.com' },
    { icon: <TwitterIcon />, href: 'https://twitter.com' },
    { icon: <InstagramIcon />, href: 'https://instagram.com' },
    { icon: <LinkedInIcon />, href: 'https://linkedin.com' },
  ];

  return (
    <Box sx={footerStyles.socialIcons}>
      {socialLinks.map((social, index) => (
        <IconButton
          key={index}
          href={social.href}
          size='small'
          target='_blank'
          rel='noopener noreferrer'
          sx={footerStyles.socialIcon}
        >
          {social.icon}
        </IconButton>
      ))}
    </Box>
  );
};

export default SocialIcons;
