import React, { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { storeToken, apiClient } from './Auth';
import { AuthContext } from './AuthContext';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { setIsLoggedIn } = useContext(AuthContext);
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await apiClient.post('/Aauth/login', {
                email,
                password,
            });

            storeToken(response.data);
            setIsLoggedIn(true);
            console.log("Login successful", response.data);

            // Navigate to the Home component/page
            navigate('/tasks');
        } catch (err) {
            setError('Invalid username or password');
            console.error('Login failed:', err);
        }
    };

    return (
        <div className="h-screen bg-background flex justify-center items-center">
            <div className="max-w-md w-full p-8 bg-white shadow-lg rounded-lg">
                <h2 className="text-4xl font-extrabold text-spacecadet mb-6 text-center">Login to Task Management App</h2>
                <form onSubmit={handleLogin} className="space-y-6">
                    <div>
                        <label className="block text-spacecadet text-sm mb-2 font-bold" htmlFor="email">Email Address</label>
                        <input
                            type="email"
                            id="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="Enter your email"
                            className="w-full p-3 border border-jasmine rounded bg-accent text-spacecadet placeholder-spacecadet focus:outline-none focus:ring-2 focus:ring-jasmine transition duration-300"
                            required
                        />
                    </div>
                    <div>
                        <label className="block text-spacecadet text-sm mb-2 font-bold" htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            placeholder="Enter your password"
                            className="w-full p-3 border border-jasmine rounded bg-accent text-spacecadet placeholder-spacecadet focus:outline-none focus:ring-2 focus:ring-jasmine transition duration-300"
                            required
                        />
                    </div>
                    <div className="flex justify-end">
                        <Link to="/forgot-password" className="text-sm text-spacecadet hover:underline font-semibold">Forgot Password?</Link>
                    </div>
                    {error && <p className="text-red-500 mt-4">{error}</p>}
                    <button
                        type="submit"
                        className="w-full bg-jasmine text-background py-3 rounded-lg hover:bg-darkjasmine transition duration-300 font-semibold"
                    >
                        Log In
                    </button>
                </form>
                <p className="text-sm text-center mt-6 text-spacecadet font-semibold">
                    Don't have an account? <Link to="/register" className="text-utorange hover:underline font-bold">Register here</Link>
                </p>
            </div>
        </div>
    );
}

export default Login;
