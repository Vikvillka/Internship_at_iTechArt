import { SxProps, Theme } from '@mui/material';

export const authButtonsStyles = {
  registerButton: {
    textTransform: 'none',
    borderRadius: '99px',
    px: 3,
  } as SxProps<Theme>,

  loginButton: {
    textTransform: 'none',
    border: 'none',
    borderRadius: '99px',
  } as SxProps<Theme>,

  authButtonsContainer: {
    display: 'flex',
    gap: 1,
  } as SxProps<Theme>,
};
