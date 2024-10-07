import React, { useContext, useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { apiClient, getToken, removeToken } from '../Auth/Auth';
import { AuthContext } from '../Auth/AuthContext';
import { FiLogOut, FiEdit, FiTrash } from 'react-icons/fi'; // Import icons

const TaskList = () => {
    const [tasks, setTasks] = useState([]);
    const { isLoggedIn, setIsLoggedIn } = useContext(AuthContext);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const token = getToken();
        if (token && isLoggedIn) {
            fetchTasks(token);
        }
    }, [isLoggedIn]); // Only run when isLoggedIn changes

    const fetchTasks = async (token) => {
        try {
            // Fetch tasks from API
            const response = await apiClient.get('/TaskModels', {
                headers: { Authorization: `Bearer ${token}` },
            });
            setTasks(response.data);
        } catch (error) {
            console.error('Error fetching task details:', error);
        }
    };

    const deleteTask = async (id) => {
        if (!isLoggedIn) return; // Do nothing if not logged in
        try {
            const token = getToken();
            await apiClient.delete(`/TaskModels/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
            setTasks(tasks.filter(task => task.taskId !== id));
        } catch (error) {
            console.error('Error deleting task:', error);
        }
    };

    const handleLogoutClick = () => {
        setIsModalOpen(true);
    };

    const confirmLogout = () => {
        removeToken();
        setIsLoggedIn(false);
        setIsModalOpen(false);
        setTimeout(() => {
            navigate('/login'); // Redirect to the login page
        }, 3000); // 3 seconds
    };

    const cancelLogout = () => {
        setIsModalOpen(false);
    };

    return (
        <div className="max-w-6xl mx-auto p-6 bg-gray-50 rounded-lg shadow-lg">
            <header className="flex justify-between items-center mb-6">
                <h1 className="text-4xl font-bold text-blue-600">Task List</h1>
                <button
                    onClick={handleLogoutClick}
                    className="bg-red-600 text-white py-2 px-4 rounded-lg transition duration-300 hover:bg-red-700 flex items-center"
                >
                    <FiLogOut className="mr-2" /> Logout
                </button>
            </header>

            <div className="mb-6">
                <Link to="/add-task" className="bg-green-600 text-white py-2 px-4 rounded-lg transition duration-300 hover:bg-green-700 shadow hover:shadow-lg">
                    Add Task
                </Link>
            </div>

            <table className="min-w-full bg-white border border-gray-300 rounded-lg overflow-hidden shadow-lg">
                <thead className="bg-gradient-to-r from-blue-500 to-blue-400 text-white">
                    <tr>
                        <th className="py-3 px-4 border-b border-gray-300">Title</th>
                        <th className="py-3 px-4 border-b border-gray-300">Description</th>
                        <th className="py-3 px-4 border-b border-gray-300">Due Date</th>
                        <th className="py-3 px-4 border-b border-gray-300">Priority</th>
                        <th className="py-3 px-4 border-b border-gray-300">Status</th>
                        <th className="py-3 px-4 border-b border-gray-300 text-center">Edit</th>
                        <th className="py-3 px-4 border-b border-gray-300 text-center">Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {tasks.map((task) => (
                        <tr key={task.taskId} className="hover:bg-gray-100 transition duration-300">
                            <td className="py-4 px-4 border-b border-gray-300">{task.title}</td>
                            <td className="py-4 px-4 border-b border-gray-300">{task.description}</td>
                            <td className="py-4 px-4 border-b border-gray-300">{task.dueDate}</td>
                            <td className="py-4 px-4 border-b border-gray-300">{task.priority}</td>
                            <td className="py-4 px-4 border-b border-gray-300">{task.status}</td>
                            <td className="py-4 px-4 border-b border-gray-300 text-center">
                                <button
                                    className="bg-yellow-500 text-white py-1 px-3 rounded-lg transition duration-300 hover:bg-yellow-600 flex items-center"
                                    onClick={() => navigate(`/tasks/edit/${task.taskId}`)}
                                >
                                    <FiEdit className="mr-1" /> Edit
                                </button>
                            </td>
                            <td className="py-4 px-4 border-b border-gray-300 text-center">
                                <button
                                    className="bg-red-500 text-white py-1 px-3 rounded-lg transition duration-300 hover:bg-red-600 flex items-center ml-2"
                                    onClick={() => deleteTask(task.taskId)}
                                >
                                    <FiTrash className="mr-1" /> Delete
                                    </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {/* Logout Confirmation Modal */}
            {isModalOpen && (
                <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
                    <div className="bg-white p-4 rounded shadow-lg">
                        <h2 className="text-lg font-semibold">Confirm Logout</h2>
                        <p>Are you sure you want to log out?</p>
                        <div className="flex justify-between mt-4">
                            <button className="bg-red-500 text-white px-4 py-2 rounded" onClick={confirmLogout}>Yes</button>
                            <button className="bg-gray-300 px-4 py-2 rounded" onClick={cancelLogout}>No</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default TaskList;
