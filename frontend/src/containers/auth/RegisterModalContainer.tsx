import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import RegisterModal from '../../components/common/auth/registerModal/RegisterModal';
import SnackbarComponent from '../../components/common/snackbar/Snackbar';
import { parseLocation } from '../../helpers/parseLoacation';
import { setAuthError } from '../../store/features/auth/authSlice';
import { isRegisterModalOpen } from '../../store/features/ui/uiSelectors';
import { closeRegisterModal, switchToLoginModal } from '../../store/features/ui/uiSlice';
import { selectUserSuccess } from '../../store/features/users/usersSelectors';
import { registerUser, resetRegistrationSuccess } from '../../store/features/users/usersSlice';

const RegisterModalContainer: React.FC = () => {
  const dispatch = useDispatch();
  const isOpen = useSelector(isRegisterModalOpen);
  const success = useSelector(selectUserSuccess);
  const [open, setOpen] = React.useState(false);

  const handleClose = () => {
    dispatch(closeRegisterModal());
    dispatch(setAuthError(''));
  };

  const handleSubmit = (
    username: string,
    email: string,
    password: string,
    gender: number,
    location: string,
  ) => {
    const { city, country } = parseLocation(location);
    dispatch(
      registerUser({
        userRegistration: { username, email, password, gender, city, country },
        showLoader: true,
      }),
    );
  };

  const handleSwitchToLogin = () => {
    dispatch(switchToLoginModal());
    dispatch(setAuthError(''));
  };

  React.useEffect(() => {
    if (success) setOpen(true);
  }, [success]);

  return (
    <>
      <RegisterModal
        isOpen={isOpen}
        onClose={handleClose}
        onSubmit={handleSubmit}
        onSwitchToLogin={handleSwitchToLogin}
      />
      <SnackbarComponent
        open={open}
        onClose={() => {
          setOpen(false);
          dispatch(resetRegistrationSuccess());
        }}
        severity='success'
        message='Registration successful! Please log in.'
      />
    </>
  );
};

export default RegisterModalContainer;
