import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import AuthButtons from '../../components/common/auth/AuthButtons';
import LoginModal from '../../components/common/auth/loginModal/LoginModal';
// import { loginUser, registerUser } from '../../store/authSlice';

const AuthButtonsContainer: React.FC = () => {
  const dispatch = useDispatch();
  const [isLoginOpen, setIsLoginOpen] = useState(false);

  const handleLoginOpen = () => setIsLoginOpen(true);
  const handleLoginClose = () => setIsLoginOpen(false);

  const handleSubmitLogin = (login: string, password: string) => {
    // dispatch(loginUser({ login, password }));
    handleLoginClose();
  };

  const handleSubmitRegister = (login: string, password: string) => {
    //  dispatch(registerUser({ login, password }));
    handleLoginClose();
  };

  return (
    <>
      <AuthButtons onLoginClick={handleLoginOpen} onRegisterClick={handleLoginOpen} />
      <LoginModal isOpen={isLoginOpen} onClose={handleLoginClose} onSubmit={handleSubmitLogin} />
    </>
  );
};

export default AuthButtonsContainer;
