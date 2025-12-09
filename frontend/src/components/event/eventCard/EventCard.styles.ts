import { makeStyles } from '@mui/styles';

export const useStyles = makeStyles(() => ({
  card: {
    margin: '10px 5px',
    minHeight: '350px',
    boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)',
    borderRadius: '5%',
    '&:hover': {
      boxShadow: '0px 6px 16px rgba(0, 0, 0, 0.2)',
      transform: 'scale(1.01)',
      transition: 'transform 0.3s ease-in-out',
    },
  },
  cardMedia: {
    height: '180px',
  },
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
  },
  communityName: {
    margin: '10px 0px',
    height: '20px',
    overflow: 'hidden',
    display: '-webkit-box',
    WebkitLineClamp: 2,
    WebkitBoxOrient: 'vertical',
  },
  participants: {
    display: 'flex',
    marginTop: '4px',
    alignItems: 'center',
  },
  link: {
    textDecoration: 'none',
  },
}));
