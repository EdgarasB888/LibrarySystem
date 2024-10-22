import React from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';

const MenuBar = () => {
    return (
        <AppBar position="static">
            <Toolbar>
                <Typography
                    variant="h6"
                    component={Link}
                    to="/books"
                    sx={{ flexGrow: 1, textDecoration: 'none', color: 'inherit' }}
                >
                    Library System
                </Typography>

                <Button color="inherit" component={Link} to="/books">
                    Books
                </Button>
                <Button color="inherit" component={Link} to="/reservations">
                    Reservations
                </Button>
            </Toolbar>
        </AppBar>
    );
};

export default MenuBar;
