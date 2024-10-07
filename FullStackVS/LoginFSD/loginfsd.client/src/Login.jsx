import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    // State variables for username, password, and error message
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    // Hook for navigation after successful login
    const navigate = useNavigate();

    // Handler for form submission
    const handleLogin = async (e) => {
        e.preventDefault(); // Prevent default form submission behavior

        try {
            // Make a POST request to the login endpoint
            const response = await axios.post('https://localhost:7279/api/Auth/login', {
                username,
                password,
            });

            // Clear any existing errors
            setError('');
            console.log('Login successful');

            // Navigate to the Home component/page
            navigate('/home');
        } catch (err) {
            // Handle errors (e.g., invalid credentials)
            setError('Invalid username or password');
            console.error('Login failed:', err);
        }
    };

    return (
        <div style={styles.container}>
            <h2>Login</h2>
            {/* Form element to handle submission */}
            <form onSubmit={handleLogin} style={styles.form}>
                <div style={styles.inputGroup}>
                    <label htmlFor="username" style={styles.label}>Username:</label>
                    <input
                        id="username"
                        type="text"
                        placeholder="Enter Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                        style={styles.input}
                    />
                </div>
                <div style={styles.inputGroup}>
                    <label htmlFor="password" style={styles.label}>Password:</label>
                    <input
                        id="password"
                        type="password"
                        placeholder="Enter Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                        style={styles.input}
                    />
                </div>
                {/* Display error message if any */}
                {error && <div style={styles.error}>{error}</div>}
                <div style={styles.buttonContainer}>
                    <button type="submit" style={styles.button}>LOGIN</button>
                </div>
            </form>
        </div>
    );
};

// Optional: Inline styles for basic styling
const styles = {
    container: {
        maxWidth: '400px',
        margin: '50px auto',
        padding: '20px',
        border: '1px solid #ccc',
        borderRadius: '5px',
        boxShadow: '0 0 10px rgba(0,0,0,0.1)',
        backgroundColor: '#f9f9f9',
    },
    form: {
        display: 'flex',
        flexDirection: 'column',
    },
    inputGroup: {
        marginBottom: '15px',
    },
    label: {
        marginBottom: '5px',
        fontWeight: 'bold',
    },
    input: {
        width: '100%',
        padding: '8px',
        boxSizing: 'border-box',
    },
    error: {
        color: 'red',
        marginBottom: '15px',
    },
    buttonContainer: {
        textAlign: 'center',
    },
    button: {
        padding: '10px 20px',
        backgroundColor: '#4CAF50',
        color: '#fff',
        border: 'none',
        borderRadius: '3px',
        cursor: 'pointer',
    },
};

export default Login;
