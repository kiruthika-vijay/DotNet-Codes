using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodingChallengeTask.Models;
using Microsoft.AspNetCore.Authorization;
using CodingChallengeTask.Interfaces;
using CodingChallengeTask.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CodingChallengeTask.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskModelsController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskModelsController> _logger;

        public TaskModelsController(ITaskRepository taskRepository, ILogger<TaskModelsController> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        // GET: api/TaskModels
        [Authorize(Roles = "Customer, Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks()
        {
            try
            {
                _logger.LogInformation("Fetching all tasks.");
                var tasks = await _taskRepository.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch(RepositoryException ex)
            {
                _logger.LogError(ex, "Error fetching tasks.");
                return StatusCode(500, "Internal server error occured while retrieving tasks.");
            }
        }

        // GET: api/TaskModels/5
        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskByID(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching task with ID {id}");
                var task = await _taskRepository.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, $"Task with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, $"Error fetching task with ID {id}");
                return StatusCode(500, "Internal server error occurred while retrieving the task.");
            }
        }

        // PUT: api/TaskModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskByID(int id, TaskModel taskModel)
        {
            if (id != taskModel.TaskId)
            {
                _logger.LogWarning($"Task ID mismatch: {id} != {taskModel.TaskId}");
                return BadRequest("Task ID mismatch.");
            }

            try
            {
                _logger.LogInformation($"Updating task with ID {id}");
                await _taskRepository.UpdateTaskAsync(taskModel);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, $"Task with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, $"Error updating task with ID {id}");
                return StatusCode(500, "Internal server error occurred while updating the task.");
            }
        }

        // POST: api/TaskModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TaskModel>> AddNewTask(TaskModel taskModel)
        {
            try
            {
                _logger.LogInformation("Adding a new task.");
                await _taskRepository.AddTaskAsync(taskModel);
                return CreatedAtAction("GetAllTasks", new { id = taskModel.TaskId }, taskModel);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error occurred while adding task.");
                return BadRequest(ex.Message);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, "Error adding task.");
                return StatusCode(500, "Internal server error occurred while adding the task.");
            }
        }

        // DELETE: api/TaskModels/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskByID(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting task with ID {id}");
                await _taskRepository.DeleteTaskByIdAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, $"Task with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, $"Error deleting task with ID {id}");
                return StatusCode(500, "Internal server error occurred while deleting the task.");
            }
        }
    }
}
