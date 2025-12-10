import { SxProps, Theme } from '@mui/material';

export const pageContainer: SxProps<Theme> = {
  mt: 5,
};

export const loadingBox: SxProps<Theme> = {
  display: 'flex',
  justifyContent: 'center',
  mt: 10,
};

export const errorContainer: SxProps<Theme> = {
  mt: 5,
};

export const errorText: SxProps<Theme> = {
  color: 'error.main',
};
