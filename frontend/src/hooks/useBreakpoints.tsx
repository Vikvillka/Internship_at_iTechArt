import { useMediaQuery, useTheme } from '@mui/material';

const useBreakpoints = () => {
  const theme = useTheme();

  return {
    isXs: useMediaQuery(theme.breakpoints.down('sm')),
    isSm: useMediaQuery(theme.breakpoints.between('sm', 'md')),
    isMd: useMediaQuery(theme.breakpoints.between('md', 'lg')),
  };
};

export default useBreakpoints;
