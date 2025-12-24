import { SxProps, Theme } from '@mui/material';

export const eventDetailsStyles = {
  containerEventDetails: {
    display: 'flex',
    flexDirection: 'row',
    gap: '32px',
    p: 2,
    '@media (max-width:900px)': {
      flexDirection: 'column',
    },
  } as SxProps<Theme>,

  leftColumn: {
    flex: 6,
    display: 'flex',
    flexDirection: 'column',
    gap: '16px',
    pr: '30px',
  } as SxProps<Theme>,

  title: {
    fontWeight: 700,
  } as SxProps<Theme>,

  ownerBox: {
    display: 'flex',
    mt: 1,
    alignItems: 'center',
    gap: 1,
  } as SxProps<Theme>,

  detailTextBold: {
    fontWeight: 700,
    mt: 2,
    mb: 1,
  } as SxProps<Theme>,

  detailsBox: {
    mt: 3,
  } as SxProps<Theme>,

  description: {
    mt: 2.5,
    mb: 1,
  } as SxProps<Theme>,

  rightColumn: {
    flex: 4,
    display: 'flex',
    flexDirection: 'column',
    gap: '16px',
    position: 'sticky',
    top: '20px',
    alignSelf: 'flex-start',
    height: 'fit-content',
  } as SxProps<Theme>,

  cardMedia: {
    borderRadius: '20px',
    maxHeight: '300px',
    objectFit: 'cover',
    mb: 2,
  } as SxProps<Theme>,

  paperBox: {
    p: '25px 20px',
    mt: 2,
    borderRadius: '20px',
    boxShadow: '0px 0px 0px rgba(0,0,0,0)',
    border: '1px dashed #C9C9C9',
  } as SxProps<Theme>,

  dateBox: {
    display: 'flex',
    alignItems: 'center',
    gap: 1,
  } as SxProps<Theme>,

  tagsBox: {
    display: 'flex',
    flexWrap: 'wrap',
    gap: 1,
  } as SxProps<Theme>,

  chip: {
    color: '#000 !important',
    border: 'none !important',
    fontWeight: '600 !important',
  } as SxProps<Theme>,
};
