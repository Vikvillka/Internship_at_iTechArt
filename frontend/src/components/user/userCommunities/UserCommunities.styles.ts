import { SxProps, Theme } from '@mui/material';

export const userCommunitiesStyles = {
  card: {
    py: 2,
    px: 1,
    boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)',
    minHeight: '200px',
    borderRadius: '20px',
    '&:hover': {
      boxShadow: '0px 6px 16px rgba(0, 0, 0, 0.2)',
      transform: 'scale(1.01)',
      transition: 'transform 0.3s ease-in-out',
    },
  } as SxProps<Theme>,

  cardTitle: {
    marginTop: '8px',
    fontWeight: 'bold',
    fontSize: '18px',
    lineHeight: '22px',
    height: '45px',
    overflow: 'hidden',
    display: '-webkit-box',
    WebkitLineClamp: 2,
    WebkitBoxOrient: 'vertical',
  } as SxProps<Theme>,

  iconLableBox: {
    display: 'flex',
    marginTop: '4px',
    alignItems: 'center',
  } as SxProps<Theme>,
};
