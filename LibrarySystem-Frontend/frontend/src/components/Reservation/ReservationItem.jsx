import React from 'react';
import { Card, CardMedia, CardContent, Typography, Button, Box } from '@mui/material';
import { makeStyles } from '@mui/styles';

const useStyles = makeStyles({
    card: {
        width: '300px',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'space-between',
        margin: '16px',
    },
    media: {
      height: 300,
      width: 'auto !important',
      maxWidth: '100%',
      objectFit: 'cover',  
      margin: '2% auto',
    },
    content: {
        textAlign: 'center',
        flexGrow: 1,
    },
    reserveButton: {
        padding: '16px',
    },
});

const ReservationItem = ({ reservation, book, onDelete }) => {
    const classes = useStyles();

    return (
        <Card className={classes.card}>
            <CardMedia
                component="img"
                className={classes.media}
                image={book.pictureUrl}
                alt={book.name}
            />
            <CardContent className={classes.content}>
                <Typography gutterBottom variant="h5" component="div">
                    {book.name}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Published: {book.year}
                </Typography>
                <Typography variant="body1">
                    {reservation.daysReserved} day(-s) reservation
                </Typography>
                <Typography variant="body1">
                    Quick pick-up: {reservation.quickPickup ? 'Yes' : 'No'}
                </Typography>
                <Typography variant="body1">
                    Total Cost: {reservation.totalCost} €
                </Typography>
            </CardContent>
            <Box className={classes.reserveButton}>
                <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    onClick={() => onDelete(reservation.reservationId)}
                >
                    Delete
                </Button>
            </Box>
        </Card>
    );
};

export default ReservationItem;
