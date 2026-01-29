import { SxProps, Theme } from '@mui/material';

export const cardStyles = {
  card: {
    m: '10px 5px',
    minHeight: '350px',
    boxShadow: '0px 4px 12px rgba(0,0,0,0.1)',
    borderRadius: '5%',
    transition: 'transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out',
    '&:hover': {
      boxShadow: '0px 6px 16px rgba(0,0,0,0.2)',
      transform: 'scale(1.01)',
    },
  } as SxProps<Theme>,

  cardMedia: {
    height: '180px',
  } as SxProps<Theme>,

  title: {
    mt: 1,
    fontWeight: 'bold',
    fontSize: '18px',
    lineHeight: '22px',
    height: '45px',
    overflow: 'hidden',
    display: '-webkit-box',
    WebkitLineClamp: 2,
    WebkitBoxOrient: 'vertical',
  } as SxProps<Theme>,

  communityName: {
    my: '10px',
    height: '20px',
    overflow: 'hidden',
    display: '-webkit-box',
    WebkitLineClamp: 2,
    WebkitBoxOrient: 'vertical',
  } as SxProps<Theme>,

  participants: {
    display: 'flex',
    mt: '4px',
    alignItems: 'center',
  } as SxProps<Theme>,
};
