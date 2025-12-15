import { SxProps, Theme } from '@mui/material';

export const pageContainer: SxProps<Theme> = {
  mt: 5,
  mb: 7,
  minHeight: '90vh',
};

export const loadingBox: SxProps<Theme> = {
  display: 'flex',
  justifyContent: 'center',
  mt: 10,
  minHeight: '90vh',
};

export const errorContainer: SxProps<Theme> = {
  mt: 5,
  mb: 7,
  minHeight: '90vh',
};

export const errorText: SxProps<Theme> = {
  color: 'error.main',
};
