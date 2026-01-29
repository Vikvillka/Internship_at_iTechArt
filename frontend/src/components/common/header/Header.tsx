import { Box } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import AuthContainer from '../../../containers/auth/AuthContainer';
import UserMenuContainer from '../../../containers/auth/UserMenuContainer';
import { selectActiveUser, selectIsLoggedIn } from '../../../store/features/auth/authSelectors';
import { headerStyles } from './Header.styles';
import HeaderLogo from './HeaderLogo';
import HeaderSearch from './HeaderSearch';

const Header: React.FC = () => {
  const isLoggedIn = useSelector(selectIsLoggedIn);
  const user = useSelector(selectActiveUser);

  return (
    <Box sx={headerStyles.header}>
      <Box sx={headerStyles.logoSearchContainer}>
        <HeaderLogo />
        <HeaderSearch />
      </Box>
      {isLoggedIn ? <UserMenuContainer /> : <AuthContainer />}
    </Box>
  );
};

export default Header;
