import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { Avatar, Box, Menu, MenuItem, Typography } from '@mui/material';
import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { selectActiveUser } from '../../../../store/features/auth/authSelectors';
import { headerStyles } from '../Header.styles';

interface UserMenuProps {
  onLogout: () => void;
}

const UserMenu: React.FC<UserMenuProps> = ({ onLogout }) => {
  const user = useSelector(selectActiveUser);
  const [menuAnchor, setMenuAnchor] = useState<null | HTMLElement>(null);
  const open = Boolean(menuAnchor);

  const handleOpen = (e: React.MouseEvent<HTMLElement>) => {
    setMenuAnchor(e.currentTarget);
  };

  const handleClose = () => {
    setMenuAnchor(null);
  };

  return (
    <>
      <Box sx={headerStyles.userMenu} onClick={handleOpen}>
        <Avatar sx={headerStyles.avatar}>{user?.username?.[0]?.toUpperCase()}</Avatar>
        <Box>
          <Typography>
            <strong>{user?.username}</strong>
          </Typography>
          <Typography variant='body2' color='text.secondary'>
            {user?.email}
          </Typography>
        </Box>
        <KeyboardArrowDownIcon />
      </Box>

      <Menu
        anchorEl={menuAnchor}
        open={open}
        onClose={handleClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        slotProps={{
          paper: {
            sx: {
              ...headerStyles.menuPaper,
              minWidth: menuAnchor?.clientWidth,
            },
          },
        }}
      >
        <MenuItem
          sx={headerStyles.menuItem}
          onClick={() => {
            handleClose();
            onLogout();
          }}
        >
          Logout
        </MenuItem>
        <MenuItem
          sx={headerStyles.menuItem}
          component={Link}
          to='/user-communities'
          onClick={handleClose}
        >
          My communities
        </MenuItem>
      </Menu>
    </>
  );
};

export default UserMenu;
