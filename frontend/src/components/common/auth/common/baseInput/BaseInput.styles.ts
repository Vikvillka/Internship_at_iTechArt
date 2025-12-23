import { SxProps, Theme } from '@mui/material/styles';

export const baseInputStyles = {
  field: {
    position: 'relative',
    width: '100%',
    transition: 'transform 0.2s ease',
    '&:focus-within': {
      transform: 'scale(1.015)',
    },
  } as SxProps<Theme>,

  outline: {
    position: 'absolute',
    inset: 0,
    borderRadius: '12px',
    pointerEvents: 'none',
    border: '1px solid',
    borderColor: 'secondary.light',
    transition: 'all 0.25s ease',
  } as SxProps<Theme>,

  fieldFocused: {
    borderColor: 'primary.main',
    boxShadow: '0 0 0 4px rgba(200, 116, 242, 0.15)',
  } as SxProps<Theme>,

  input: {
    width: '100%',
    height: 40,
    px: 2,
    borderRadius: '12px',
    border: 'none',
    fontSize: 16,
    outline: 'none',
    backgroundColor: 'transparent',
    position: 'relative',
    zIndex: 1,
  } as SxProps<Theme>,

  inputWithLeftIcon: {
    pl: 5,
  } as SxProps<Theme>,

  inputWithRightIcon: {
    pr: 5,
  } as SxProps<Theme>,

  labelRow: {
    display: 'flex',
    alignItems: 'center',
    gap: 0.5,
    mb: 0.2,
  } as SxProps<Theme>,

  label: {
    fontSize: 14,
    color: 'secondary.dark',
    transition: 'color 0.2s ease, transform 0.2s ease',
  } as SxProps<Theme>,

  labelFocused: {
    color: 'primary.main',
    transform: 'translateY(-1px)',
  } as SxProps<Theme>,

  iconInside: {
    position: 'absolute',
    top: '50%',
    transform: 'translateY(-40%)',
    zIndex: 2,
    color: 'secondary.main',
  } as SxProps<Theme>,

  iconLeft: {
    left: 12,
  } as SxProps<Theme>,

  iconRight: {
    right: 12,
  } as SxProps<Theme>,

  caption: {
    mt: 0.5,
    fontSize: 12,
    color: 'secondary.main',
  } as SxProps<Theme>,

  tooltip: {
    cursor: 'pointer',
  } as SxProps<Theme>,
};
