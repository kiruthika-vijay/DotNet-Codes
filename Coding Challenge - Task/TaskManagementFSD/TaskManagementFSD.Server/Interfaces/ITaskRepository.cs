using TaskManagementFSD.Server.Models;

namespace TaskManagementFSD.Server.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetAllTasksAsync();
        Task<TaskModel> GetTaskByIdAsync(int taskId);
        Task AddTaskAsync(TaskModel task);
        Task UpdateTaskAsync(TaskModel task);
        Task DeleteTaskByIdAsync(int taskId);
    }
}
