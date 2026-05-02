import { SxProps, Theme } from '@mui/material';

export const communityCardStyles = {
  card: {
    py: 3,
    px: 1,
    minHeight: '300px',
    boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)',
    borderRadius: '20px',
    '&:hover': {
      boxShadow: '0px 6px 16px rgba(0, 0, 0, 0.2)',
      transform: 'scale(1.01)',
      transition: 'transform 0.3s ease-in-out',
    },
  } as SxProps<Theme>,

  title: {
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

  discription: {
    margin: '10px 0px',
    overflow: 'hidden',
    display: '-webkit-box',
    WebkitLineClamp: 8,
    color: 'text.secondary',
    WebkitBoxOrient: 'vertical',
  } as SxProps<Theme>,

  iconLableBox: {
    display: 'flex',
    marginTop: '4px',
    alignItems: 'center',
  } as SxProps<Theme>,

  subscription: {
    display: 'flex',
    marginTop: '4px',
    alignItems: 'center',
  } as SxProps<Theme>,
};
