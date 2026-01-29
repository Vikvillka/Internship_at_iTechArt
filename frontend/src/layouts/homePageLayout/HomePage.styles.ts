import { SxProps, Theme } from '@mui/material';

export const homePageStyles = {
  buttonGroup: {
    mb: 2,
  } as SxProps<Theme>,

  activeButton: {
    textTransform: 'none',
    px: 2,
    py: 1,
    fontWeight: 500,
    color: 'primary.main',
    borderColor: 'primary.main',

    '&:hover': {
      backgroundColor: 'primary.main',
      color: 'white',
    },
  } as SxProps<Theme>,

  inactiveButton: {
    textTransform: 'none',
    px: 2,
    py: 1,
    fontWeight: 500,
    color: 'text.secondary',
    borderColor: 'divider',
    '&:hover': {
      color: 'white',
      backgroundColor: 'primary.main',
    },
  } as SxProps<Theme>,

  content: {
    mt: 2,
  } as SxProps<Theme>,
};
