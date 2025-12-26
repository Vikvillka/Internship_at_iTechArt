import { SxProps, Theme } from '@mui/material';

export const loaderStyles = {
  loaderBox: {
    position: 'fixed',
    top: 0,
    left: 0,
    width: '100vw',
    height: '100vh',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 999999,
  } as SxProps<Theme>,
};
