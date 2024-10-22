import React, { useState } from 'react';
import BookList from '../components/Book/BookList';
import ReservationForm from '../components/Reservation/ReservationForm';
import ReservationsService from '../api/reservationsService';
import { Container } from '@mui/material';

const BooksPage = () => {
  const [selectedBook, setSelectedBook] = useState(null);

  const handleReserveClick = (book) => {
    setSelectedBook(book);
  };

  const handleReservationSubmit = async (reservationData) => {
    await ReservationsService.createReservation(reservationData);
    setSelectedBook(null);
};

  return (
    <Container>
      {selectedBook ? (
        <ReservationForm book={selectedBook} onSubmit={handleReservationSubmit} />
      ) : (
        <BookList onReserveClick={handleReserveClick} />
      )}
    </Container>
  );
};

export default BooksPage;
