import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Logout.css'; // Optional: for additional styling
import { removeToken } from './Auth';
import { AuthContext } from './AuthContext';

const Logout = ({ isModalOpen, setIsModalOpen }) => {
    const { setIsLoggedIn } = useContext(AuthContext);
    const navigate = useNavigate();

    const confirmLogout = () => {
        removeToken();
        setIsLoggedIn(false);
        setIsModalOpen(false);
        setTimeout(() => {
            navigate('/login'); // Redirect to the login page
        }, 3000); // 3 seconds
    };

    return (
        <>
            {isModalOpen && (
                <div className="modal">
                    <div className="modal-content">
                        <h2>Are you sure you want to log out?</h2>
                        <button onClick={confirmLogout} className="confirm-button">Yes</button>
                        <button onClick={() => setIsModalOpen(false)} className="cancel-button">No</button>
                    </div>
                </div>
            )}
        </>
    );
};

export default Logout;
