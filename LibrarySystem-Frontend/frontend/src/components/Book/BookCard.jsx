import React from 'react'
import { Card, CardMedia, CardContent, Typography, Button, Box } from '@mui/material';
import { makeStyles } from '@mui/styles';

const useStyles = makeStyles({
    card: {
        maxWidth: 345,
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'space-between',
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
    },
    reserveButton: {
        padding: '16px',
    }
});

const BookCard = ({ book, onReserveClick }) => {
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
                    Year: {book.year}
                </Typography>
            </CardContent>
            <Box className={classes.reserveButton}>
                <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    onClick={() => onReserveClick(book)}
                >
                    Reserve
                </Button>
            </Box>
        </Card>
    );
};

export default BookCard;
