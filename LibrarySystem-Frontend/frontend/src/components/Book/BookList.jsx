import React, { useState, useEffect } from 'react';
import BookCard from './BookCard';
import BooksService from '../../api/booksService';  
import { TextField, Button, Grid, Box } from '@mui/material';
import { makeStyles } from '@mui/styles';
import SearchIcon from '@mui/icons-material/Search';

const useStyles = makeStyles({
    searchBar: {
      display: 'flex',
      gap: '16px',
      marginBottom: '16px',
    },
    gridContainer: {
      marginTop: '16px',
    }
  });

  const BookList = ({ onReserveClick }) => {
    const classes = useStyles();
    const [books, setBooks] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
  
    useEffect(() => {
      const loadBooks = async () => {
        const result = await BooksService.getAllBooks();
        setBooks(result);
      };
      loadBooks();
    }, []);
  
    const handleSearch = async () => {
      const result = await BooksService.search(searchQuery);
      setBooks(result);
    };
  
    return (
      <Box p={3}>
        <Box className={classes.searchBar}>
          <TextField
            label="Search by name, year, type"
            variant="outlined"
            fullWidth
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
          <Button variant="contained" color="primary" onClick={handleSearch}>
          {<SearchIcon />}
          </Button>
        </Box>
        <Grid container spacing={3} className={classes.gridContainer}>
          {books.map((book) => (
            <Grid item xs={12} sm={6} md={4} key={book.id}>
              <BookCard book={book} onReserveClick={onReserveClick} />
            </Grid>
          ))}
        </Grid>
      </Box>
    );
  };
  
  export default BookList;