import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import BooksPage from './pages/BooksPage';
import ReservationsPage from './pages/ReservationsPage';
import NotFound from './pages/NotFound';
import { CssBaseline } from '@mui/material';
import MenuBar from './components/Menu/MenuBar';
import { ThemeProvider } from '@mui/material/styles';
import theme from './config/libraryTheme';

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <Router>
        <CssBaseline />
        <MenuBar />
        <Routes>
          <Route path="/books" element={<BooksPage />} />
          <Route path="/reservations" element={<ReservationsPage />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
};

export default App;
