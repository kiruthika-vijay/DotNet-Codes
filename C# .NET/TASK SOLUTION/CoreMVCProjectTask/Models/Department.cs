namespace CoreMVCProjectTask.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        // Navigation properties
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
