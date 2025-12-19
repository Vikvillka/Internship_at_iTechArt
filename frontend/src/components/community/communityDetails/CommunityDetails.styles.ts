import { SxProps, Theme } from '@mui/material';

export const communityDetailsStyles = {
  container: {
    paddingTop: '20px',
    paddingBottom: '20px',
  } as SxProps<Theme>,

  chip: {
    color: '#000 !important',
    border: 'none !important',
    fontWeight: '600 !important',
  } as SxProps<Theme>,

  infoBox: {
    display: 'flex',
    alignItems: 'center',
    gap: 1,
    my: '10px',
  } as SxProps<Theme>,

  infoContainer: {
    marginTop: '20px',
    display: 'flex',
    flexWrap: 'wrap',
    flexDirection: 'row',
    marginBottom: 5,
  } as SxProps<Theme>,

  ownerContainer: {
    marginTop: '20px',
    flex: '1 0 250px',
    height: 'fit-content',
    position: 'sticky',
    py: 2,
    px: 3,
    top: '10px',
    boxShadow: '0px 0px 0px rgba(0, 0, 0, 0)',
    border: '1px dashed #C9C9C9',
    borderRadius: '20px',
  } as SxProps<Theme>,

  ownerNameIconBox: {
    display: 'flex',
    alignItems: 'center',
    gap: '10px',
    marginTop: '10px',
  } as SxProps<Theme>,

  descriptionContainer: {
    flex: '3 0 400px',
    paddingRight: 4,
    mt: 2,
  } as SxProps<Theme>,

  titleSection: {
    mb: 2,
  } as SxProps<Theme>,
};
