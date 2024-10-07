using System.ComponentModel.DataAnnotations;

namespace ASPWebAPITask1.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string? StudentName { get; set; }
        [Required]
        [Range(5,18)]
        public int? Age { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
