using System.ComponentModel.DataAnnotations;

namespace CoreMVCProjectTask.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Designation {  get; set; }
        [Required]
        public int DepartmentID { get; set; }

        // Navigation properties
        public virtual Department? Department { get; set; }

    }
}
