import { SxProps } from '@mui/material';
import { Theme } from '@mui/material/styles';

export const paginationStyles = {
  paginationContainer: {
    display: 'flex',
    justifyContent: 'center',
    mt: 4,
  } as SxProps<Theme>,

  pagination: {
    color: 'primary.main',
  } as SxProps<Theme>,
};
