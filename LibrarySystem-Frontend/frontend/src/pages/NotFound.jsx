import React from 'react';
import { Typography, Container } from '@mui/material';

const NotFound = () => {
  return (
    <Container>
      <Typography variant="h4" component="h1" align="center">
        404 - Page Not Found
      </Typography>
      <Typography variant="body1" align="center">
        Sorry, the page you are looking for does not exist.
      </Typography>
    </Container>
  );
};

export default NotFound;