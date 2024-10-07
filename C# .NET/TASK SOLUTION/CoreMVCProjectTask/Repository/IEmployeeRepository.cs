using CoreMVCProjectTask.Models;

namespace CoreMVCWebApp.Respository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int EmployeeID);
        Task InsertAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int employeeId);
        Task SaveAsync();
    }
}

