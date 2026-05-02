import React from 'react';
import { useDispatch } from 'react-redux';
import UserMenu from '../../components/common/header/userMenu/UserMenu';
import { logout } from '../../store/features/auth/authSlice';

const UserMenuContainer: React.FC = () => {
  const dispatch = useDispatch();
  return <UserMenu onLogout={() => dispatch(logout())} />;
};

export default UserMenuContainer;
