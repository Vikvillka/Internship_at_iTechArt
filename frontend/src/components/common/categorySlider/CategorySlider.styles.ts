import { SxProps, Theme } from '@mui/material';

export const sliderStyles = {
  buttonBase: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: 2,
    width: '100%',
    py: 1,
    px: 0.5,
    color: 'secondary.main',
  } as SxProps<Theme>,

  arrowBase: {
    minWidth: 0,
    width: 32,
    height: 32,
    borderRadius: '50%',
    position: 'absolute',
    top: '50%',
    transform: 'translateY(-50%)',
    zIndex: 1,
    bgcolor: 'secondary.main',
    color: 'white',
  } as SxProps<Theme>,
};
