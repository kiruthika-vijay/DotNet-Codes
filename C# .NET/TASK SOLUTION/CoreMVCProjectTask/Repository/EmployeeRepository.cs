using CoreMVCProjectTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMVCWebApp.Respository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //The following variable is going to hold the EmployeeDBContext instance
        private readonly EFCoreDbContext _context;

        //Initializing the EmployeeDBContext instance which it received as an argument
        //MVC Framework DI Container will provide the EFCoreDbContext instance
        public EmployeeRepository(EFCoreDbContext context)
        {
            _context = context;
        }

        //Returns all employees from the database.
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.Include(e => e.Department).ToListAsync();
        }
        public async Task<Employee?> GetByIdAsync(int EmployeeID)
        {
            var employee = await _context.Employees
               .Include(e => e.Department)
               .FirstOrDefaultAsync(m => m.EmployeeId == EmployeeID);

            return employee;
        }
        public async Task InsertAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }
        public async Task DeleteAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}


