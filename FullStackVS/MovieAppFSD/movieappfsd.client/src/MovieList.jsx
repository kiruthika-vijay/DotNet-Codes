// src/MovieList.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './MovieList.css'; // Add this for CSS styling

const MovieList = ({ onEdit, onDelete }) => {
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        fetchMovies();
    }, []);

    const fetchMovies = async () => {
        try {
            const response = await axios.get('https://localhost:7074/api/Movies');
            setMovies(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching movies:', error);
        }
    };

    return (
        <div className="movie-list-container">
            <h2>Movies List</h2>
            <ul className="movie-list">
                {movies.map((movie) => (
                    <li key={movie.movieID} className="movie-item">
                        {movie.title} ({movie.releaseYear}) - {movie.genre} - Directed by {movie.director}
                        <div className="movie-actions">
                            <button onClick={() => onEdit(movie)} className="edit-btn">Edit</button>
                            <button onClick={() => onDelete(movie.movieID)} className="delete-btn">Delete</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MovieList;
