import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { apiClient, getToken } from '../Auth/Auth';
import { useContext } from 'react';
import { AuthContext } from '../Auth/AuthContext';

const EditTask = () => {
    const { id } = useParams();
    const [task, setTask] = useState({
        title: '',
        description: '',
        dueDate: '',
        priority: '',
        status: ''
    });
    const { isLoggedIn } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        const token = getToken();
        if (token && isLoggedIn) {
            fetchTaskToEdit(token);
        }
    }, [isLoggedIn, id]); // Only run when isLoggedIn changes

    const fetchTaskToEdit = async (token) => {
        try {
            const response = await apiClient.get(`/TaskModels/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
            setTask(response.data);
        } catch (error) {
            console.error('Error fetching task:', error);
        }
    };

    const handleUpdate = async (e) => {
        if (!isLoggedIn) return; // Do nothing if not logged in
        e.preventDefault();
        try {
            const token = getToken();
            await apiClient.put(`/TaskModels/${id}`, task, {
                headers: { Authorization: `Bearer ${token}` },
            });
            navigate('/tasks');
            setTimeout(() => {
                window.location.reload(); // Reload the page after navigating
            }, 100); // Delay for navigation to complete
        } catch (error) {
            console.error('Error updating task:', error);
        }
    };

    return (
        <div className="max-w-lg mx-auto p-6 bg-white shadow-lg rounded-lg">
            <h1 className="text-3xl font-bold text-blue-600 mb-6">Edit Task</h1>
            <form onSubmit={handleUpdate}>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Title</label>
                    <input
                        type="text"
                        value={task.title}
                        onChange={(e) => setTask({ ...task, title: e.target.value })}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Description</label>
                    <textarea
                        value={task.description}
                        onChange={(e) => setTask({ ...task, description: e.target.value })}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Due Date</label>
                    <input
                        type="date"
                        value={task.dueDate}
                        onChange={(e) => setTask({ ...task, dueDate: e.target.value })}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Priority</label>
                    <input
                        type="text"
                        value={task.priority}
                        onChange={(e) => setTask({ ...task, priority: e.target.value })}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block mb-2 text-gray-700">Status</label>
                    <input
                        type="text"
                        value={task.status}
                        onChange={(e) => setTask({ ...task, status: e.target.value })}
                        className="w-full border border-gray-300 rounded-lg py-2 px-3 transition duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>
                <button
                    type="submit"
                    className="bg-blue-600 text-white py-2 px-4 rounded-lg transition duration-300 hover:bg-blue-700 shadow hover:shadow-lg"
                >
                    Update Task
                </button>
            </form>
        </div>
    );
};

export default EditTask;
