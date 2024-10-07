using System.ComponentModel.DataAnnotations;

namespace ASPCoreTask1MVC.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public int TeacherId { get; set; }

        // Navigation properties
        public virtual Teacher? Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
