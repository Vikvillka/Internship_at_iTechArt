import { SxProps, Theme } from '@mui/material';

export const registerModalStyles = {
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
    pb: 3,
    pt: 1,
  } as SxProps<Theme>,

  modalContent: {
    mt: 2,
    display: 'flex',
    flexDirection: 'column',
    pt: 1,
    pb: 3,
    mx: 5,
  } as SxProps<Theme>,

  title: {
    mt: 4,
    mb: 2,
    textAlign: 'center',
    fontWeight: 'bold',
  } as SxProps<Theme>,

  inputContainer: {
    display: 'flex',
    flexDirection: 'column',
    gap: 1,
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
    mt: 1,
    textAlign: 'center',
  } as SxProps<Theme>,

  linkLogIn: {
    fontWeight: 'bold',
    cursor: 'pointer',
  } as SxProps<Theme>,

  radio: {
    color: 'primary.main',
    '&.Mui-checked': {
      color: 'primary.main',
    },
  } as SxProps<Theme>,

  genderTitle: {
    color: 'secondary.dark',
  } as SxProps<Theme>,

  errorText: {
    color: 'error.main',
    mt: 1,
    textAlign: 'center',
  } as SxProps<Theme>,

  errorBox: {
    mt: 1,
    display: 'flex',
    justifyContent: 'center',
  } as SxProps<Theme>,
};
