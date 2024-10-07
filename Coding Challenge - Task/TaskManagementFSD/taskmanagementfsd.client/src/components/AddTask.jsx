import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { apiClient, getToken } from '../Auth/Auth';
import { AuthContext } from '../Auth/AuthContext';

const AddTask = () => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [dueDate, setDueDate] = useState('');
    const [priority, setPriority] = useState('');
    const [status, setStatus] = useState('');
    const { isLoggedIn } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        if (!isLoggedIn) return; // Do nothing if not logged in
        e.preventDefault();
        const token = getToken();
        const newTask = { title, description, dueDate, priority, status };

        try {
            await apiClient.post('/TaskModels', newTask, {
                headers: { Authorization: `Bearer ${token}` },
            });
            navigate('/tasks'); // Navigate to tasks page
            setTimeout(() => {
                window.location.reload(); // Reload the page after navigating
            }, 100); // Delay for navigation to complete
        } catch (error) {
            console.error('Error adding task:', error);
        }
    };

    return (
        <div className="max-w-lg mx-auto p-6 bg-white shadow-lg rounded-lg">
            <h1 className="text-3xl font-bold text-blue-600 mb-6">Add Task</h1>
            <form onSubmit={handleSubmit}>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Title</label>
                    <input
                        type="text"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Description</label>
                    <textarea
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Due Date</label>
                    <input
                        type="date"
                        value={dueDate}
                        onChange={(e) => setDueDate(e.target.value)}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Priority</label>
                    <input
                        type="text"
                        value={priority}
                        onChange={(e) => setPriority(e.target.value)}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Status</label>
                    <input
                        type="text"
                        value={status}
                        onChange={(e) => setStatus(e.target.value)}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <button
                    type="submit"
                    className="bg-blue-600 text-white py-2 px-4 rounded-lg transition duration-300 hover:bg-blue-700 shadow hover:shadow-lg"
                >
                    Add Task
                </button>
            </form>
        </div>
    );
};

export default AddTask;
