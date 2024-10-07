// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { AuthProvider } from './Auth/AuthContext';
import Login from './Auth/LoginForm';
import TaskList from './components/TaskList';
import AddTask from './components/AddTask';
import EditTask from './components/EditTask';
import './index.css'; // or the path to your Tailwind CSS file
import Logout from './Auth/Logout';
import Register from './Auth/Register';
import ViewTask from './components/ViewTask';

const App = () => {
    return (
        <AuthProvider>
            <Router>
                <Routes>
                    <Route path="/" element={<Login />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/logout" element={<Logout />} />
                    <Route path="/tasks" element={<TaskList />} />
                    <Route path="/viewTasks" element={<ViewTask />} />
                    <Route path="/add-task" element={<AddTask />} />
                    <Route path="/tasks/edit/:id" element={<EditTask />} />
                </Routes>
            </Router>
        </AuthProvider>
    );
};

export default App;
