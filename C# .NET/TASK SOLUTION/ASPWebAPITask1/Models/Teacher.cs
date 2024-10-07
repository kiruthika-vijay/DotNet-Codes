using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebAPITask1.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [Required]
        public string? TeacherName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime HireDate { get; set; }

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
