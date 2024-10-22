import React, { useEffect, useState } from 'react';
import reservationsService from '../../api/reservationsService';
import booksService from '../../api/booksService';
import { Box, Typography } from '@mui/material';
import ReservationItem from './ReservationItem';

const ReservationsList = () => {
  const [reservations, setReservations] = useState([]);
  const [books, setBooks] = useState({});

  const loadReservations = async () => {
    const result = await reservationsService.getAllReservations();
    setReservations(result);
    return result;
  };

  const loadBooks = async (reservations) => {
    const bookIds = reservations.map(reservation => reservation.bookId);

    const booksData = await Promise.all(
      bookIds.map(bookId => booksService.getBookById(bookId))
    );

    const booksMap = booksData.reduce((acc, book) => {
      if (book && book.bookId) {
        acc[book.bookId] = {
          name: book.name,
          year: book.year,
          pictureUrl: book.pictureUrl
        };
      }
      return acc;
    }, {});

    setBooks(booksMap);
  };

  useEffect(() => {
    const fetchReservationsAndBooks = async () => {
      const reservations = await loadReservations();
      await loadBooks(reservations);
    };

    fetchReservationsAndBooks();
  }, []);

  const deleteReservation = async (reservationId) => {
    try {
      await reservationsService.delete(reservationId);
      setReservations(prevReservations =>
        prevReservations.filter(reservation => reservation.reservationId !== reservationId)
      );
    } catch (error) {
      console.error("Failed to delete reservation:", error);
    }
  };

  return (
    <Box sx={{ p: 3 }}>
      <Typography variant="h4" sx={{ mb: 2 }}>Your Reservations</Typography>
      <Box sx={{ display: 'flex', flexWrap: 'wrap', justifyContent: 'flex-start' }}>
        {reservations.map((reservation) => (
          <ReservationItem
            key={reservation.reservationId}
            reservation={reservation}
            book={books[reservation.bookId] || { name: 'Unknown Book', year: 'N/A', pictureUrl: '' }}
            onDelete={deleteReservation}
          />
        ))}
      </Box>
    </Box>
  );
};

export default ReservationsList;
