import { Box } from '@mui/material';
import React from 'react';
import AuthContainer from '../../../containers/auth/AuthContainer';
import { headerStyles } from './Header.styles';
import HeaderLogo from './HeaderLogo';
import HeaderSearch from './HeaderSearch';

const Header: React.FC = () => {
  return (
    <Box sx={headerStyles.header}>
      <Box sx={headerStyles.logoSearchContainer}>
        <HeaderLogo />
        <HeaderSearch />
      </Box>
      <AuthContainer />
    </Box>
  );
};

export default Header;
