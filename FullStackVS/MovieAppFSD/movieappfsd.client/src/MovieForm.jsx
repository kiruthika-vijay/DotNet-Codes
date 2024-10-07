// src/MovieForm.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './MovieForm.css'; // Add this for CSS styling

const MovieForm = ({ movieToEdit, onSave }) => {
    const [movie, setMovie] = useState({
        title: '',
        director: '',
        releaseYear: '',
        genre: '',
        rating: ''
    });

    useEffect(() => {
        if (movieToEdit) {
            setMovie(movieToEdit);
        }
    }, [movieToEdit]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setMovie({ ...movie, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (movie.movieID) {
            await axios.put(`https://localhost:7074/api/Movies/${movie.movieID}`, movie);
        } else {
            await axios.post('https://localhost:7074/api/Movies', movie);
        }
        onSave();
        setMovie({
            title: '',
            director: '',
            releaseYear: '',
            genre: '',
            rating: ''
        });
    };

    return (
        <form onSubmit={handleSubmit} className="movie-form">
            <h2>{movie.movieID ? 'Edit Movie' : 'Add Movie'}</h2>
            <input
                type="text"
                name="title"
                value={movie.title}
                onChange={handleChange}
                placeholder="Title"
                required
            />
            <input
                type="text"
                name="director"
                value={movie.director}
                onChange={handleChange}
                placeholder="Director"
                required
            />
            <input
                type="number"
                name="releaseYear"
                value={movie.releaseYear}
                onChange={handleChange}
                placeholder="Release Year"
                required
            />
            <input
                type="text"
                name="genre"
                value={movie.genre}
                onChange={handleChange}
                placeholder="Genre"
                required
            />
            <input
                type="number"
                name="rating"
                step="0.1"
                value={movie.rating}
                onChange={handleChange}
                placeholder="Rating"
                required
            />
            <button type="submit">{movie.movieID ? 'Update' : 'Add'}</button>
        </form>
    );
};

export default MovieForm;
