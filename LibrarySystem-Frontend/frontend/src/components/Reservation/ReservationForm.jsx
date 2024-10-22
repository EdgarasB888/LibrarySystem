import React, { useState } from 'react';
import { Box, Button, TextField, FormControl, InputLabel, Select, MenuItem, Checkbox, FormControlLabel } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { ReservationType } from '../../config/constants';

const useStyles = makeStyles({
  formControl: {
    marginBottom: '16px !important',
  },
  submitButton: {
    marginTop: '16px',
  },
});

const ReservationForm = ({ book, onSubmit }) => {
  const classes = useStyles();
  const [type, setType] = useState(ReservationType.BOOK);
  const [days, setDays] = useState(1);
  const [quickPickup, setQuickPickup] = useState(false);

  const handleSubmit = () => {
    const reservationData = {
      bookId: book.bookId,
      type,
      daysReserved: days,
      quickPickup,
    };
    onSubmit(reservationData);
  };

  return (
    <Box p={3}>
      <h2>Reserve {book.name}</h2>
      <FormControl fullWidth className={classes.formControl}>
        <InputLabel>Type</InputLabel>
        <Select
          value={type}
          onChange={(e) => setType(e.target.value)}
        >
          <MenuItem value={ReservationType.BOOK}>Book (€2/day)</MenuItem>
          <MenuItem value={ReservationType.AUDIOBOOK}>Audiobook (€3/day)</MenuItem>
        </Select>
      </FormControl>

      <TextField
        label="Days"
        type="number"
        value={days}
        onChange={(e) => {
          const value = Number(e.target.value);
          if (value >= 1) {
            setDays(value);
          }
        }}
        fullWidth
        className={classes.formControl}
        inputProps={{ min: 1 }}
      />

      <FormControlLabel
        control={
          <Checkbox
            checked={quickPickup}
            onChange={() => setQuickPickup(!quickPickup)}
          />
        }
        label="Quick Pick-up (€5)"
      />

      <Button
        variant="contained"
        color="primary"
        fullWidth
        onClick={handleSubmit}
        className={classes.submitButton}
      >
        Reserve
      </Button>
    </Box>
  );
};

export default ReservationForm;
