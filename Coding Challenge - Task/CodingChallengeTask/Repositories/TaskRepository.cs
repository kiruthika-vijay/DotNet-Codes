using CodingChallengeTask.Exceptions;
using CodingChallengeTask.Interfaces;
using CodingChallengeTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallengeTask.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDBContext _context;
        public TaskRepository(TaskDBContext context)
        {
            _context = context;
        }

        // Retrieve all tasks
        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            try
            {
                return await _context.Tasks.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving all tasks.", ex);
            }
        }

        // Retrieve a single task by its ID
        public async Task<TaskModel> GetTaskByIdAsync(int taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null)
                {
                    throw new NotFoundException($"Task with ID {taskId} not found.");
                }
                return task;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving task by ID.", ex);
            }
        }

        // Add a new task
        public async Task AddTaskAsync(TaskModel task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding new task to the database.", ex);
            }
        }

        // Update an existing task
        public async Task UpdateTaskAsync(TaskModel task)
        {
            try
            {
                var existingTask = await GetTaskByIdAsync(task.TaskId);
                if(existingTask == null)
                {
                    throw new NotFoundException($"Task with ID {task.TaskId} not found.");
                }
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.Priority = task.Priority; 
                existingTask.Status = task.Status;

                await _context.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Concurrency error occurred while updating task.", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating task.", ex);
            }
        }

        // Delete a task by its ID
        public async Task DeleteTaskByIdAsync(int taskId)
        {
            try
            {
                var task = await GetTaskByIdAsync(taskId);
                if (task == null)
                {
                    throw new NotFoundException($"Task with ID {taskId} not found.");
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error deleting task from the database.", ex);
            }
        }
    }
}


