import React from 'react';
import ReservationsList from '../components/Reservation/ReservationsList';
import { Container } from '@mui/material';

const ReservationsPage = () => {
  return (
    <Container>
      <ReservationsList />
    </Container>
  );
};

export default ReservationsPage;
