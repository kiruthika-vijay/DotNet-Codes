// src/App.js
import React, { useState } from 'react';
import MovieList from './MovieList';
import MovieForm from './MovieForm';
import axios from 'axios';

const App = () => {
    const [movieToEdit, setMovieToEdit] = useState(null);

    const handleEdit = (movie) => {
        setMovieToEdit(movie);
    };

    const handleDelete = async (id) => {
        try {
            await axios.delete(`https://localhost:7074/api/Movies/${id}`);
            window.location.reload();
        } catch (error) {
            console.error('Error deleting movie:', error);
        }
    };

    const handleSave = () => {
        setMovieToEdit(null);
        window.location.reload();
    };

    return (
        <div>
            <h1>Movies CRUD</h1>
            <div>
                <MovieForm movieToEdit={movieToEdit} onSave={handleSave} />
            </div>
            <div>
                <MovieList onEdit={handleEdit} onDelete={handleDelete} />
            </div>
        </div>
    );
};

export default App;
