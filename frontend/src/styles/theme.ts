import { createTheme } from '@mui/material/styles';

export const theme = createTheme({
  palette: {
    primary: {
      main: '#8B42D6',
      light: '#9F69D6',
      dark: '#682ea1ff',
    },
    secondary: {
      main: '#726974ff',
      light: '#a99eaaff',
      dark: '#5a5555ff',
    },
    error: {
      main: '#c53e35ff',
    },
    success: {
      main: '#099a30ff',
    },
    customText: {
      primary: '#ddddddff',
      secondary: '#939393ff',
    },
  },
  typography: {
    h4: {
      fontWeight: 700,
    },
    h5: {
      fontWeight: 700,
    },
    h6: {
      fontWeight: 700,
    },
  },
});

declare module '@mui/material/styles' {
  interface Palette {
    customText: {
      primary: string;
      secondary: string;
    };
  }
  interface PaletteOptions {
    customText?: {
      primary?: string;
      secondary?: string;
    };
  }
}
