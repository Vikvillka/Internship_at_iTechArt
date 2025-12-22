import { SxProps, Theme } from '@mui/material';

export const loginModalStyles = {
  modalBox: {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 420,
    borderRadius: '25px',
    boxShadow: 24,
    backgroundColor: 'background.paper',
    px: 4,
    pb: 6,
    pt: 3,
  } as SxProps<Theme>,

  modalContent: {
    mt: 2,
    display: 'flex',
    flexDirection: 'column',
    pt: 4,
    pb: 2,
    mx: 5,
  } as SxProps<Theme>,

  title: {
    mt: 4,
    mb: 3,
    textAlign: 'center',
    fontWeight: 'bold',
  } as SxProps<Theme>,

  inputContainer: {
    display: 'flex',
    flexDirection: 'column',
    gap: 3,
  } as SxProps<Theme>,

  passwordContainer: {
    display: 'flex',
    flexDirection: 'column',
  } as SxProps<Theme>,

  submitButton: {
    mt: 2,
    borderRadius: '999px',
    textTransform: 'none',
    fontWeight: 'bold',
    py: 1,
  } as SxProps<Theme>,

  closeButton: {
    position: 'absolute',
    top: 8,
    right: 8,
  } as SxProps<Theme>,

  showPasswordContainer: {
    display: 'flex',
    alignItems: 'center',
    mt: 1,
  } as SxProps<Theme>,

  switchText: {
    mt: 2,
    textAlign: 'center',
  } as SxProps<Theme>,

  linkSignUp: {
    fontWeight: 'bold',
    cursor: 'pointer',
  } as SxProps<Theme>,
};
