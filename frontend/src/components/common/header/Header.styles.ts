import { SxProps, Theme } from '@mui/material';

export const headerStyles = {
  header: {
    height: 75,
    px: 6,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
  } as SxProps<Theme>,

  logo: {
    display: 'flex',
    alignItems: 'center',
    cursor: 'pointer',
  } as SxProps<Theme>,

  logoImage: {
    height: 40,
    width: 40,
    mr: 1,
  } as SxProps<Theme>,

  searchWrapper: {
    maxWidth: 700,
    mx: 6,
    display: 'flex',
    alignItems: 'center',
    borderRadius: '99px',
    border: '1px solid rgba(0, 0, 0, 0.15)',
    overflow: 'hidden',
    backgroundColor: '#fff',
  } as SxProps<Theme>,

  searchInput: {
    flex: 1,
    py: 1,
    px: 3,
  } as SxProps<Theme>,

  locationInput: {
    flex: 1,
    px: 3,
    py: 1,
    borderLeft: '1px solid rgba(0, 0, 0, 0.1)',
  } as SxProps<Theme>,

  logoSearchContainer: {
    display: 'flex',
    justifyContent: 'start',
    alignItems: 'center',
    flex: 1,
  } as SxProps<Theme>,

  searchButton: {
    bgcolor: 'primary.main',
    height: 35,
    width: 35,
    mr: 0.5,
    color: '#fff',
    '&:hover': {
      bgcolor: 'primary.dark',
    },
  } as SxProps<Theme>,
};
