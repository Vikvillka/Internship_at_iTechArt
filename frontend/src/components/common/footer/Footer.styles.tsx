import { SxProps, Theme } from '@mui/material';

export const footerStyles = {
  footer: {
    backgroundColor: '#212121ff',
    color: 'customText.primary',
    py: 6,
    px: { xs: 2, sm: 4, md: 8 },
  } as SxProps<Theme>,

  container: {
    margin: '0 auto',
  } as SxProps<Theme>,

  topSection: {
    display: 'flex',
    flexDirection: { xs: 'column', md: 'row' },
    justifyContent: 'space-between',
    alignItems: { xs: 'flex-start', md: 'center' },
    mb: 4,
    pb: 3,
    borderBottom: '2px solid rgba(255, 255, 255, 0.1)',
  } as SxProps<Theme>,

  columnsContainer: {
    width: '50%',
    display: 'grid',
    gridTemplateColumns: { xs: '1fr', sm: 'repeat(2, 1fr)', md: 'repeat(3, 1fr)' },
    gap: { xs: 3, md: 4 },
    mb: 4,
  } as SxProps<Theme>,

  column: {
    display: 'flex',
    flexDirection: 'column',
    gap: 1.5,
  } as SxProps<Theme>,

  columnTitle: {
    fontSize: '1rem',
    fontWeight: 600,
    mb: 1,
    color: 'customText.primary',
  } as SxProps<Theme>,

  columnItem: {
    color: 'customText.secondary',
    textDecoration: 'none',
    cursor: 'pointer',
  } as SxProps<Theme>,

  createButton: {
    fontSize: '1rem',
    color: 'customText.primary',
  } as SxProps<Theme>,

  bottomSection: {
    display: 'flex',
    justifyContent: 'space-between',
    pt: 3,
    fontSize: '0.85rem',
    color: 'customText.secondary',
  } as SxProps<Theme>,

  socialIcons: {
    display: 'flex',
    gap: 2,
  } as SxProps<Theme>,

  socialIcon: {
    color: 'customText.secondary',
    transition: 'color 0.2s ease',
    '&:hover': {
      color: 'primary.main',
    },
  } as SxProps<Theme>,
};
