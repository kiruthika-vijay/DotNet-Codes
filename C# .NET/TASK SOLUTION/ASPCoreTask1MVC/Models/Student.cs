using System.ComponentModel.DataAnnotations;

namespace ASPCoreTask1MVC.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateOnly DOB { get; set; }

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Fee> Fees { get; set; } = new List<Fee>();
    }
}
