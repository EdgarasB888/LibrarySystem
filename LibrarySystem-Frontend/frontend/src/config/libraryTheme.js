import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: {
      main: '#6F4C3E',
    },
    secondary: {
      main: '#B2B2B2',
    },
    background: {
      default: '#F2F2F2',
      paper: '#FFFFFF',
    },
    text: {
      primary: '#333333',
      secondary: '#666666',
    },
  },
  typography: {
    fontFamily: 'Georgia, serif',
  },
});

export default theme;