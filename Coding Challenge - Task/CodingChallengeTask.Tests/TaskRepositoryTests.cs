using CodingChallengeTask.Enums;
using CodingChallengeTask.Exceptions;
using CodingChallengeTask.Interfaces;
using CodingChallengeTask.Models;
using CodingChallengeTask.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingChallengeTask.Tests.Repositories
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private TaskRepository _taskRepository;
        private DbContextOptions<TaskDBContext> _options;
        private TaskDBContext _context;

        [SetUp]
        public void SetUp()
        {
            // Set up in-memory database
            _options = new DbContextOptionsBuilder<TaskDBContext>()
                .UseInMemoryDatabase(databaseName: "TestTaskDB")
                .Options;
            _context = new TaskDBContext(_options);

            // Populate in-memory database with some initial data
            _context.Tasks.AddRange(new List<TaskModel>
            {
                new TaskModel { TaskId = 1, Title = "Test Task 1", Description = "Description 1", DueDate = DateOnly.FromDateTime(new DateTime(2024, 09, 25)), Priority = Priority.Medium, Status = Status.Pending },
                new TaskModel { TaskId = 2, Title = "Test Task 2", Description = "Description 2", DueDate = DateOnly.FromDateTime(new DateTime(2024, 09, 15)), Priority = Priority.High, Status = Status.InProgress }
            });
            _context.SaveChanges();

            _taskRepository = new TaskRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Cleanup after tests
            _context.Dispose();
        }

        [Test]
        public async Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            // Act
            var tasks = await _taskRepository.GetAllTasksAsync();

            // Assert
            Assert.AreEqual(2, tasks.Count(), "Expected 2 tasks to be retrieved.");
        }

        [Test]
        public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
        {
            // Act
            var task = await _taskRepository.GetTaskByIdAsync(1);

            // Assert
            Assert.IsNotNull(task);
            Assert.AreEqual("Test Task 1", task.Title);
        }

        [Test]
        public void GetTaskByIdAsync_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _taskRepository.GetTaskByIdAsync(999));
            Assert.AreEqual("Task with ID 999 not found.", ex.Message);
        }

        [Test]
        public async Task AddTaskAsync_ShouldAddNewTask()
        {
            // Arrange
            var newTask = new TaskModel
            {
                TaskId = 3,
                Title = "New Task",
                Description = "New Task Description",
                DueDate = DateOnly.FromDateTime(new DateTime(2024, 10, 30)),
                Priority = Priority.Low,
                Status = Status.Pending
            };

            // Act
            await _taskRepository.AddTaskAsync(newTask);

            // Assert
            var tasks = await _context.Tasks.ToListAsync();
            Assert.AreEqual(3, tasks.Count, "Expected task count to be 3 after adding a new task.");
            Assert.AreEqual("New Task", tasks[2].Title);
        }

        [Test]
        public void AddTaskAsync_ShouldThrowRepositoryException_WhenDbUpdateFails()
        {
            // Arrange
            var newTask = new TaskModel
            {
                TaskId = 0,
                Title = "Invalid Task",
                Description = null, // Invalid because Description is [Required]
                DueDate = DateOnly.FromDateTime(new DateTime(2024, 10, 30)),
                Priority = Priority.Low,
                Status = Status.Pending
            };

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(() => _taskRepository.AddTaskAsync(newTask));
        }

        [Test]
        public async Task UpdateTaskAsync_ShouldUpdateExistingTask()
        {
            // Arrange
            var updatedTask = new TaskModel
            {
                TaskId = 1,
                Title = "Updated Task",
                Description = "Updated Description",
                DueDate = DateOnly.FromDateTime(new DateTime(2024, 09, 18)),
                Priority = Priority.High,
                Status = Status.Completed
            };

            // Act
            await _taskRepository.UpdateTaskAsync(updatedTask);

            // Assert
            var task = await _taskRepository.GetTaskByIdAsync(1);
            Assert.AreEqual("Updated Task", task.Title);
            Assert.AreEqual(Status.Completed, task.Status);
        }

        [Test]
        public void UpdateTaskAsync_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            var nonExistentTask = new TaskModel
            {
                TaskId = 999,
                Title = "Non-existent Task",
                Description = "Description",
                DueDate = DateOnly.FromDateTime(new DateTime(2024, 11, 25)),
                Priority = Priority.Low,
                Status = Status.Pending
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _taskRepository.UpdateTaskAsync(nonExistentTask));
            Assert.AreEqual("Task with ID 999 not found.", ex.Message);
        }

        [Test]
        public async Task DeleteTaskByIdAsync_ShouldDeleteTask_WhenTaskExists()
        {
            // Act
            await _taskRepository.DeleteTaskByIdAsync(1);

            // Assert
            var tasks = await _taskRepository.GetAllTasksAsync();
            Assert.AreEqual(1, tasks.Count(), "Expected task count to be 1 after deletion.");
        }

        [Test]
        public void DeleteTaskByIdAsync_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _taskRepository.DeleteTaskByIdAsync(999));
            Assert.AreEqual("Task with ID 999 not found.", ex.Message);
        }
    }
}
