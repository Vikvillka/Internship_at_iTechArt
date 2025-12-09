import { makeStyles } from '@mui/styles';
//test husky!!!!!
import { Event } from '../../../models/Event'

export const useStyles = makeStyles(() => ({
  container: {
    display: 'flex',
    flexDirection: 'row',
    gap: '32px',
    padding: '16px',
    '@media (max-width: 900px)': {
      flexDirection: 'column',
    },
  },
  leftColumn: {
    flex: 6,
    display: 'flex',
    flexDirection: 'column',
    gap: '16px',
    paddingRight: '30px',
  },
  title: {
    fontWeight: '700',
  },
  ownerBox: {
    display: 'flex',
    alignItems: 'center',
    gap: '8px',
  },
  detailTextBold: {
    fontWeight: '700',
  },
  detailsBox: {
    marginTop: '24px',
  },
  description: {
    marginTop: '20px',
    marginBottom: '8px',
  },
  rightColumn: {
    flex: 4,
    display: 'flex',
    flexDirection: 'column',
    gap: '16px',
    position: 'sticky',
    top: '20px',        
    alignSelf: 'flex-start',
    height: 'fit-content',
  },
  cardMedia: {
    borderRadius: '20px',
    maxHeight: '300px',
    objectFit: 'cover',
    marginBottom: '16px',
  },
  paperBox: {
    padding: '25px 20px',
    marginTop: '16px',
    borderRadius: '20px',
  },
  dateBox: {
    display: 'flex',
    alignItems: 'center',
    gap: '8px',
    marginTop: '16px',
  },
  tagsBox: {
    display: 'flex',
    flexWrap: 'wrap',
    gap: '8px',
  },
  chip: {
    color: '#000 !important',
    border: 'none !important',
    fontWeight: '600 !important',
  },
}));
